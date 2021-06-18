using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GlobalUpdater : MonoBehaviour
{
    public Sprite Empty;
    //public GameEvent ev;
    public List<Item> items;
    public bool update;
    
    private void init()
    {
        Global.Empty = Empty;
        Global.AllItems = items;
        GameObject[] temp = GameObject.FindGameObjectsWithTag("ChillPoint");
        Global.Canvas = GameObject.Find("Canvas");
        foreach (GameObject g in temp)
        {
            Global.ChillPoints.Add(g.transform);
        }
    }

    void Awake()
    {
        GameObject.FindGameObjectsWithTag
        if(Application.isPlaying)
            init();
    }



    void Update()
    {
        if (update)
        {

        }
        if (Application.isPlaying)
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
                foreach (Contain c in Global.Opened)
                {
                    c.close();
                }
                Global.Opened.Clear();
            }
            if (Input.GetMouseButtonUp(0))
            {
                Global.DroppedNow = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (Application.isPlaying)
        {
            if (Global.DroppedNow && !Global.PickedNow)
            {
                if (Drag.now!=-1)
                {
                    GameObject g = Instantiate(Global.AllItems[Drag.now].Instance);
                    g.transform.position = Player._p.transform.position;
                    Vector3 vel = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Player._p.transform.position;
                    Rigidbody2D rb1 = g.GetComponent<Rigidbody2D>();
                    rb1.velocity = vel * rb1.drag;
                    Drag.now = -1;
                }
            }
            Global.DroppedNow = false;
            Global.PickedNow = false;
        }
    }
}
