using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class ReflectorBullet : MonoBehaviour
{

    [Range(0, 90)]
    public float MaxAngleTargeting = 45f;
    [Range(0, 100)]
    public float TargetingRadius = 20f;
    [Range(0, 100)]
    public float Speed = 8f;
    [Range(0, 100)]
    public int MaxRebound = 10;
    [Range(0, 100)]
    public int TargetingPercent = 50;

    public float TimeToDie = 8f;

    private float _startTime = 0;




    [Header("Damage")]
    [Range(0, 100)]
    public int BaseBulletDamage = 2;
    private int _bulletDamage = 2;
    [Range(0, 10)]
    public int DamageMinus = 1;

    [Header("Vfx")]
    public GameObject ImpactVFX;

    [Header("Masks")]
    public LayerMask targetedMask;
    public LayerMask wallMask;

    private Vector3 _direction = Vector3.forward;
    private Vector3 previousPosition;
    private RaycastHit hitInfo;
    private int currentRebound = 0;

    public IEnumerator Setup(Vector3 position, Vector3 direction, int reboundCount)
    {
        _startTime = Time.time;
        _bulletDamage = BaseBulletDamage;
        transform.position = position;
        MaxRebound = reboundCount;
        currentRebound = 0;
        _direction = direction;
        previousPosition = position;
        gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        GetComponent<TrailRenderer>().time = 0.1f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (_startTime + TimeToDie < Time.time) {
            EndBullet();
            return;
        }


        MoveBullet();

    }


    private void MoveBullet()
    {
        transform.position += _direction * Speed * Time.deltaTime;

        if (Physics.Linecast(previousPosition, transform.position, out hitInfo, wallMask,
            QueryTriggerInteraction.Collide)) {
            transform.position = hitInfo.point;

            if (Utilities.IsInLayerMask(hitInfo.collider.gameObject, targetedMask)) {
                HitTarget(hitInfo.collider.gameObject);
            }

            ReflectBullet(hitInfo);
        }
        previousPosition = transform.position;
    }



    private void HitTarget(GameObject target)
    {
        Damageable hitable = target.GetComponent<Damageable>();

        if (hitable != null) {
            hitable.Hit(_bulletDamage);

            if (target.tag == "Enemy") {
                target.GetComponent<NavMeshAgent>().velocity = _direction * 5;
                //target.GetComponent<Health>().Hurt(_bulletDamage);
                target.GetComponent<Enemy>().Stun();
            }
        }

        EndBullet();
    }

    private void ReflectBullet(RaycastHit hit)
    {

        _direction = Vector3.Reflect(_direction, hit.normal).normalized;

        GameObject sparks = PoolManager.GetPoolObject(ImpactVFX);
        sparks.GetComponent<DestroyVFX>().Setup(hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));

        if (UnityEngine.Random.Range(0, 100) < TargetingPercent) {


            float dotAngle = Mathf.Cos(Mathf.Deg2Rad * MaxAngleTargeting);
            Collider[] colliders = Physics.OverlapSphere(transform.position, TargetingRadius);

            List<GameObject> inRangeEnemies = new List<GameObject>();
            for (int i = 0; i < colliders.Length; i++) {
                if (Utilities.IsInLayerMask(colliders[i].gameObject, targetedMask)) {
                    inRangeEnemies.Add(colliders[i].gameObject);
                }
            }

            if (inRangeEnemies.Count > 0) {

                int enemyIndex = UnityEngine.Random.Range(0, inRangeEnemies.Count);

                Vector3 enemyDirection = (inRangeEnemies[enemyIndex].transform.position - transform.position).normalized;

                if (Vector3.Dot(_direction, enemyDirection) >= dotAngle) {

                    if (Vector3.Dot(enemyDirection, hit.normal) > 0) {
                        _direction = enemyDirection;
                    }
                }
            }
        }

        currentRebound++;
        if (currentRebound >= MaxRebound) {
            EndBullet();
        }

        _bulletDamage = _bulletDamage - DamageMinus >= 1 ? _bulletDamage - DamageMinus : 1;

    }

    private void EndBullet()
    {
        GetComponent<TrailRenderer>().time = 0;
        gameObject.SetActive(false);
    }

}
