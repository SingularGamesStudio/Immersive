//using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : DestObject
{
    public Person me;
    public int Hunger;
    public int Alcohol;
    public int Happiness = 100;
    public int Money;
    private Contain _inventory;

    public Contain Inventory {
        get { return _inventory; }
        set { _inventory = value; }
    }

    [Header("Behavior Model")]
    public Profession MyProf;
    public List<TimeTableEvent> TimeTable;
    public int INow;
    public string Doing;

    GameObject Target;
    //AIDestinationSetter ADS;
    // Start is called before the first frame update
    [System.Serializable]
    public class Person
    {
        public string Name;
        public int hp;
        public int SlowWalkSpeed;
        public int WalkSpeed;
        public int RunSpeed;

        [Header("Happiness")]
        public int MinHappinessNeeded;
        public int LowMentalBreak;
        public int MentalBreak;
        public float MentalBreakProbability;

        [Header("Food")]
        public int NutrionPerDay;
        public int NeedForFood;
        public int NeedForAlcohol;

        [Header("Mental")]
        public int MentalPoints;
        public int NeedForBooks;

        [Header("Things")]
        public int NeedForDamage;
        public int NeedForArmor;
        public int NeedForMoney;
    }
    public enum Profession
    {
        Trader,
        JustAGuy,
        Warrior,
        Taxi,
        Farmer,
        God,
        NoWork,
        Guard,
        Mercenary
    }
    [System.Serializable]
    public class TimeTableEvent
    {
        [Range(0, Global.DayLen)]
        public int StartTime;
        public int Len;
        public string Type;
        public List<OneJob> Actions = new List<OneJob>();
    }
    [System.Serializable]
    public class OneJob
    {
        public Transform pos;
        public DestObject ToUse;
        public int len;
    }
    void Start()
    {
        Target =new GameObject();
        Target.transform.position = gameObject.transform.position;
        //ADS = gameObject.GetComponent<AIDestinationSetter>();
        //ADS.target = Target.transform;
    }
    int TTNow, TTNowTime;
    bool Staying;
    // Update is called once per frame
    void Update()
    {
        if (Doing == "Timetable")
        {
            if (Global.Time >= TimeTable[INow].StartTime + TimeTable[INow].Len)
            {
                INow++;
                Doing = "Null";
                Target.transform.position = gameObject.transform.position;
            } else
            {
                if (Vector3.Distance(transform.position, Target.transform.position) <= Geom.eps) {
                    if (Staying) {
                        if (Global.Time >= TTNowTime + TimeTable[INow].Actions[TTNow].len)
                        {
                            TTNow = (TTNow + 1) % TimeTable[INow].Actions.Count;
                            Target.transform.position = TimeTable[INow].Actions[TTNow].pos.position;
                            Staying = false;
                        }
                    } else
                    {
                        Staying = true;
                        TimeTable[INow].Actions[TTNow].ToUse.use();
                        TTNowTime = Global.Time;
                    }
                }
            }
        }
        if (Doing == "Null" || Doing == "Reading")
        {
            if (INow<TimeTable.Count && TimeTable[INow].StartTime >= Global.Time)
            {
                TTNow = 0;
                Doing = "Timetable";
                Target.transform.position = TimeTable[INow].Actions[0].pos.position;
            }
            else {
                if (Hunger < me.NeedForFood)
                {
                    //go eat
                }
                else if (Alcohol < me.NeedForAlcohol)
                {
                    //go drink
                } else if(Happiness < me.MinHappinessNeeded)
                {
                    //go chill
                } else
                {
                    if (Doing == "Null")
                    {
                        int k = Global.rnd.Next(me.NeedForBooks + 1);
                        if (k == 0)
                        {
                            readAnim();
                            Doing = "Reading";
                        }
                        else
                        {
                            Doing = "Walking";
                            k = Global.rnd.Next(Global.ChillPoints.Count);
                            Target.transform.position = Global.ChillPoints[k].position;
                        }
                    }
                }
            }
        }
        if (Doing == "Walking")
        {
            if(Vector3.Distance(transform.position, Target.transform.position) <= Geom.eps)
            {
                Doing = "Null";
            }
        }
    }
    void readAnim() {
    
    }
}
