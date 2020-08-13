using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{
    public bool IsNull;
    public Sprite sp;
    public GameObject Instance;
    public Item(bool ok)
    {
        IsNull = !ok;
    }
}
