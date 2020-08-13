using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRes : MonoBehaviour
{
    public static GenerateRes _g;
    public List<Wall> Walls = new List<Wall>();
    public List<Floor> Floors = new List<Floor>();
    [System.Serializable]
    public class Wall
    {
        public GameObject lu;
        public GameObject ld;
        public GameObject rd;
        public GameObject ru;
        public GameObject ud;
        public GameObject lr;
        public GameObject lud;
        public GameObject rud;
        public GameObject ulr;
        public GameObject dlr;
        public GameObject all;
        public GameObject doord;
        public GameObject dooru;
    }
    [System.Serializable]
    public class Floor
    {
        public Sprite ru;
        public Sprite rd;
        public Sprite lu;
        public Sprite ld;
        public Sprite l;
        public Sprite r;
        public Sprite u;
        public Sprite d;
        public Sprite all;
    }
    void Awake()
    {
        _g = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
