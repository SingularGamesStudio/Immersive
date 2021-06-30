using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameAction
{
    public List<GameRequirement> requirements = new List<GameRequirement>();
    public List<GameEvent> events = new List<GameEvent>();

    public bool check(long user)
    {
        foreach(GameRequirement g in requirements)
        {
            if (!g.check(user))
            {
                return false;
            }
        }
        return true;
    }
}
