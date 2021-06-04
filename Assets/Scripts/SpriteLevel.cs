using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLevel : MonoBehaviour
{
    public List<SpriteRenderer> sp;
    public SpriteLevel par;
    public int Type;
    public Transform LegPoint;
    [HideInInspector]
    public int level;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Type == 0)
        {
            level = 32759-((int)(LegPoint.position.y * Global.SpriteScaling))*5;
            if (level < 0)
                Debug.LogError("превышен вертикальный размер мира");
            else foreach(SpriteRenderer s in sp){
                if(s.gameObject.activeSelf)
                    s.sortingOrder = level;
			}
        } else if (Type == 1) {
            foreach (SpriteRenderer s in sp) {
                if (s.gameObject.activeSelf)
                    s.sortingOrder = par.level + 1;
            }
        } else if (Type == 2) {
            foreach (SpriteRenderer s in sp) {
                if (s.gameObject.activeSelf)
                    s.sortingOrder = par.level - 1;
            }
        }
    }
}
