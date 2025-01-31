using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Damageable))]

public class Destructible : MonoBehaviour {

    public GameObject DestroyedObject;
    private Health _health;
    private Damageable _hitable;

    // Start is called before the first frame update
    void Start()
    {
        _hitable = GetComponent<Damageable>();
        _hitable.OnHit = OnHit;
        _health = GetComponent<Health>();
        _health.OnDeath = OnDeath;
    }

    private void OnHit(int damage)
    {
        _health.Hurt(damage);
    }

    private void OnDeath()
    {
        if (DestroyedObject != null) {
            Instantiate(DestroyedObject, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

}
