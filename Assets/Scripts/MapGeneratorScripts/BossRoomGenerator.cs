using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomGenerator : MonoBehaviour {

    private List<Room> graph = new List<Room>();
    private List<Room> tree = new List<Room>();

    public Transform wall, plane, roof, Scrap;
    [HideInInspector]
    public PlayerControllerMapTut player;
    public Door door;
    public int bossRoomWidth, bossRoomHeight, doorOffset, roomspace;
    private Room spawnRoom, bossRoom;
    void Start () {
        player = FindObjectOfType<PlayerControllerMapTut>();

        makeBossRoom();
        graph[0].visited = true; // initialize visited value from the start
        player.currentRoom = graph[0];
        player.spawn(new Vector3(player.currentRoom.width / 2, transform.position.y, 10));
        makeRoomObjects();
    }

    private void makeBossRoom()
    {
        bossRoom = new Room(0, 0, bossRoomWidth, bossRoomHeight);
        graph.Add(bossRoom);
    }

    private void makeRoomObjects()
    {
        foreach (Room r in graph)
        {
            // create wall to each room
            // lower wall
            wall.GetComponent<RoomWall>().makeSize(r.width, roomspace);
            Instantiate(wall, new Vector3(r.pos.x + r.width / 2, transform.position.y, r.pos.z + roomspace / 2), Quaternion.identity);

            // upper Wall
            wall.GetComponent<RoomWall>().makeSize(r.width, roomspace);
            Instantiate(wall, new Vector3(r.pos.x + r.width / 2, transform.position.y, r.pos.z + r.height - roomspace / 2), Quaternion.identity);

            // left wall
            wall.GetComponent<RoomWall>().makeSize(roomspace, r.height);
            Instantiate(wall, new Vector3(r.pos.x + roomspace / 2, transform.position.y, r.pos.z + r.height / 2), Quaternion.identity);

            // right wall
            wall.GetComponent<RoomWall>().makeSize(roomspace, r.height);
            Instantiate(wall, new Vector3(r.pos.x + r.width - roomspace / 2, transform.position.y, r.pos.z + r.height / 2), Quaternion.identity);

            // create panel showing the room area
            plane.transform.localScale = new Vector3(r.width / 10, 0.1f, r.height / 10);
            Instantiate(plane, new Vector3(r.pos.x + r.width / 2, -4.9f + transform.position.y, r.pos.z + r.height / 2), Quaternion.identity);
            roof.transform.localScale = new Vector3(r.width / 10, 20f, r.height / 10); // this math makes no sense, but it works
            Instantiate(roof, new Vector3(r.pos.x + r.width / 2, 6f + transform.position.y, r.pos.z + r.height / 2), Quaternion.identity);

        }
    }

    public void spawnScrap()
    {
        for (int i = 0; i < Random.Range(10, 50); i++)
        {
            Instantiate(Scrap, bossRoom.getRandomRoomPosition(10, transform.position.y + 40), Quaternion.identity);
        }
    }
}
