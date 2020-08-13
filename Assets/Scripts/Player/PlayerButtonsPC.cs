using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonsPC : MonoBehaviour
{
    public Player pl;
    public float LongTouchLen;
    // Start is called before the first frame update
    void Start()
    {
        HoldingE = 0;
        HoldingF = 0;
    }
    float HoldingE;
    float HoldingF;
    // Update is called once per frame
    void Update()
    {
        HoldingF += Time.deltaTime;
        HoldingE += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            HoldingE = 0;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            HoldingF = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (pl.Holding != null)
            {
                pl.dropHauling();
            }
            else
            {
                if (HoldingE >= LongTouchLen)
                {
                    pl.findInteractObject();
                    //use item
                }
                else
                {
                    pl.findInteractObject();
                    if (pl.NowActions.CanContain)
                    {
                        pl.openActive();
                    }
                    else if (pl.NowActions.CanBeUsed)
                    {
                        //use
                    }
                    else if (pl.NowActions.IsWorkbench)
                    {
                        //work at
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (pl.Holding != null)
            {
                pl.dropHauling();
            }
            else
            {
                if (HoldingF >= LongTouchLen)
                {
                    pl.findInteractObject();
                    if (pl.NowActions.CanBeMoved)
                    {
                        pl.takeActive();
                    }
                }
                else
                {
                    pl.findInteractObject();
                    if (pl.NowActions.CanBePicked)
                    {
                        pl.pickActive();
                    }
                }
            }
        }
    }
}
