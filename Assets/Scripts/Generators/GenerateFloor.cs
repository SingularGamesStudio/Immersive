using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GenerateFloor : MonoBehaviour
{
    public int x;
    public int y;
    public GameObject Inst;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            GameObject parent = new GameObject();
            for(int i = -x/2; i<x/2; i++)
            {
                for (int j = -y / 2; j < y / 2; j++)
                {
                    GameObject g = Instantiate(Inst);
                    g.transform.position = new Vector3(i, j);
                    g.transform.parent = parent.transform;
                }
            }
            start = false;
        }
    }
}
