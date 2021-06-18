using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParams : MonoBehaviour
{
    public Color SkinColor;
    public List<SpriteRenderer> SkinObjects = new List<SpriteRenderer>();
    public GameObject Weapon;
    public Contain Inventory;

    [Header("Stats")]
    public int hp;
}
