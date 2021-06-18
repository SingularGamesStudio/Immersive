using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player: NPC
{
    public static Player _p;

    /*Hidden Params*/
    float Speed = 2;
    float RotationSpeed = 0.1f;
    float AttackDist = 0.5f;
    float AttackAngle = 20;
    int Damage = 3;

    public DestObject NowActions;
    public DestObject Holding;
    GameObject Weapon = null;
    Color SkinColor;
    Rigidbody2D rb;
    List<SpriteRenderer> SkinObjects = new List<SpriteRenderer>();
    Animation Anim;
    Animation AttackAnim;

    GameObject Actions;
    float RotationNow;
    Vector2 TargetRotation = new Vector2(1, 0);
    Quaternion RotationR, RotationL;

    // Start is called before the first frame update
    void Start()
    {
        _p = this;
        CopyInspector.copy(gameObject.GetComponent<PlayerParams>(), this);
        //
        rb = gameObject.GetComponent<Rigidbody2D>();
        RotationR = Quaternion.identity;
        RotationL = Quaternion.AngleAxis(180, Vector3.up);
        Anim = gameObject.GetComponent<Animation>();
        AttackAnim = Weapon.transform.GetChild(0).GetComponent<Animation>();
        RotationNow = 0;
        SkinColor.a = 1;
        foreach (SpriteRenderer sp in SkinObjects) {
            sp.color = SkinColor;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 Move = Vector3.zero;
        //#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.W)) {
            Move += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            Move += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
            Move += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            Move += new Vector3(1, 0, 0);
        }
        //#endif
        if (Move != Vector3.zero) {
            if (!Anim.isPlaying) {
                Anim.clip = Anim.GetClip("Walk");
                Anim.Play();
            }
        } else {
            if (Anim.isPlaying && Anim.clip == Anim.GetClip("Walk")) {
                Anim.Stop();
                Anim.clip = Anim.GetClip("Idle");
                Anim.Play();
            }
        }
        Move.Normalize();
        rb.velocity = Move * Speed;
        //rb.MovePosition(transform.position + Move * Speed * Time.deltaTime);
        Vector2 AttackDir = JoyController.AllJoy["attack"];
        if (AttackDir.magnitude > 1.2f) {
            JoyController.updJoy("attack", 0, 0);
            if (AttackDir.x > 0) {
                attack();
            }
        } else if (AttackDir.magnitude != 0) {
            TargetRotation = AttackDir;
        }
        float NewAngle = Mathf.Atan2(TargetRotation.y, TargetRotation.x) - Mathf.PI / 2;
        if (NewAngle < 0)
            NewAngle += 2 * Mathf.PI;
        if (NewAngle >= Mathf.PI) {
            transform.rotation = RotationR;
            if (RotationNow < Mathf.PI) {
                RotationNow = Mathf.PI * 2 - RotationNow;
            }
        } else {
            transform.rotation = RotationL;
            if (RotationNow >= Mathf.PI) {
                RotationNow = Mathf.PI * 2 - RotationNow;
            }
        }
        float Delta = NewAngle - RotationNow;
        if (Delta < 0)
            Delta += 2 * Mathf.PI;
        if (Delta > 2 * Mathf.PI)
            Delta -= 2 * Mathf.PI;
        if (Delta > Mathf.PI)
            RotationNow = (Mathf.Max(Delta - 2 * Mathf.PI, -RotationSpeed) + RotationNow);
        else RotationNow = (Mathf.Min(Delta, RotationSpeed) + RotationNow);
        if (RotationNow > 2 * Mathf.PI)
            RotationNow -= 2 * Mathf.PI;
        if (RotationNow < 0)
            RotationNow += 2 * Mathf.PI;
        if (RotationNow < Mathf.PI) {
            Weapon.transform.rotation = Quaternion.AngleAxis(RotationNow * Mathf.Rad2Deg, Vector3.forward);
        } else {
            Weapon.transform.rotation = Quaternion.AngleAxis(RotationNow * Mathf.Rad2Deg, Vector3.forward) * RotationL;
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            Inventory.open();
        }
    }
    public void findInteractObject()
    {
        List<GameObject> ObjectsNearby = new List<GameObject>();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D coll in hitColliders) {
            if (coll.gameObject.tag == "Object") {
                ObjectsNearby.Add(coll.gameObject);
            }
        }
        if (ObjectsNearby.Count != 0) {
            GameObject Closest = ObjectsNearby[0];
            for (int i = 1; i < ObjectsNearby.Count; i++) {
                if ((Closest.transform.position - transform.position).magnitude > (ObjectsNearby[i].transform.position - transform.position).magnitude) {
                    Closest = ObjectsNearby[i];
                }
            }
            NowActions = Closest.GetComponent<DestObject>();
        } else NowActions = null;
    }
    public void openActive()
    {
        if (NowActions.CanContain) {
            NowActions.gameObject.GetComponent<Contain>().open();
        }
    }
    public void pickActive()
    {
        if (NowActions.CanBePicked) {
            int ok = -1; int temp = 0;
            foreach (int now in Inventory.Items) {
                if (now==-1) {
                    ok = temp;
                    break;
                }
                temp++;
            }
            if (ok >= 0) {
                Inventory.Items[ok] = NowActions.ItemNumber;
                Global.DestroyableObjects.Remove(NowActions);
                Destroy(NowActions.gameObject);
            }
        }
    }
    public void takeActive()
    {
        if (NowActions.CanBeHauled) {
            dropHauling();
            NowActions.gameObject.transform.parent = transform;
            NowActions.gameObject.transform.localPosition = -NowActions.PlayerPos.transform.localPosition;
            NowActions.PlayerPos.SetActive(true);
            NowActions.PlayerPos.transform.GetChild(0).GetComponent<SpriteRenderer>().color = SkinColor;
            NowActions.PlayerPos.transform.GetChild(1).GetComponent<SpriteRenderer>().color = SkinColor;
            Holding = NowActions;
        }
    }
    public void dropHauling()
    {
        if (Holding != null) {
            Holding.transform.SetParent(null);
            Holding.PlayerPos.SetActive(false);
            Holding = null;
        }
    }
    void attack()
    {
        if(!AttackAnim.isPlaying)
            AttackAnim.Play();
    }
    public void dealDamage()
    {
        foreach(DestObject o in Global.DestroyableObjects)
        {
            if (o.CanBeDamaged) {
                Vector2 dot1 = (Vector2)o.gameObject.transform.position + (o.Inst.offset - o.Inst.size / 2f) * o.transform.lossyScale;
                Vector2 dot2 = (Vector2)o.gameObject.transform.position + (o.Inst.offset + o.Inst.size / 2f) * o.transform.lossyScale;
                bool ok = false;
                if (Geom.intersectSegmentSector(dot1, new Vector2(dot1.x, dot2.y), Weapon.transform.position, AttackDist, RotationNow, AttackAngle * Mathf.Deg2Rad)) {
                    ok = true;
                } else
                if (Geom.intersectSegmentSector(dot1, new Vector2(dot2.x, dot1.y), Weapon.transform.position, AttackDist, RotationNow, AttackAngle * Mathf.Deg2Rad)) {
                    ok = true;
                } else if (Geom.intersectSegmentSector(dot2, new Vector2(dot1.x, dot2.y), Weapon.transform.position, AttackDist, RotationNow, AttackAngle * Mathf.Deg2Rad)) {
                    ok = true;
                } else if (Geom.intersectSegmentSector(dot2, new Vector2(dot2.x, dot1.y), Weapon.transform.position, AttackDist, RotationNow, AttackAngle * Mathf.Deg2Rad)) {
                    ok = true;
                }
                if (ok) {
                    o.dealDamage(Damage);
                }
            }
        }
    }
}
