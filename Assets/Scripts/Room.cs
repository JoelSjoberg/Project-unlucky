using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    
    public Vector3 pos;
    public float width, height;
    public List<Room> adjList = new List<Room>();
    public bool visited = false;
    public int DFI = -1;
        public Room(float x, float z, float width, float height)
        {
            pos = new Vector3(x, 0, z);
            this.width = width;
            this.height = height;
        }

        // check if value is between min and max
        public bool valueInRange(float value, float min, float max)
        {
            return (value >= min) && (value <= max);
        }

    // Rooms are colliding if one edge is inside the other room
        private bool xOverlap, yOverlap;
        public bool isRoomColliding(Room room)
        {
            // if one room-edge is between the others x-values and
            xOverlap = valueInRange(pos.x, room.pos.x, room.pos.x + room.width) || valueInRange(room.pos.x, pos.x, pos.x + width);
            // if one room-edge is between the others y-values
            yOverlap = valueInRange(pos.z, room.pos.z, room.pos.z + room.height) || valueInRange(room.pos.z, pos.z, pos.z + height);

            return xOverlap && yOverlap;
        }


        private bool xCol, yCol;
        public bool vectorInRoom(Vector3 v, int offset)
        {
            xCol = valueInRange(v.x, pos.x + offset, pos.x + width - offset);
            yCol = valueInRange(v.z, pos.z + offset, pos.z + height - offset);

            return xCol && yCol;
        }

        public Vector3 getRoomCenter()
        {
            return new Vector3(pos.x + width / 2, pos.y, pos.z + height / 2);
        }

        float offset = 10; 
        public Vector3 getRandomRoomPosition()
        {
        // avoid the exact edges by using the offset
            return new Vector3(Random.Range(pos.x + offset, pos.x + width - offset), 0, Random.Range(pos.z + offset, pos.z + height - offset));
        }

        private Vector3 v, searcher;
        public Vector3 getDoorPosition(Vector3 adjRoom, int offset)
        {
            v = (adjRoom - this.getRoomCenter()).normalized; // get vector between rooms
            searcher = this.getRoomCenter();
            while (vectorInRoom(searcher, offset))
            {
                searcher += v; // Add unit to vector untill it is outside the room
            }
            return searcher;

        }
}
