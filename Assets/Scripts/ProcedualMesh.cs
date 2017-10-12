using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualMesh : MonoBehaviour {
	//Simplest way of creating a mesh 

	Mesh mesh;
	Vector3[] vertices;
	int[] triangles;

	void Awake(){
		mesh = GetComponent<MeshFilter> ().mesh;
	}

	// Use this for initialization
	void Start () {
		MakeMeshData ();
		CreateMesh ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MakeMeshData(){
		//array of verticies
		vertices = new Vector3[] {
			//1d Quad
			new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), new Vector3 (1, 0, 0), new Vector3(1, 0, 1)
		};

		//order of drawings, CLOCKWISE
		triangles = new int[]{0, 1, 2, 2, 1, 3};

	}

	void CreateMesh(){
		//refers to the object's mesh
		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;

		//for shading
		mesh.RecalculateNormals ();
	}

}
