using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{

    GameObject player;
    NavMeshAgent agent;
    Health health;
    Health playerHealth;
    Damageable damageable;

    public AudioClip[] punchSound;
    public AudioClip[] ExplosionSound;
    public GameObject StunnedStar;
    public ParticleSystem Hearts;

    public GameObject Coin;
    public GameObject Explosion;
    public GameObject HeartMissile;
    [SerializeField] Transform _missileSpawnPoint;
    public GameObject[] BurnMarks;

    public float MinHurtDistance = 5f;
    public float hitInterval = 5f;
    public float hitIntervalTimer = 0f;

    private bool _isStunned = false;
    private float _stunnedTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = ObjectManager.GetObjectsOfType<Player>()[0];
        playerHealth = player.GetComponent<Player>().GetHealth();
        playerHealth.OnDeath += OnPlayerDeath;

        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        health.OnHurt += OnHurt;
        health.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        playerHealth.OnDeath -= OnPlayerDeath;
        health.OnHurt -= OnHurt;
        health.OnDeath -= OnDeath;
    }


    private void OnPlayerDeath()
    {
        if(agent.isOnNavMesh){
            agent.isStopped = true;
        }
    }

    public void Setup(Vector3 position, Quaternion rotation, int hp)
    {
        health = GetComponent<Health>();
        health.MaxHp = hp;
        health.Hp = hp;

        transform.position = position;
        transform.rotation = rotation;
        StopStun();

        gameObject.SetActive(true);
    }


    private void OnHurt()
    {
        AudioSource.PlayClipAtPoint(punchSound[UnityEngine.Random.Range(0, punchSound.Length)], transform.position, 1f);
    }

    private void OnDeath()
    {
        GameObject coin = PoolManager.GetPoolObject(Coin);
        coin.GetComponent<Coin>().Setup(transform.position, Quaternion.identity);

        StencilSpawner.SpawnStencil("BurnMarks", BurnMarks[UnityEngine.Random.Range(0,(BurnMarks.Length-1))], 1.5f, new Vector3(transform.position.x, 0.1f, transform.position.z),
            Quaternion.Euler(90, 0, UnityEngine.Random.Range(0f, 360f)));
        GameObject explosion = PoolManager.GetPoolObject(Explosion);
        explosion.GetComponent<DestroyVFX>().Setup(transform.position, Quaternion.identity);

        float shakeValue = 1.0f - (Vector3.Distance(transform.position, player.transform.position) / 15f);
        GameManager.Instance.CameraObject.GetComponent<CameraShake>().AddTrauma(shakeValue);
        AudioSource.PlayClipAtPoint(ExplosionSound[UnityEngine.Random.Range(0, ExplosionSound.Length)], transform.position, 1f);
        StopStun();

        gameObject.SetActive(false);
    }

    void OnHit()
    {
    }

    private void Update()
    {

        HandleAttack();

        if (_isStunned) {
            _stunnedTimer += Time.deltaTime;
            if (_stunnedTimer >= 0.5f) {
                StopStun();
            }
        }
    }

    private void HandleAttack()
    {
        hitIntervalTimer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance > MinHurtDistance){
            if (Hearts.isPlaying) {
                Hearts.Stop();
            }

            return;
        }


        Vector3 dir = player.transform.position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,desiredRotation, Mathf.Deg2Rad * 45);

        if (hitIntervalTimer >= hitInterval && !_isStunned){
            
            GameObject missile = PoolManager.GetPoolObject(HeartMissile);
            missile.GetComponent<HeartMissile>().Setup(_missileSpawnPoint.position, transform.rotation, player.transform);

            hitIntervalTimer = 0;
        }

        if (!_isStunned && !Hearts.isPlaying) {
            Hearts.Play();
        }
    }

    public void Stun()
    {
        if (_isStunned) {
            return;
        }

        _isStunned = true;
        if (agent != null && agent.isOnNavMesh) {
            agent.updateRotation = false;
            agent.isStopped = true;
        }
        StunnedStar.SetActive(true);
        _stunnedTimer = 0;

    }

    public void StopStun()
    {
        if (agent != null && agent.isOnNavMesh) {
            agent.isStopped = false;
        }
        if (agent != null)
            agent.updateRotation = true;
        _isStunned = false;
        StunnedStar.SetActive(false);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        agent.SetDestination(player.transform.position);
    }
}
