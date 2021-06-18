using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class SubClassAttribute : Attribute
{
    public string name { get; set; }
    public SubClassAttribute()
    {

    }
    public SubClassAttribute(string nm)
    {
        name = nm;
    }
}
