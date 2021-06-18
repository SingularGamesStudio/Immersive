using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public static class CopyInspector
{
    public static void copy (UnityEngine.Object from, UnityEngine.Object to)
    {
        Type fromType = from.GetType();
        Type toType = to.GetType();
        FieldInfo[] vars = fromType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo inf in vars)
        {
            FieldInfo tmp = toType.GetField(inf.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (tmp != null)
            {
                if (inf.GetCustomAttribute<SubClassAttribute>() != null)
                {
                    FieldInfo subclass = toType.GetField(inf.GetCustomAttribute<SubClassAttribute>().name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    FieldInfo subfield = subclass.FieldType.GetField(inf.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    subfield.SetValue(subclass.GetValue(to), inf.GetValue(from));
                }
                else
                {
                    tmp.SetValue(to, inf.GetValue(from));
                }
            }
        }
    }
}
