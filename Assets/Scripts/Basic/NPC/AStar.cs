using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class AStar : MonoBehaviour
{
    public static AStar _a;
    bool upd = true;
    bool[,] a;
    public bool showGrid;
    public Transform ldPoint;
    public Transform ruPoint;
    public Vector2Int sz;
    bool upd_done = false;
    public float cell = 1;
    public int chunksize = 10;
    public HashSet<long>[,] chunk;
    System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Awake()
    {
        _a = this;
        upd_done = false;
    }
    private void Start()
    {
        init();
    }
    public void init()
    {
        if (upd)
        {
            upd = false;
            upd_done = true;
            sz = new Vector2Int((int)((ruPoint.position.x - ldPoint.position.x) / cell) + 1, (int)((ruPoint.position.y - ldPoint.position.y) / cell) + 1);
            a = new bool[sz.x, sz.y];
            chunk = new HashSet<long>[sz.x / chunksize + 2, sz.y / chunksize + 2];
            for(int i = 0; i<sz.x/chunksize+1; i++)
            {
                for (int j = 0; j < sz.x / chunksize + 1; j++)
                {
                    chunk[i, j] = new HashSet<long>();
                }
            }
            for (int i = 0; i < sz.x; i++)
            {
                for (int j = 0; j < sz.y; j++)
                {
                    a[i, j] = true;
                }
            }
            Physics2D.queriesHitTriggers = false;
            for (int i = 0; i < sz.x; i++)
            {
                for (int j = 0; j < sz.y; j++)
                {
                    for (int di = 0; di <= 2; di++)
                    {
                        for (int dj = 0; dj <= 2; dj++)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(ldPoint.position + new Vector3(i * cell + di * cell / 2, j * cell + dj * cell / 2), Vector2.zero);
                            if (hit.collider != null && hit.collider.gameObject.tag != "Object")
                                a[i, j] = false;
                        }
                    }
                }
            }
            Physics2D.queriesHitTriggers = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (upd_done && showGrid) {
            for (int i = 0; i < sz.x; i++) {
                for (int j = 0; j < sz.y; j++) {
                    Vector3 v = ldPoint.position;
                    v.x += i * cell;
                    v.y += j * cell;
                    Debug.DrawLine(v, new Vector3(v.x + cell, v.y), Color.white);
                    Debug.DrawLine(v, new Vector3(v.x, v.y + cell), Color.white);
                    Debug.DrawLine(new Vector3(v.x + cell, v.y + cell), new Vector3(v.x + cell, v.y), Color.white);
                    Debug.DrawLine(new Vector3(v.x + cell, v.y + cell), new Vector3(v.x, v.y + cell), Color.white);
                    if (a[i, j]) {
                        Debug.DrawLine(v, new Vector3(v.x + cell, v.y + cell), Color.green);
                        Debug.DrawLine(new Vector3(v.x, v.y + cell), new Vector3(v.x + cell, v.y), Color.green);
                    } else {
                        Debug.DrawLine(v, new Vector3(v.x + cell, v.y + cell), Color.red);
                        Debug.DrawLine(new Vector3(v.x, v.y + cell), new Vector3(v.x + cell, v.y), Color.red);
                    }
                }
            }
        }
    }

    public Vector2Int getLocalPos(Vector3 pos)
    {
        pos = pos - ldPoint.position;
        if(pos.x<0 || pos.y < 0)
        {
            Debug.LogError("Position outside of Astar");
            return new Vector2Int(0, 0);
        }
        Vector2Int ans = new Vector2Int((int)(pos.x / cell), (int)(pos.y / cell));
        if(pos.x>=sz.x || pos.y >= sz.y)
        {
            Debug.LogError("Position outside of Astar");
            return new Vector2Int(0, 0);
        }
        return ans;
    }

    void Calc(int x1, int y1, int x2, int y2) {
    
    }
}
