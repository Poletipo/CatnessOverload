using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public Action<Collider> OnTrigger;

    void OnTriggerEnter(Collider other){
        OnTrigger?.Invoke(other);
    }
    
}
