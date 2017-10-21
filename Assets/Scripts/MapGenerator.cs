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

	// Use this for initialization
	void Start () {
		//cubes
		for (int i = 0; i < 10; i++) {
			Instantiate(prefab, new Vector3(i * 2f, 0, 27), Quaternion.identity);
		}
        makeMap();
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
            Instantiate(caveGenerator, new Vector3((r.x - 200), 0, r.z), Quaternion.identity);
        }
	}

	void spawnDoors(){
		for (int x = 0; x < xNumberRooms; x++) {
			for (int y = 0; y < yNumberRooms; y++) {
				//Instantiate (doorParent, new Vector3 (x * 80, 2 , y * 60), Quaternion.identity);
			}

		}
	}

    public float minWidth = 60, minHeight = 60, maxWidth = 120, maxHeight = 120;
    public int minRooms = 6, maxRooms = 10;
    private int num_rooms;
    private List<Room> graph = new List<Room>();
    public int iterations = 60;

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
                x = parent.x;
                z = parent.z - height - 1;
            }
            else if (direction == 2) // RIGHT
            {
                x = parent.x + parent.width + 1;
                z = parent.z;
            }
            else if (direction == 3) // DOWN
            {
                x = parent.x;
                z = parent.z + parent.height + 1;
            }
            else if (direction == 4) // LEFT
            {
                x = parent.x - width + 1;
                z = parent.z;
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
        print(graph.Count + " rooms generated");
    }

    class Room
    {
        public float x, z, width, height;

        public Room(float x, float z, float width, float height)
        {
            this.x = x;
            this.z = z;
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
            bool xOverlap = valueInRange(x, room.x, room.x + room.width) || valueInRange(room.x, x, x + width);
            // if one room-edge is between the others y-values
            bool yOverlap = valueInRange(z, room.z, room.z + room.height) || valueInRange(room.z, z, z + height);

            return xOverlap && yOverlap;
        }
    }
}
