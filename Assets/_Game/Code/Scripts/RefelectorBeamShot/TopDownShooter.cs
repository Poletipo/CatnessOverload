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

    private bool isController = false;

    private bool _isDead;

    public bool IsDead {
        get { return _isDead; }
        set {
            _isDead = value;
        }
    }

    public bool InputEnabled = true;

    public GameObject AimFocus;
    public GameObject MoveFocus;

    public Transform Wheels;

    public GameObject Explosion;
    public ParticleSystem Smoke;

    private Vector3 _controllerDir;
    private Vector3 _moveDirection;

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
        AimFocus.transform.position = transform.position;
        MoveFocus.transform.position = transform.position;

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
        if (!IsDead && InputEnabled) {
            Move();
            Look();

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

    private void Move()
    {

        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.z = Input.GetAxisRaw("Vertical");
        _moveDirection.Normalize();

        if (_moveDirection.magnitude > 0) {
            Wheels.rotation = Quaternion.Lerp(Wheels.rotation, Quaternion.LookRotation(_moveDirection, Vector3.up), 10 * Time.deltaTime);
        }

        MoveFocus.transform.position = Vector3.Lerp(MoveFocus.transform.position, (transform.position + _moveDirection * 5f), 8f * Time.deltaTime);
    }

    private void Look()
    {

        _controllerDir.x = -Input.GetAxisRaw("RightStickX");
        _controllerDir.y = Input.GetAxisRaw("RightStickY");

        if (_controllerDir.magnitude > 0) {
            isController = true;
        }
        else if (Input.GetAxisRaw("Mouse X") > 0 || Input.GetAxisRaw("Mouse Y") > 0) {
            isController = false;
        }

        AimFocus.transform.position = transform.position + _controllerDir.normalized * _controllerDir.magnitude;

        if (isController && _controllerDir.magnitude > 0) {
            Vector3 mousePosition = _controllerDir;


            float lookAtAngle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(lookAtAngle - 90, Vector3.up);
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
