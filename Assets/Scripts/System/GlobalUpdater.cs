using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobalUpdater : MonoBehaviour
{
    public Sprite Empty;
    //public GameEvent ev;
    public List<Item> items;
    bool upd = true;
    System.Random rnd = new System.Random();
    
    private void init()
    {
        if (upd)
        {
            upd = false;
            AStar._a.init();
            GameObject[] toadd = GameObject.FindGameObjectsWithTag("add");
            foreach (GameObject g in toadd)
            {
                if (g.name == "EventSystem")
                    continue;
                const int e9 = (int)1e9;
                long t = ((long)rnd.Next(e9)) * e9 + rnd.Next(e9);
                while (Global.ID.ContainsKey(t))
                {
                    t = ((long)rnd.Next(e9)) * e9 + rnd.Next(e9);
                }
                IDHolder sc = g.GetComponent<IDHolder>();
                sc.ID = t;
                Global.add(t, sc);
                Vector2Int aPos = AStar._a.getLocalPos(g.transform.position);
                AStar._a.chunk[aPos.x / AStar._a.chunksize, aPos.y / AStar._a.chunksize].Add(t);
                sc.chunk = new Vector2Int(aPos.x / AStar._a.chunksize, aPos.y / AStar._a.chunksize);
                g.tag = "Object";
            }
            Global.Empty = Empty;
            Global.AllItems = items;
            GameObject[] temp = GameObject.FindGameObjectsWithTag("ChillPoint");
            Global.Canvas = GameObject.Find("Canvas");
            foreach (GameObject g in temp)
            {
                Global.ChillPoints.Add(g.transform);
            }
        }
    }

    void Start()
    {
        init();
    }



    void Update()
    {
        GameObject[] toadd = GameObject.FindGameObjectsWithTag("add");
        foreach (GameObject g in toadd)
        {
            if (g.name == "EventSystem")
                continue;
            const int e9 = (int)1e9;
            long t = ((long)rnd.Next(e9)) * e9 + rnd.Next(e9);
            while (Global.ID.ContainsKey(t))
            {
                t = ((long)rnd.Next(e9)) * e9 + rnd.Next(e9);
            }
            IDHolder sc = g.GetComponent<IDHolder>();
            sc.ID = t;
            Global.add(t, sc);
            Vector2Int aPos = AStar._a.getLocalPos(g.transform.position);
            AStar._a.chunk[aPos.x / AStar._a.chunksize, aPos.y / AStar._a.chunksize].Add(t);
            sc.chunk = new Vector2Int(aPos.x / AStar._a.chunksize, aPos.y / AStar._a.chunksize);
            g.tag = "Object";
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
