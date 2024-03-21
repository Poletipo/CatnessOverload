using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : Unit
{

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

    [SerializeField] Health _health;
    [SerializeField] InputManager _inputManager;


    public Health GetHealth()
    {
        return _health;
    }

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
        _health.OnDeath += OnDeath;
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
                _health.Heal(1);
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
