using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TopDownShooter : MonoBehaviour {

    public float healthInterval = 1;
    public float healthIntervalTimer = 1;

    private bool _isDead;

    public bool IsDead {
        get { return _isDead; }
        set {
            _isDead = value;
        }
    }


    public GameObject Explosion;
    public ParticleSystem Smoke;

    private Health health;

    private bool _isSmoking = false;

    public Action OnMoneyChanged;

    [Header("Money")]
    [SerializeField]
    private int _moneyAmount = 100;

    public int MoneyAmount {
        get { return _moneyAmount; }
        set {
            _moneyAmount = Mathf.Clamp(value, 0, 1000000);
            OnMoneyChanged?.Invoke();
        }
    }

    // TEST 
    public CameraShake _cameraShake;


    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath += OnDeath;
        health.OnHpChanged += OnHpChanged;
    }

    private void OnHpChanged()
    {
        float hpPercent = 1 - (float)health.Hp / health.MaxHp;

        if (hpPercent >= 0.54f && !_isSmoking) {
            _isSmoking = true;
            Smoke.Play();
        }
        else if (hpPercent < 0.54f && _isSmoking) {
            Smoke.Stop();
            _isSmoking = false;
        }

    }

    private async void OnDeath()
    {
        IsDead = true;

        GetComponent<Animation>().Play();

        float end = Time.time + 1;

        while (Time.time < end) {
            await Task.Yield();
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        GameManager.Instance.CameraObject.GetComponent<CameraShake>().AddTrauma(1);
    }

    void Update()
    {
        if (!IsDead) {

            healthIntervalTimer += Time.deltaTime;

            if (healthIntervalTimer > healthInterval) {
                health.Heal(1);
                healthIntervalTimer = 0;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.R) && IsDead) {
                SceneManager.LoadScene(0);
            }
        }

    }

    public bool Pay(int cost)
    {
        if (cost <= MoneyAmount) {
            MoneyAmount -= cost;
            return true;
        }
        return false;
    }


}
