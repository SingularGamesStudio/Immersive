using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyBehaviour : MonoBehaviour
{
    public string JoyName;
    int IsMoving = -1;
    float r;
    public bool Cancelable = false;
    public float CancelR = 60;
    public RectTransform RectT;
    public RectTransform Knob;
    // Start is called before the first frame update
    void Start()
    {
        JoyController.addJoy(JoyName);
        RectT = gameObject.GetComponent<RectTransform>();
        r = RectT.sizeDelta.x / 2;
        Knob = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ok = false;
#if UNITY_EDITOR
        ok = true;
        Vector3 mPos = Input.mousePosition;
        mPos -= new Vector3(Screen.width, Screen.height) / 2f;
        Vector3 RV = mPos - new Vector3(RectT.anchoredPosition.x, RectT.anchoredPosition.y);
        float dist = RV.magnitude;
        if (IsMoving == -1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (dist <= r)
                {
                    IsMoving = 1;
                }
            }
        }
        if(IsMoving==1)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (Cancelable)
                {
                    if(dist <= CancelR)
                    {
                        JoyController.updJoy(JoyName, 0, 0);
                    } else
                        JoyController.updJoy(JoyName, 2, 2);
                } else
                    JoyController.updJoy(JoyName, 2, 2);
                IsMoving = -1;
            } else
            {
                Vector3 now = RV / r;
                if(now.magnitude>1)
                    now.Normalize();
                JoyController.updJoy(JoyName, now.x, now.y);
            }
            if(JoyController.AllJoy[JoyName].magnitude>1.2f)
                Knob.anchoredPosition = new Vector2(0, 0);
            else Knob.anchoredPosition = JoyController.AllJoy[JoyName] * r;
        }

#elif UNITY_STANDALONE
        ok = true;
        //при компиляции скопировать из UNITYEDITOR
#endif
        if (!ok) {
            //джойстик под андроид
        }
    }
}
