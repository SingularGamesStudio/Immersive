using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class AStar : MonoBehaviour
{
    public bool upd;
    bool[,] a;
    public Transform ldPoint;
    public Transform ruPoint;
    public Vector2Int sz;
    bool upd_done = false;
    public float cell = 1;
    System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Awake()
    {
        upd_done = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (upd) {
            upd = false;
            upd_done = true;
            sz = new Vector2Int((int)((ruPoint.position.x - ldPoint.position.x) / cell) + 1, (int)((ruPoint.position.y - ldPoint.position.y) / cell) + 1);
            a = new bool[sz.x, sz.y];
            for (int i = 0; i < sz.x; i++) {
                for (int j = 0; j < sz.y; j++) {
                    a[i, j] = true;
                }
            }
            Physics2D.queriesHitTriggers = false;
            for (int i = 0; i < sz.x; i++) {
                for (int j = 0; j < sz.y; j++) {
                    for (int di = 0; di <= 2; di++) {
                        for (int dj = 0; dj <= 2; dj++) {
                            RaycastHit2D hit = Physics2D.Raycast(ldPoint.position + new Vector3(i * cell + di*cell / 2, j * cell + dj*cell / 2), Vector2.zero);
                            if (hit.collider != null && hit.collider.gameObject.tag!="Object")
                                a[i, j] = false;
                        }
                    }
                }
            }
            Physics2D.queriesHitTriggers = true;
        }
        if (upd_done) {
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

    void Calc(int x1, int y1, int x2, int y2) {
    
    }
}
