using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestObject : MonoBehaviour
{
    public int hp;
    public BoxCollider2D Inst;
    bool ToDestroy = false;
    Animation anim;
    public ParticleSystem PSystem;
    // Start is called before the first frame update
    void Start()
    {
        Global.DestroyableObjects.Add(this);
        anim = gameObject.GetComponent<Animation>();
    }
    public void dealDamage(int Damage)
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
