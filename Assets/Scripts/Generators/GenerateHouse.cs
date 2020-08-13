using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class GenerateHouse : MonoBehaviour
{
    public int WallNum;
    public GameObject FloorBase;
    public List<int> Floors = new List<int>();
    public Texture2D sp;
    public int n;
    public int m;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            int[,] c = new int[n,m];
            bool[,] IsDoor = new bool[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    IsDoor[i, j] = false;
                }
            }
                    List<Vector2Int> Doors = new List<Vector2Int>();
            GameObject parent = new GameObject();
            Debug.Log(sp.GetPixel(0, 0));
            for(int i = 0; i<n; i++)
            {
                for(int j = 0; j<m; j++)
                {
                    if (sp.GetPixel(i, j) == new Color(1, 1, 1))
                    {
                        c[i, j] = 10;
                    }
                    else if (sp.GetPixel(i, j) == new Color(0, 0, 0))
                    {
                        c[i, j] = 10;
                        IsDoor[i, j] = true;
                        Doors.Add(new Vector2Int(i, j));
                    }
                    else if (sp.GetPixel(i, j) == new Color32(97, 222, 42, 255))
                    {
                        c[i, j] = 0;
                    }
                    else if (sp.GetPixel(i, j) == new Color32(158, 33, 213, 255))
                    {
                        c[i, j] = 1;
                    }
                    else if (sp.GetPixel(i, j) == new Color32(42, 222, 167, 255))
                    {
                        c[i, j] = 2;
                    }
                    else if (sp.GetPixel(i, j) == new Color32(222, 167, 42, 255))
                    {
                        c[i, j] = 3;
                    }
                    else if (sp.GetPixel(i, j) == new Color32(222, 42, 97, 255))
                    {
                        c[i, j] = 4;
                    }
                    else if (sp.GetPixel(i, j) == new Color32(39, 39, 207, 255))
                    {
                        c[i, j] = 5;
                    }
                    else if (sp.GetPixel(i, j) == new Color32(8, 8, 43, 255))
                    {
                        c[i, j] = 6;
                    }
                    else if (sp.GetPixel(i, j) == new Color32(128, 39, 39, 255))
                    {
                        c[i, j] = 7;
                    }
                    else c[i, j] = -1;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if(IsDoor[i, j])
                    {
                        if (c[i, j - 1] == 10)
                        {
                            GameObject g = Instantiate(GenerateRes._g.Walls[WallNum].doord);
                            g.transform.position = new Vector3(-i, j);
                            g.transform.parent = parent.transform;
                        }
                        bool up = false;
                        bool down = false;
                        if (c[i, j - 1] == 10)
                            down = true;
                        if (c[i, j + 1] == 10 || c[i, j + 1] == 11)
                            up = true;
                        if(up && down)
                        {
                            if (c[i + 1, j] != -1)
                            {
                                GameObject g1 = Instantiate(FloorBase);
                                g1.transform.position = new Vector3(-i, j);
                                g1.transform.parent = parent.transform;
                                g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j]].l;
                            }
                            if (c[i - 1, j] != -1)
                            {
                                GameObject g1 = Instantiate(FloorBase);
                                g1.transform.position = new Vector3(-i, j);
                                g1.transform.parent = parent.transform;
                                g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j]].r;
                            }
                        } else
                        {
                            if (c[i, j + 1] != -1)
                            {
                                GameObject g1 = Instantiate(FloorBase);
                                g1.transform.position = new Vector3(-i, j);
                                g1.transform.parent = parent.transform;
                                g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j + 1]].u;
                            }
                            if (c[i, j - 1] != -1)
                            {
                                GameObject g1 = Instantiate(FloorBase);
                                g1.transform.position = new Vector3(-i, j);
                                g1.transform.parent = parent.transform;
                                g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                            }
                        }
                    } else if(c[i, j] == 10)
                    {
                        bool up = false;
                        bool down = false;
                        bool right = false;
                        bool left = false;
                        if (c[i - 1, j] == 10 || c[i - 1, j] == 11)
                            right = true;
                        if (c[i + 1, j] == 10 || c[i + 1, j] == 11)
                            left = true;
                        if (c[i, j - 1] == 10)
                            down = true;
                        if (c[i, j + 1] == 10 || c[i, j + 1] == 11)
                            up = true;
                        GameObject g = null;
                        if (up)
                        {
                            if (down)
                            {
                                if (right)
                                {
                                    if (left)
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].all);
                                        if (c[i + 1, j + 1] != -1) {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j + 1]].lu;
                                        }
                                        if (c[i + 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j - 1]].ld;
                                        }
                                        if (c[i - 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j - 1]].rd;
                                        }
                                        if (c[i - 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j + 1]].ru;
                                        }
                                    } else
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].rud);
                                        if (c[i + 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j]].l;
                                        }
                                        if (c[i - 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j - 1]].rd;
                                        }
                                        if (c[i - 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j + 1]].ru;
                                        }
                                    }
                                } else
                                {
                                    if (left)
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].lud);
                                        if (c[i + 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j + 1]].lu;
                                        }
                                        if (c[i + 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j - 1]].ld;
                                        }
                                        if (c[i - 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j]].r;
                                        }
                                    }
                                    else
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].ud);
                                        if (c[i + 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j]].l;
                                        }
                                        if (c[i - 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j]].r;
                                        }
                                    }
                                }
                            } else
                            {
                                if (right)
                                {
                                    if (left)
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].ulr);
                                        if (c[i + 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j + 1]].lu;
                                        }
                                        if (c[i, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                                        }
                                        if (c[i - 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j + 1]].ru;
                                        }
                                    }
                                    else
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].ru);
                                        if (c[i + 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j]].l;
                                        }
                                        if (c[i + 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j - 1]].ld;
                                        }
                                        if (c[i, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                                        }
                                        if (c[i - 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j + 1]].ru;
                                        }
                                    }
                                }
                                else
                                {
                                    if (left)
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].lu);
                                        if (c[i + 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j + 1]].lu;
                                        }
                                        if (c[i, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                                        }
                                        if (c[i - 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j - 1]].rd;
                                        }
                                        if (c[i - 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j]].r;
                                        }
                                    }
                                    else
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].dooru);
                                        if (c[i + 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j]].l;
                                        }
                                        if (c[i + 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j - 1]].ld;
                                        }
                                        if (c[i, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                                        }
                                        if (c[i - 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j - 1]].rd;
                                        }
                                        if (c[i - 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j]].r;
                                        }
                                    }
                                }
                            }
                        } else
                        {
                            if (down)
                            {
                                if (right)
                                {
                                    if (left)
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].dlr);
                                        if (c[i, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j + 1]].u;
                                        }
                                        if (c[i + 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j - 1]].ld;
                                        }
                                        if (c[i - 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j - 1]].rd;
                                        }
                                    }
                                    else
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].rd);
                                        if (c[i + 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j + 1]].lu;
                                        }
                                        if (c[i + 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j]].l;
                                        }
                                        if (c[i - 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j - 1]].rd;
                                        }
                                        if (c[i, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j + 1]].u;
                                        }
                                    }
                                }
                                else
                                {
                                    if (left)
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].ld);
                                        if (c[i, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j + 1]].u;
                                        }
                                        if (c[i + 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j - 1]].ld;
                                        }
                                        if (c[i - 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j]].r;
                                        }
                                        if (c[i - 1, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j + 1]].ru;
                                        }
                                    }
                                    else
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].ud);
                                        if (c[i + 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j]].l;
                                        }
                                        if (c[i - 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j]].r;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (right)
                                {
                                    if (left)
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].lr);
                                        if (c[i, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j + 1]].u;
                                        }
                                        if (c[i, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                                        }
                                    }
                                    else
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].lr);
                                        if (c[i, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j + 1]].u;
                                        }
                                        if (c[i, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                                        }
                                    }
                                }
                                else
                                {
                                    if (left)
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].lr);
                                        if (c[i, j + 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j + 1]].u;
                                        }
                                        if (c[i, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                                        }
                                    }
                                    else
                                    {
                                        g = Instantiate(GenerateRes._g.Walls[WallNum].dooru);
                                        if (c[i + 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j]].l;
                                        }
                                        if (c[i + 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i + 1, j - 1]].ld;
                                        }
                                        if (c[i, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j - 1]].d;
                                        }
                                        if (c[i - 1, j - 1] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j - 1]].rd;
                                        }
                                        if (c[i - 1, j] != -1)
                                        {
                                            GameObject g1 = Instantiate(FloorBase);
                                            g1.transform.position = new Vector3(-i, j);
                                            g1.transform.parent = parent.transform;
                                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i - 1, j]].r;
                                        }
                                    }
                                }
                            }
                        }
                        g.transform.position = new Vector3(-i, j);
                        g.transform.parent = parent.transform;
                    } else
                    {
                        if (c[i, j] != -1)
                        {
                            GameObject g1 = Instantiate(FloorBase);
                            g1.transform.position = new Vector3(-i, j);
                            g1.transform.parent = parent.transform;
                            g1.GetComponent<SpriteRenderer>().sprite = GenerateRes._g.Floors[c[i, j]].all;
                        }
                    }
                }
            }
        }
    }
}
