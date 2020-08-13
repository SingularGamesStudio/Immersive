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
        if (parent.Items[num].IsNull)
        {
            img.sprite = Global.Empty;
        } else img.sprite = parent.Items[num].sp;
    }
    public void startDrag() {
        if (!parent.Items[num].IsNull)
        {
            Drag.now = parent.Items[num];
            img.sprite = Global.Empty;
            parent.Items[num] = Global.NullItem;
        }
    }
    public void endDrag()
    {
        if (!Drag.now.IsNull)
        {
            parent.Items[num] = Drag.now;
            img.sprite = parent.Items[num].sp;
            Drag.now = Global.NullItem;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mPos = Input.mousePosition;
            if (parent.Items[num].IsNull && RectTransformUtility.RectangleContainsScreenPoint(RectT, mPos))
            {
                GlobalUpdater._g.PickedNow = true;
                endDrag();
            }
        }
    }
}
