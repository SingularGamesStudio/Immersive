//using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : DestObject
{
    Vector2Int pos;
    public Needs need;
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

    }
}
