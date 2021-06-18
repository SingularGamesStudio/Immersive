using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Contain : MonoBehaviour
{
    public bool square = true;
    public Vector2Int size;
    public bool HasBooks;
    public bool HasFood;
    public List<int> Items = new List<int>();
    List<Slot> Slots = new List<Slot>();
    int[,] num;
    public GameObject Ui;
    GameObject UiInst;
    // Start is called before the first frame update
    void Start()
    {
        if (square)
        {
            num = new int[size.x, size.y];
        }
        while (Items.Count < size.x * size.y)
        {
            Items.Add(-1) ;
        }
    }
    public void open()
    {
        
        if (UiInst == null)
        {
            
            UiInst = Instantiate(Ui);
            UiInst.transform.SetParent(Global.Canvas.transform, false);
            Global.Opened.Add(this);
            Transform temp = UiInst.transform.GetChild(0);
            for (int i = 0; i < temp.childCount; i++)
            {
                Slots.Add(temp.GetChild(i).gameObject.GetComponent<Slot>());
                Slots[Slots.Count - 1].parent = this;
                Slots[Slots.Count - 1].num = Slots.Count - 1;
                Slots[Slots.Count - 1].updateContents();
            }
        }
    }
    public void close()
    {
        Slots.Clear();
        Destroy(UiInst);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
