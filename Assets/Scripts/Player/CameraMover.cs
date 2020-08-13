using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    Transform plr;
    // Start is called before the first frame update
    void Start()
    {
        plr = GameObject.Find("Player").transform;
        gameObject.transform.position = new Vector3(plr.position.x, plr.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(plr.position.x, plr.position.y, -10);
    }
}
