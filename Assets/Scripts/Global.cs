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
    public static Sprite Empty;
    public static GameObject Canvas;
    public const int DayLen = 60 * 40;
    public static Item NullItem = new Item(false);
    public static System.Random rnd = new System.Random(42);
    public static List<Contain> Opened = new List<Contain>();
    public static float SpriteScaling = 20;
    public static GameObject PathFinder;
}
