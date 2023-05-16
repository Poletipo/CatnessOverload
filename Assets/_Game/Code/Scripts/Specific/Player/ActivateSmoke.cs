using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSmoke : MonoBehaviour {

    [Range(0, 1)]
    [SerializeField] float _threshold = 0.5f;
    [SerializeField] ParticleSystem _smokeParticles;
    [SerializeField] Health health;
    private bool _isSmoking = false;

    // Start is called before the first frame update
    void Start()
    {
        health.OnHpChanged += OnHpChanged;
    }

    private void OnHpChanged()
    {
        float hpPercent = 1 - (float)health.Hp / health.MaxHp;

        if (hpPercent >= _threshold && !_isSmoking) {
            _isSmoking = true;
            _smokeParticles.Play();
        }
        else if (hpPercent < _threshold && _isSmoking) {
            _smokeParticles.Stop();
            _isSmoking = false;
        }
    }
}
