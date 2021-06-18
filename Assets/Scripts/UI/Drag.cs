using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    public static int now;
    public GameObject Inst;
    public GameObject Frame;
    // Start is called before the first frame update
    void Start()
    {
        now = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (now!=-1)
        {
            if (Inst == null)
            {
                Inst = Instantiate(Frame);
                Inst.transform.SetParent(Global.Canvas.transform, false);
                Inst.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = Global.AllItems[now].sp;
            }
            Vector3 mPos = Input.mousePosition;
            //mPos -= new Vector3(Screen.width, Screen.height) / 2f;
            Inst.transform.position = mPos;
        } else
        {
            if (Inst != null)
            {
                Destroy(Inst);
            }
        }
    }

}
