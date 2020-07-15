using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JoyController
{

    public static SortedDictionary<string, Vector2> AllJoy = new SortedDictionary<string, Vector2>();
    public static void addJoy(string s)
    {
        updJoy(s, 0, 0);
    }
    public static void updJoy(string s, float x, float y)
    {
        AllJoy[s] = new Vector2(x, y);
    }
}
