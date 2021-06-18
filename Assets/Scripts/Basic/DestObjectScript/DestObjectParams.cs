using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestObjectParams : MonoBehaviour
{
    public int hp;
    public bool CanContain;
    public bool CanBeDamaged;
    public bool CanBeHauled;
    public bool CanBePicked;
    public bool CanBeUsed;
    public bool IsWorkbench;
    public int ItemNumber;
    [Tooltip("Where the player stays when hauling (Relatively to this)")]
    public GameObject PlayerPos;
}
