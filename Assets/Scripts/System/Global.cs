using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static List<Transform> ChillPoints = new List<Transform>();
    public static List<DestObject> DestroyableObjects = new List<DestObject>();
    public static float TimeReal;
    public static int Time;
    public static int Day;
    public static bool PickedNow = false;
    public static bool DroppedNow = false;
    public static Sprite Empty;
    public static GameObject Canvas;
    public const int DayLen = 60 * 40;
    public static System.Random rnd = new System.Random(42);
    public static List<Contain> Opened = new List<Contain>();
    public static float SpriteScaling = 20;
    public static List<Item> AllItems;
    public static Dictionary<int, GameObject> ID = new Dictionary<int, GameObject>();

    public static void add(int id, GameObject g)
    {
        if (ID[id] != null)
            Debug.LogError(g.name + " has same ID as " + ID[id].name);
        else ID[id] = g;
    }
}
