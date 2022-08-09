using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    protected Transform playerTransform;
    protected Vector3 destinationPos;
    protected GameObject[] wandarPoints;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();   
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    protected virtual void Initialize()
    {

    }

    protected virtual void FSMUpdate()
    {

    }
}
