using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDHolder : MonoBehaviour
{
    public long ID;
    public Vector2Int chunk;
    Vector3 UpdatedPos = new Vector3(0, 0, 0);
    Player pl;
    DestObject obj;
    NPC npc;

    void Start()
    {
        pl = gameObject.GetComponent<Player>();
        obj = gameObject.GetComponent<DestObject>();
        npc = gameObject.GetComponent<NPC>();
    }
    public Player GetPlayer()
    {
        return pl;
    }
    public NPC GetNPC()
    {
        if (pl != null)
            return pl;
        return npc;
    }
    public DestObject GetObject()
    {
        if (pl != null)
            return pl;
        if (npc != null)
            return npc;
        return obj;
    }

    void Update()
    {
        if ((transform.position - UpdatedPos).sqrMagnitude > 4)
        {
            UpdatedPos = transform.position;
            Vector2Int aPos = AStar._a.getLocalPos(transform.position);
            aPos = new Vector2Int(aPos.x / AStar._a.chunksize, aPos.y / AStar._a.chunksize);
            if (aPos != chunk)
            {
                AStar._a.chunk[chunk.x, chunk.y].Remove(ID);
                AStar._a.chunk[aPos.x, aPos.y].Add(ID);
                chunk = aPos;
                Debug.Log(chunk);
            }
        }
    }
}
