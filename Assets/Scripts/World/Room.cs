using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Door> Doors;
    float[,] Dist;
    Seeker[,] DistCheckers;
    int St = 3;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(Door d1 in Doors)
        {
            d1.RoomsConnected.Add(this);
            int j = 0;
            foreach (Door d2 in Doors)
            {
                if (d2 != d1)
                {
                    GameObject g = Instantiate(Global.PathFinder);
                    g.transform.position = d1.transform.position;
                    g.GetComponent<AIDestinationSetter>().target = d2.transform;
                    DistCheckers[i, j] = g.GetComponent<Seeker>();
                }
                j--;
            }
            j--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(St>0)
            St--;
        if (St == 1)
        {
            for(int i = 0; i<Doors.Count; i++)
            {
                for (int j = 0; j < Doors.Count; j++)
                {
                    Dist[i, j] = DistCheckers[i, j].path.GetTotalLength();
                }
            }
            WayFinder._w.DoneCalc++;
        }
    }
}
