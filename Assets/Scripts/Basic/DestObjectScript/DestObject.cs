using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DestObjectParams))]
[RequireComponent(typeof(IDHolder))]
public class DestObject : MonoBehaviour
{
    public int hp;
    public BoxCollider2D Inst;
    public bool CanContain;
    public bool CanBeDamaged;
    public bool CanBeHauled;
    public bool CanBePicked;
    public bool CanBeUsed;
    public bool IsWorkbench;
    public int ItemNumber;
    public List<GameAction> actions = new List<GameAction>();
    public Contain Inventory;
    public GameObject PlayerPos;
    bool ToDestroy = false;
    Animation anim;
    ParticleSystem PSystem;

    // Start is called before the first frame update
    
    void Start()
    {
        
        CopyInspector.copy(gameObject.GetComponent<DestObjectParams>(), this);
        Transform t = transform.Find("ParticleSystem");
        if (t != null)
            PSystem = t.gameObject.GetComponent<ParticleSystem>();
        t = transform.Find("SpriteHolder");
        if (t != null)
            Inst = t.gameObject.GetComponent<BoxCollider2D>();
        else Debug.LogError("DestObject has no child named SpriteHolder with BoxCollider(Trigger) attached to it");
        Global.DestroyableObjects.Add(this);
        Inventory = gameObject.GetComponent<Contain>();
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
