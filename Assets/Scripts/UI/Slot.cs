using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image img;
    public int num;
    public Contain parent;
    RectTransform RectT;
    // Start is called before the first frame update
    void Start()
    {
        RectT = gameObject.GetComponent<RectTransform>();
    }
    public void updateContents()
    {
        if (parent.Items[num]==-1)
        {
            img.sprite = Global.Empty;
        } else img.sprite = Global.AllItems[parent.Items[num]].sp;
    }
    public void startDrag() {
        if (parent.Items[num]!=-1)
        {
            Drag.now = parent.Items[num];
            img.sprite = Global.Empty;
            parent.Items[num] = -1;
        }
    }
    public void endDrag()
    {
        if (Drag.now!=-1)
        {
            parent.Items[num] = Drag.now;
            img.sprite = Global.AllItems[parent.Items[num]].sp;
            Drag.now = -1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mPos = Input.mousePosition;
            if (parent.Items[num]==-1 && RectTransformUtility.RectangleContainsScreenPoint(RectT, mPos))
            {
                Global.PickedNow = true;
                endDrag();
            }
        }
    }
}
