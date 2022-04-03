using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //public Orientor orientor;
    //public Movement movement;
    //public UnitAI unitAI;

    // Start is called before the first frame update
    void Awake()
    {
        //orientor = transform.GetComponent<Orientor>();
        //movement = transform.GetComponent<Movement>();
        //unitAI = transform.GetComponent<UnitAI>();
    }

    private void Start()
    {
        //EntityMgr.instance.entities.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}