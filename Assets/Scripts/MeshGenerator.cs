using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator : MonoBehaviour {
	//[fixed] Optimize mesh delete code in CreateWallMesh()
	//[	] texture code needs to be merged and tested
	//2d is not implemented since Unity's 2d mode is supposed to not work with the NavMeshes that are needed for path finding

	public SquareGrid squareGrid;
	public MeshFilter walls;
	public MeshFilter cave;
	public bool is2D;
	private int caveXPos;
	private int caveYPos;

	List<Vector3> vertices;
	List<int> triangles;

	Dictionary<int, List<Triangle>> triangleDictionary = new Dictionary<int, List<Triangle>>();
	List<List<int>> outlines = new List<List<int>> ();
	HashSet<int> checkedVertices = new HashSet<int> ();


	public void GenerateMesh(int[,] map, float squareSize, int _caveXPos, int _caveYPos) {	
		caveXPos = _caveXPos;
		caveYPos = _caveYPos;

		triangleDictionary.Clear ();
		outlines.Clear ();
		checkedVertices.Clear ();

		//added currentMapGeneration
		squareGrid = new SquareGrid(map, squareSize, caveXPos, caveYPos);
		vertices = new List<Vector3> ();
		triangles = new List<int> ();

		for (int x = 0; x < squareGrid.squares.GetLength (0); x++) {
			for (int y = 0; y < squareGrid.squares.GetLength (1); y++) {
				TriangulateSquare (squareGrid.squares [x, y]);

			}
		}

		Mesh mesh = new Mesh ();
		cave.mesh = mesh;
		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();
		mesh.RecalculateNormals ();



		if (!is2D) {
			CreateWallMesh ();
		}
	}


	void CreateWallMesh(){
		//Optimize according to:

		/*
		instead of going through your entire code & deleting all the collider locations, just add a Mesh Collider Component to your Walls object (in unity).  Then in your MeshGenerator script...

		Instead of:
		MeshCollider wallCollider = walls.gameObject.AddComponent<MeshCollider>();  (should be around line 110)

		replace:
		MeshCollider wallCollider = walls.gameObject.GetComponent<MeshCollider>();

		That way instead of adding colliders, then destroying them over and over, you're just reassigning the collider's position every time you redraw your map.  
		Granted there are better optimizations than what I've presented, and other reasons why you'd call destroy.		
		Rule of thumb: Don't call Destroy if you don't have to. It invokes the Garbage Collector which can cause spikes in performance. 
		Check out Object Pooling for more info on this type of optimization.﻿
		
		Old code that is tested and works
		MeshCollider[] currentColliders3D = walls.gameObject.GetComponents<MeshCollider>();

		for (int i = 0; i < currentColliders3D.Length; i++)
		{
			Destroy(currentColliders3D[i]);

		}
		*/


		CalculateMeshOutlines ();

		List<Vector3> wallVertices = new List<Vector3> ();
		List<int> wallTriangles = new List<int> ();
		Mesh wallMesh = new Mesh ();
		float wallHeight = 5;

		foreach (List<int> outline in outlines) {
			for (int i = 0; i < outline.Count - 1; i++) {
				int startIndex = wallVertices.Count;
				wallVertices.Add (vertices [outline [i]]);		//left 
				wallVertices.Add (vertices [outline [i+1]]);	//right 
				wallVertices.Add (vertices [outline [i]] - Vector3.up * wallHeight);		//bottom left 
				wallVertices.Add (vertices [outline [i+1]] - Vector3.up * wallHeight);		//bottom right 

				wallTriangles.Add (startIndex + 0);
				wallTriangles.Add (startIndex + 2);
				wallTriangles.Add (startIndex + 3);

				wallTriangles.Add (startIndex + 3);
				wallTriangles.Add (startIndex + 1);
				wallTriangles.Add (startIndex + 0);

			}
		}
		wallMesh.vertices = wallVertices.ToArray ();
		wallMesh.triangles = wallTriangles.ToArray ();
		walls.mesh = wallMesh;

		//Wall collider code
		MeshCollider wallCollider = walls.gameObject.GetComponent<MeshCollider>();
		wallCollider.sharedMesh = wallMesh;
	}

	void TriangulateSquare(Square square){
		switch (square.configuration) {
			case 0:
				break;

			//points refer to the # of control nodes
			// 1 points: 
			//triangle
			//A	B
			//C
			case 1:
				MeshFromPoints(square.centreLeft, square.centreBottom, square.bottomLeft);
				break;
			case 2:
				MeshFromPoints(square.bottomRight, square.centreBottom, square.centreRight);
				break;
			case 4:
				MeshFromPoints(square.topRight, square.centreRight, square.centreTop);
				break;
			case 8:
				MeshFromPoints(square.topLeft, square.centreTop, square.centreLeft);
				break;

			// 2 points:
			//
			//D		A
			//C		B

			case 3:
				MeshFromPoints(square.centreRight, square.bottomRight, square.bottomLeft, square.centreLeft);
				break;
			case 6:
				MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.centreBottom);
				break;
			case 9:
				MeshFromPoints(square.topLeft, square.centreTop, square.centreBottom, square.bottomLeft);
				break;
			case 12:
				MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreLeft);
				break;
			case 5:
				MeshFromPoints(square.centreTop, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft, square.centreLeft);
				break;
			case 10:
				MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.centreBottom, square.centreLeft);
				break;

				// 3 point:
			case 7:
				MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.bottomLeft, square.centreLeft);
				break;
			case 11:
				MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.bottomLeft);
				break;
			case 13:
				MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft);
				break;
			case 14:
				MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.centreBottom, square.centreLeft);
				break;

			// 4 point:
			//full cube
		case 15:
				MeshFromPoints (square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
				checkedVertices.Add (square.topLeft.vertexIndex);
				checkedVertices.Add (square.topRight.vertexIndex);
				checkedVertices.Add (square.bottomRight.vertexIndex);
				checkedVertices.Add (square.bottomLeft.vertexIndex);
				break;
		}
	}


	//Mesh = 3d model
	//everything made of triangles
	void MeshFromPoints(params Node[] points){
		AssignVertices (points);
		if (points.Length >= 3) {
			CreateTriangle (points [0], points [1], points [2]);
		}
		//two triangles
		if (points.Length >= 4) {
			CreateTriangle (points [0], points [2], points [3]);
		}
		//third triangle
		if (points.Length >= 5) {
			CreateTriangle (points [0], points [3], points [4]);
		}
		if (points.Length >= 6) {
			CreateTriangle (points [0], points [4], points [5]);
		}


	}

	//Vertex = point
	void AssignVertices(Node[] points){
		for(int i = 0; i < points.Length; i++){
			if(points[i].vertexIndex == -1){
				points[i].vertexIndex = vertices.Count;
				vertices.Add(points[i].position);
			}
		}
	}

	void CreateTriangle(Node a, Node b, Node c){
		triangles.Add (a.vertexIndex);
		triangles.Add (b.vertexIndex);
		triangles.Add (c.vertexIndex);

		Triangle triangle = new Triangle (a.vertexIndex, b.vertexIndex, c.vertexIndex);
		AddtriangleToDictionary (triangle.vertexIndexA, triangle);
		AddtriangleToDictionary (triangle.vertexIndexB, triangle);
		AddtriangleToDictionary (triangle.vertexIndexC, triangle);


	}

	void AddtriangleToDictionary(int vertexIndexKey, Triangle triangle){
		if (triangleDictionary.ContainsKey (vertexIndexKey)) {
			triangleDictionary [vertexIndexKey].Add (triangle);

		} else {
			List<Triangle> triangleList = new List<Triangle> ();
			triangleList.Add (triangle);
			triangleDictionary.Add (vertexIndexKey, triangleList);
		}
	}

	int GetConnectedOutlineVertex(int vertexIndex){
		List<Triangle> trianglesContainingVertex = triangleDictionary [vertexIndex];

		for (int i = 0; i < trianglesContainingVertex.Count; i ++) {
			Triangle triangle = trianglesContainingVertex[i];

			for (int j = 0; j < 3; j ++) {
				int vertexB = triangle[j];
				if (vertexB != vertexIndex && !checkedVertices.Contains(vertexB)) {
					if (IsOutlineEdge(vertexIndex, vertexB)) {
						return vertexB;
					}
				}
			}
		}

		return -1;
	}

	//check if outline vertices -> follow map around -> add to outlines list
	void CalculateMeshOutlines(){
		for (int vertexIndex = 0; vertexIndex < vertices.Count; vertexIndex++) {
			if (!checkedVertices.Contains (vertexIndex)) {
				int newOutlineVertex = GetConnectedOutlineVertex (vertexIndex);
				if (newOutlineVertex != -1) {
					checkedVertices.Add (vertexIndex);

					List<int> newOutline = new List<int> ();
					newOutline.Add (vertexIndex);
					outlines.Add (newOutline);
					FollowOutline (newOutlineVertex, outlines.Count-1);
					outlines [outlines.Count - 1].Add (vertexIndex);
				}
					
			}
		}
	}


	void FollowOutline(int vertexIndex, int outlineIndex){
		outlines [outlineIndex].Add (vertexIndex);
		checkedVertices.Add (vertexIndex);
		int nextVertexIndex = GetConnectedOutlineVertex (vertexIndex);

		if (nextVertexIndex != -1) {
			FollowOutline (nextVertexIndex, outlineIndex);
		}
	}

	bool IsOutlineEdge(int vertexA, int vertexB){
		List<Triangle> trianglesContainingVertexA = triangleDictionary [vertexA];
		int sharedTriangleCount = 0;

		for (int i = 0; i < trianglesContainingVertexA.Count; i++) {
			if (trianglesContainingVertexA [i].Contains (vertexB)) {
				sharedTriangleCount++;
				if (sharedTriangleCount > 1) {
					break;
				}
			}
		}
		return sharedTriangleCount == 1;
	}

	struct Triangle {
		public int vertexIndexA;
		public int vertexIndexB;
		public int vertexIndexC;
		int[] vertices;

		public Triangle (int a, int b, int c) {
			vertexIndexA = a;
			vertexIndexB = b;
			vertexIndexC = c;

			vertices = new int[3];
			vertices[0] = a;
			vertices[1] = b;
			vertices[2] = c;
		}

		public int this[int i] {
			get {
				return vertices[i];
			}
		}

		public bool Contains(int vertexIndex) {
			return vertexIndex == vertexIndexA || vertexIndex == vertexIndexB || vertexIndex == vertexIndexC;
		}


	}


	//Function where the position of the cave is set
	public class SquareGrid {
		public Square[,] squares;
		private int caveXPos;
		private int caveYPos;

		public SquareGrid(int[,] map, float squareSize, int _caveXPos, int _caveYPos) {
			caveXPos = _caveXPos;
			caveYPos = _caveYPos;
			int nodeCountX = map.GetLength(0);
			int nodeCountY = map.GetLength(1);
			float mapWidth = nodeCountX * squareSize;
			float mapHeight = nodeCountY * squareSize;

			ControlNode[,] controlNodes = new ControlNode[nodeCountX,nodeCountY];

			for (int x = 0; x < nodeCountX; x ++) {
				for (int y = 0; y < nodeCountY; y ++) {
					//Position vector3
					Vector3 pos = new Vector3((mapWidth * caveXPos) + x * squareSize + squareSize/2, 0, (mapHeight * caveYPos) + y * squareSize + squareSize/2);
					controlNodes[x,y] = new ControlNode(pos,map[x,y] == 1, squareSize);
				}
			}

			squares = new Square[nodeCountX -1,nodeCountY -1];
			for (int x = 0; x < nodeCountX-1; x ++) {
				for (int y = 0; y < nodeCountY-1; y ++) {
					squares[x,y] = new Square(controlNodes[x,y+1], controlNodes[x+1,y+1], controlNodes[x+1,y], controlNodes[x,y]);
				}
			}

		}
	}







	public class Square {

		public ControlNode topLeft, topRight, bottomRight, bottomLeft;
		public Node centreTop, centreRight, centreBottom, centreLeft;
		public int configuration;

		//16 ways to turn control nodes on and off
		/*
		x	o	x

		o	o	o
		
		x	o	x	

		*/

		// control nodes are the corners
		public Square (ControlNode _topLeft, ControlNode _topRight, ControlNode _bottomRight, ControlNode _bottomLeft) {
			topLeft = _topLeft;
			topRight = _topRight;
			bottomRight = _bottomRight;
			bottomLeft = _bottomLeft;

			centreTop = topLeft.right;
			centreRight = bottomRight.above;
			centreBottom = bottomLeft.right;
			centreLeft = bottomLeft.above;

			//may be represented in binary
			if(topLeft.active){
				configuration += 8;
			}
			if(topRight.active){
				configuration += 4;
			}
			if(bottomRight.active){
				configuration += 2;
			}
			if(bottomLeft.active){
				configuration += 1;
			}
		}
	}

	public class Node {
		public Vector3 position;
		public int vertexIndex = -1;

		public Node(Vector3 _pos) {
			position = _pos;
		}
	}

	public class ControlNode : Node {

		public bool active;
		public Node above, right;

		public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) {
			active = _active;
			above = new Node(position + Vector3.forward * squareSize/2f);
			right = new Node(position + Vector3.right * squareSize/2f);
		}

	}
}