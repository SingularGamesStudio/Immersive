using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLevel : MonoBehaviour
{
    SpriteRenderer sp;
    public SpriteRenderer spp;
    public int Type;
    public Transform LegPoint;
    // Start is called before the first frame update
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Type == 0)
        {
            sp.sortingOrder = 32759-((int)(LegPoint.position.y * Global.SpriteScaling))*5;
            if (sp.sortingOrder < 0)
                Debug.LogError("превышен вертикальный размер мира");
        }
    }

    void FixedUpdate()
    {
        if (Type == 1)
        {
            sp.sortingOrder = spp.sortingOrder + 1;
        } else if (Type == 2)
        {
            sp.sortingOrder = spp.sortingOrder - 1;
        }
    }
}
