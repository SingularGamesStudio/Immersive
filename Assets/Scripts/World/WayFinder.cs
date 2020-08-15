using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class WayFinder : MonoBehaviour
{
    public static WayFinder _w;
    List<Room> Rooms;
    List<Door> Doors;
    public int DoneCalc = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        _w = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    class Dxtra {
        public float minD = 1000000000;
        public Door Node;
    }
    public List<Door> FindWay(int RoomNow, int[] PathLens)
    {
        if (DoneCalc >= Rooms.Count) {
            Dxtra[] dx = new Dxtra[Doors.Count];
            int i = 0;
            foreach(Door d in Doors)
            {
                dx[i].Node = d;
                if (d.RoomsConnected[0] == Rooms[RoomNow] || d.RoomsConnected[1] == Rooms[RoomNow])
                {
                    for (int j = 0; j < Rooms[RoomNow].Doors.Count; j++)
                    {
                        if (Rooms[RoomNow].Doors[j] == d)
                        {
                            dx[j].minD = PathLens[j];
                            break;
                        }
                    }
                }
                i++;
            }
            while (true)
            {

            }
        }
    }
}
