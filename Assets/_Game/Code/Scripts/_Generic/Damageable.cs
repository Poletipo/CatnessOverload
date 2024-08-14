using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

    public Action<int> OnHit;
    [SerializeField] Health health;

    public void Hit(int damage)
    {
        OnHit?.Invoke(damage);

        if(health!=null){
            health.Hurt(damage);
        }
        //Debug.Log(gameObject.name + " has been hit for " + damage + " damage");
    }
}
