using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUpdater : MonoBehaviour
{
    public Sprite Empty;
    public static GlobalUpdater _g;
    bool DroppedNow;
    public bool PickedNow;
    public List<Item> AllItems;
    void Awake()
    {
        _g = this;
        Global.Empty = Empty;
        GameObject[] temp = GameObject.FindGameObjectsWithTag("ChillPoint");
        Global.Canvas = GameObject.Find("Canvas");
        foreach(GameObject g in temp)
        {
            Global.ChillPoints.Add(g.transform);
        }
    }
    void Update()
    {
        Global.TimeReal += Time.deltaTime;
        Global.Time = (int)Global.TimeReal;
        if (Global.Time >= Global.DayLen)
        {
            Global.Time -= Global.DayLen;
            Global.TimeReal -= Global.DayLen;
            Global.Day++;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach(Contain c in Global.Opened)
            {
                c.close();
            }
            Global.Opened.Clear();
        }
        if (Input.GetMouseButtonUp(0))
        {
            DroppedNow = true;
        }
    }
    void FixedUpdate()
    {
        if(DroppedNow && !PickedNow)
        {
            if (!Drag.now.IsNull)
            {
                GameObject g = Instantiate(Drag.now.Instance);
                g.transform.position = Player._p.transform.position;
                Vector3 Force = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-Player._p.transform.position).normalized*Player._p.ThrowForce;
                g.GetComponent<Rigidbody2D>().AddForce(Force);
                Drag.now = Global.NullItem;
            }
        }
        DroppedNow = false;
        PickedNow = false;
    }
}
