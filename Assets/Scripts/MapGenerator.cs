using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	//Creates CaveGenerators

	public Transform prefab, caveGenerator, wall, doorParent, plane;
    public PlayerControllerMapTut player;
    public Door door;

    // for map generator
    private List<Room> graph = new List<Room>();
    private List<Room> tree = new List<Room>();

    public float minWidth = 60, minHeight = 60, maxWidth = 120, maxHeight = 120, space = 15;
    public int minRooms = 6, maxRooms = 10, doorOffset = 0;
    private int num_rooms = 7, iterations = 60;
    // Use this for initialization
    void Start () {

        makeMap();

        graph[0].visited = true; // initialize visited value from the start
        player.currentRoom = graph[0];
        player.spawn(player.currentRoom.width / 2, player.currentRoom.height / 2);
        tree.Add(graph[0]); // add spawnroom to tree before algorithm begins
        connectMapWithPrims(); // excecute prims algorithm to create map connections
        // each connection now exists in each rooms adjList(i.e. if you want to know which room is connected to which)
        //  simply check the adjList
        print(graph.Count + " rooms generated");
        print("Printing rooms in spanning tree");

        //access script before initiation
        GameObject gameObject = caveGenerator.GetComponent<CellularAutomata>().gameObject;
        //CellularAutomata script = gameObject.GetComponent<CellularAutomata> ();
        //caves

        foreach (Room r in graph)
        {
            caveGenerator.GetComponent<CellularAutomata>().width = Mathf.RoundToInt(r.width);
            caveGenerator.GetComponent<CellularAutomata>().height = Mathf.RoundToInt(r.height);
            //caveGenerator.GetComponent<CellularAutomata>().randomFillPercent = 30;

            
            // make cave-room at room position
            //Instantiate(caveGenerator, new Vector3(r.pos.x, 0, r.pos.z), Quaternion.identity);
            float roomspace = space / 2;
            // create wall to each room
            // lower wall
            wall.GetComponent<RoomWall>().makeSize(r.width, roomspace);
            Instantiate(wall, new Vector3(r.pos.x + r.width / 2, 0, r.pos.z - roomspace / 2), Quaternion.identity);

            // upper Wall
            wall.GetComponent<RoomWall>().makeSize(r.width, roomspace);
            Instantiate(wall, new Vector3(r.pos.x + r.width / 2, 0, r.pos.z + r.height + roomspace / 2), Quaternion.identity);

            // left wall
            wall.GetComponent<RoomWall>().makeSize(roomspace, r.height);
            Instantiate(wall, new Vector3(r.pos.x - roomspace / 2, 0 , r.pos.z + r.height / 2), Quaternion.identity);

            // right wall
            wall.GetComponent<RoomWall>().makeSize(roomspace, r.height);
            Instantiate(wall, new Vector3(r.pos.x + r.width + roomspace / 2, 0, r.pos.z + r.height / 2), Quaternion.identity);
            
            // create panel showing the room area
            plane.transform.localScale = new Vector3(r.width / 10, 0.1f, r.height / 10);
            Instantiate(plane, new Vector3(r.pos.x + r.width / 2, -4.9f, r.pos.z + r.height / 2), Quaternion.identity);

        }

        Door door1, door2;
       /* for(int i = 1; i < tree.Count; i++)
        {
            door1 = Instantiate(door, graph[i].getDoorPosition(graph[i - 1].getRoomCenter(), doorOffset), Quaternion.identity) as Door;
            door1.room = graph[i];
            door2 = Instantiate(door, graph[i - 1].getDoorPosition(graph[i].getRoomCenter(), doorOffset), Quaternion.identity) as Door;
            door2.room = graph[i - 1];

            door1.connectToPair(door2);
            
        }*/
        // this is buggy
        foreach(Room r in graph)
        {
            foreach(Room n in r.adjList)
            {
                door1 = Instantiate(door, r.getDoorPosition(n.getRoomCenter(), doorOffset), Quaternion.identity) as Door;
                door1.room = r;
                door2 = Instantiate(door, n.getDoorPosition(r.getRoomCenter(), doorOffset), Quaternion.identity) as Door;
                door2.room = n;

                door1.connectToPair(door2);
            }
        }
    }

    private void Update()
    {
        drawRoomConnectors();
    }
    // Make initial spawn room
    private void makeSpawnRoom()
    {
        Room spawnRoom = new Room(0, 0, minWidth + 20, minHeight + 20);
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
                z = parent.pos.z - height - space;
            }
            else if (direction == 2) // RIGHT
            {
                x = parent.pos.x + parent.width + space;
                z = parent.pos.z;
            }
            else if (direction == 3) // DOWN
            {
                x = parent.pos.x;
                z = parent.pos.z + parent.height + space;
            }
            else if (direction == 4) // LEFT
            {
                x = parent.pos.x - width + space;
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

    // make rooms in succession
    private void makeMap()
    {
        // create spawn room
        makeSpawnRoom();
        num_rooms = Random.Range(minRooms, maxRooms);
        //while(graph.Count < num_rooms)
        for (int i = 0; i < iterations; i++)
        {
            int n = Random.Range(1, 5); // 5 is excluded so the real range is from 1 - 4
            
            for (int j = 1; j <= n; j++)
            {
                makeRoomInDirection(graph[graph.Count - 1], j);
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

    // Show how the minimum spanning tree is built up

        // buggy connections
    void drawRoomConnectors()
    {
        
        foreach( Room r in graph)
        {
            foreach(Room n in r.adjList)
            {
                Debug.DrawLine(r.getRoomCenter(), n.getRoomCenter(), Color.red);
            }
        }

        // linear connections
        /*for (int i = 1; i < graph.Count; i++)
        {
            Debug.DrawLine(graph[i].getRoomCenter(), graph[i - 1].getRoomCenter(), Color.red);
        }*/
    }
}
