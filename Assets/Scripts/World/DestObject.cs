using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestObject : MonoBehaviour
{
    public _Params Object_Params;

    [HideInInspector]
    public int hp;
    [HideInInspector]
    public BoxCollider2D Inst;
    [HideInInspector]
    public bool CanContain;
    [HideInInspector]
    public bool CanBeDamaged;
    [HideInInspector]
    public bool CanBeMoved;
    [HideInInspector]
    public bool CanBePicked;
    [HideInInspector]
    public bool CanBeUsed;
    [HideInInspector]
    public bool IsWorkbench;
    [HideInInspector]
    public int ThisItem;
    [HideInInspector]
    public GameObject PlayerPos;
    bool ToDestroy = false;
    Animation anim;
    public ParticleSystem PSystem;

    [System.Serializable]
    public class _Params
    {
        public int HP;
        public BoxCollider2D Trigger;
        public bool CanContain;
        public bool CanBeDamaged;
        public bool CanBeMoved;
        public bool CanBePicked;
        public bool CanBeUsed;
        public bool IsWorkbench;
        public int ThisItem;
        public GameObject PlayerPos;
    };

    // Start is called before the first frame update
    void Start()
    {
        Global.DestroyableObjects.Add(this);
        anim = gameObject.GetComponent<Animation>();
    }
    public void dealDamage(int Damage)
    {
        if (CanBeDamaged)
        {
            hp -= Damage;
            if (hp <= 0)
            {
                die();
            }
            else
            {
                anim.Stop();
                anim.clip = anim.GetClip("Init");
                anim.Play();
                anim.PlayQueued("Dmg", QueueMode.CompleteOthers);
            }
        }
    }
    public void die()
    {
        ToDestroy = true;
    }
    public void playParticleSystem()
    {
        PSystem.Stop(true);
        PSystem.Play(true);
    }
    public void use()//use by npc
    {
        Debug.Log("Used "+gameObject.name);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (ToDestroy)
        {
            Global.DestroyableObjects.Remove(this);
            Destroy(gameObject);
        }
    }
}
