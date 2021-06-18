﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameEvent
{
    public EventType type;
    public enum EventType
    {
        get,
        lose
    }
    List<int> targets;
}
