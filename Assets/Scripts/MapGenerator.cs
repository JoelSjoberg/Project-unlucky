using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	//Creates CaveGenerators

	public Transform prefab;
	public Transform caveGenerator;
	public Transform doorParent;
	public int xNumberRooms = 5;
	public int yNumberRooms = 3;
	float caveWidth;
	float caveHeight;


    // for map generator
    private List<Room> graph = new List<Room>();
    private List<Room> tree = new List<Room>();
    public float minWidth = 60, minHeight = 60, maxWidth = 120, maxHeight = 120;
    public int minRooms = 6, maxRooms = 10;
    private int num_rooms;
    public int iterations = 60;

    // Use this for initialization
    void Start () {
		//cubes
		for (int i = 0; i < 10; i++) {
			Instantiate(prefab, new Vector3(i * 2f, 0, 27), Quaternion.identity);
		}
        makeMap();
        graph[0].visited = true; // initialize visited value from the start
        tree.Add(graph[0]); // add spawnroom to tree before algorithm begins
        connectMapWithPrims(); // excecute prims algorithm to create map connections
        // each connection now exists in each rooms adjList(i.e. if you want to know which room is connected to which)
        //  simply check the adjList
        print(graph.Count + " rooms generated");
        print("Printing rooms in spanning tree");

        foreach (Room r in tree)
        {
            print("Room " + r.pos.x + ", " + r.pos.z + " exists");
        }

		spawnRooms ();
		spawnDoors ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void spawnRooms(){
		//access script before initiation
		GameObject gameObject = caveGenerator.GetComponent<CellularAutomata> ().gameObject;
		CellularAutomata script = gameObject.GetComponent<CellularAutomata> ();
		caveWidth = script.width + 5f;
		caveHeight = script.height + 5f;
		//caves
		for (int x = 0; x < xNumberRooms; x++) {
			for (int y = 0; y < yNumberRooms; y++) {
				//Instantiate (caveGenerator, new Vector3 (x * caveWidth, 0, y * caveHeight), Quaternion.identity);
			}

		}
        foreach (Room r in graph)
        {
            caveGenerator.GetComponent<CellularAutomata>().width = Mathf.RoundToInt(r.width);
            caveGenerator.GetComponent<CellularAutomata>().height = Mathf.RoundToInt(r.height);
            caveGenerator.GetComponent<CellularAutomata>().randomFillPercent = 30;
            Instantiate(caveGenerator, new Vector3((r.pos.x), 0, r.pos.z), Quaternion.identity);
        }
	}

	void spawnDoors(){
		for (int x = 0; x < xNumberRooms; x++) {
			for (int y = 0; y < yNumberRooms; y++) {
				//Instantiate (doorParent, new Vector3 (x * 80, 2 , y * 60), Quaternion.identity);
			}

		}
	}

    // Make initial spawn room
    private void makeSpawnRoom()
    {
        Room spawnRoom = new Room(0, 0, Random.Range(minWidth, maxWidth + 1), Random.Range(minHeight, maxHeight + 1));
        graph.Add(spawnRoom);
    }

    // Make a room in a given direction from parent room
    private void makeRoomInDirection(Room parent, int direction)
    {
        bool collision = true;
        Room newRoom = new Room(0, 0, 0, 0);
        int attempts = 10;
        while(collision && attempts > 0)
        {
            float width = Random.Range(minWidth, maxWidth + 1);
            float height = Random.Range(minHeight, maxHeight + 1);
            float x = 0;
            float z = 0;
            attempts -= 1;
            // add 1 at the end of the longer equations or else every room will always collide
            if (direction == 1) // UP
            {
                x = parent.pos.x;
                z = parent.pos.z - height - 1;
            }
            else if (direction == 2) // RIGHT
            {
                x = parent.pos.x + parent.width + 1;
                z = parent.pos.z;
            }
            else if (direction == 3) // DOWN
            {
                x = parent.pos.x;
                z = parent.pos.z + parent.height + 1;
            }
            else if (direction == 4) // LEFT
            {
                x = parent.pos.x - width + 1;
                z = parent.pos.z;
            }

            newRoom = new Room(x, z, width, height);

            foreach(Room room in graph)
            {
                if (room.isRoomColliding(newRoom))
                {
                    collision = true;
                    break;
                }
                else collision = false;
            }
        }
        if(attempts > 0)
        {
            graph.Add(newRoom);
        }
    }

    private void makeMap()
    {
        // create spawn room
        makeSpawnRoom();
        num_rooms = Random.Range(minRooms, maxRooms);
        for (int i = 0; i < iterations; i++)
        {
            int n = Random.Range(1, 5); // 5 is excluded so the real range is from 1 - 4
            
            for (int j = 1; j <= n; j++)
            {
                makeRoomInDirection(graph[i], j);
                if (graph.Count == num_rooms) break;
            }
            if (graph.Count == num_rooms) break;
        }
    }

    // Connect rooms in a maze like structure
    private void connectMapWithPrims()
    {
        float shortestDistance = Mathf.Infinity;
        Room roomToConnect = graph[0];
        Room parentRoom = graph[0];
        foreach (Room room in tree)
        {
            for (int j = 0; j < graph.Count; j++)
            {
                // count the distance from every node in tree to every node on map
               float distFromCurrentToTree = Mathf.Sqrt(Mathf.Pow((graph[j].pos.x + (graph[j].width / 2) - room.pos.x + (room.width / 2)), 2) + Mathf.Pow((graph[j].pos.z + (graph[j].height / 2) - room.pos.z + (room.height / 2)), 2));
               if (!graph[j].visited && shortestDistance >= distFromCurrentToTree)
               {
                    shortestDistance = distFromCurrentToTree;
                    roomToConnect = graph[j];
                    parentRoom = room;
               }
            }
        }
        roomToConnect.visited = true;
        parentRoom.adjList.Add(roomToConnect);
        roomToConnect.adjList.Add(parentRoom);
        tree.Add(roomToConnect);
        
        while (graph.Count > tree.Count) connectMapWithPrims();
    }

    class Room
    {
        public Vector3 pos;
        public float width, height;
        public List<Room> adjList = new List<Room>();
        public bool visited = false;
        public Room(float x, float z, float width, float height)
        {
            pos = new Vector3(x, 0, z);
            this.width = width;
            this.height = height;
        }

        // check if value is between min and max
        bool valueInRange(float value, float min, float max)
        {
            return (value >= min) && (value <= max);
        }

        // Rooms are colliding if one edge is inside the other room
        public bool isRoomColliding(Room room)
        {
            // if one room-edge is between the others x-values and
            bool xOverlap = valueInRange(pos.x, room.pos.x, room.pos.x + room.width) || valueInRange(room.pos.x, pos.x, pos.x + width);
            // if one room-edge is between the others y-values
            bool yOverlap = valueInRange(pos.z, room.pos.z, room.pos.z + room.height) || valueInRange(room.pos.z, pos.z, pos.z + height);

            return xOverlap && yOverlap;
        }
    }
}
