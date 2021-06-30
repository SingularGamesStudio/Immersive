//using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(IDHolder))]
public class NPC : DestObject
{
    Vector2Int pos;
    public Needs need;
    public IDHolder ID;
    [System.Serializable]
    public class Needs
    {
        public int Hunger = 0;
        public int HungerRate;
        public int HungerMax;

        public int Thirst = 0;
        public int ThirstRate;
        public int ThirstMax;

        public int Sleep = 0;
        public int SleepRate;
        public int SleepMax;
    }
    
    void Start()
    {
        ID = gameObject.GetComponent<IDHolder>();
        pos = AStar._a.getLocalPos(gameObject.transform.position);
    }

    void Update()
    {

    }

    void Find()
    {
        int coef_Food = (int)((need.Hunger / need.HungerMax) * 100);
        if (need.Hunger > need.HungerMax * 0.5f)
            coef_Food = 50+((int)((need.Hunger/need.HungerMax)*100-50))*((int)((need.Hunger / need.HungerMax) * 100 - 50));
        int coef_Drink = (int)((need.Thirst*1.5f / need.ThirstMax) * 100);
        if (need.Thirst * 1.5f > need.ThirstMax * 0.5f)
            coef_Drink = 50 + ((int)((need.Thirst * 1.5f / need.ThirstMax) * 100 - 50)) * ((int)((need.Thirst * 1.5f / need.ThirstMax) * 100 - 50));
        long best = -1000000000000;
        long[] bestat = new long[20];
        for (int dx = -2; dx <= 2; dx++)
        {
            for (int dy = -2; dy <= 2; dy++)
            {
                if (ID.chunk.x + dx >= 0 && ID.chunk.y + dy >= 0)
                {
                    foreach (int id in AStar._a.chunk[ID.chunk.x, ID.chunk.y])
                    {
                        if (id != ID.ID)
                        {
                            DestObject obj = Global.ID[id].GetObject();
                            if (obj != null)
                            {
                                long waylen = 0;
                                int num = 0;
                                foreach (GameAction act in obj.actions)
                                {
                                    if (act.check(ID.ID))
                                    {
                                        long val = 0;
                                        foreach (GameEvent ev in act.events) 
                                        {
                                            if (ev.type == GameEvent.EventType.changeFood)
                                            {
                                                if (ev.targets[0] == -1 || ev.targets[0] == ID.ID)
                                                {
                                                    val += (coef_Food * Mathf.Min(-ev.targets[1], need.Hunger)) / need.Hunger;
                                                }
                                            }
                                        }
                                        val += waylen;
                                        if (val > best)
                                        {
                                            best = val;
                                            bestat[0] = id;
                                            bestat[1] = num;
                                            bestat[2] = -1;
                                        }
                                    }
                                    num++;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
