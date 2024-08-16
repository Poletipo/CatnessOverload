using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class HeartMissile : MonoBehaviour
{

    [SerializeField] Transform _target = null;
    [SerializeField] float _maxTurnAngle = 45;
    [SerializeField] float _minTurnAngle = 0;
    [SerializeField] float _duration = 1;

    [SerializeField] float _startSpeed = 5;
    [SerializeField] float _endSpeed = 8;
    [SerializeField] GameObject HitExplosion;

    public LayerMask targetedMask;
    public LayerMask wallMask;

    [SerializeField] Trigger _trigger;

    [SerializeField] int _damage = 1;


    Quaternion _desiredRotation;
    private float _startTime = 0;

    float _turnAngle = 0;
    float _speed = 1;


    // Start is called before the first frame update
    void Start()
    {
        _trigger.OnTrigger += OnColliderTrigger;
    }

    private void OnColliderTrigger(Collider collider)
    {
        Damageable damageable;

        if( Utilities.IsInLayerMask(collider.gameObject, targetedMask)){
            if(collider.TryGetComponent<Damageable>(out damageable)){

                damageable.Hit(_damage);

                GameObject explosion = PoolManager.GetPoolObject(HitExplosion);
                explosion.GetComponent<DestroyVFX>().Setup(transform.position, Quaternion.identity);

                SetupEnd();
            }
        }
        else if(Utilities.IsInLayerMask(collider.gameObject, wallMask)){
            GameObject explosion = PoolManager.GetPoolObject(HitExplosion);
            explosion.GetComponent<DestroyVFX>().Setup(transform.position, Quaternion.identity);
            SetupEnd();
        }

        
    }

    private void SetupEnd()
    {
        gameObject.SetActive(false);
    }

    public void Setup(Vector3 position, Quaternion rotation, Transform target){

        transform.position = position;
        transform.rotation = rotation;
        _target = target;

        _startTime = Time.time;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if(_target== null){
            return;
        }
        
        Vector3 direction = _target.position - transform.position;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        float t = (Time.time - _startTime)/_duration;
        _turnAngle = Mathf.Lerp(_maxTurnAngle, _minTurnAngle,  t);
        _speed = Mathf.Lerp(_startSpeed, _endSpeed, t);

        _desiredRotation = Quaternion.AngleAxis(angle-90,Vector3.down);
    
        Quaternion rot = Quaternion.RotateTowards(transform.rotation, _desiredRotation, _turnAngle * Time.deltaTime);

        transform.rotation = rot;
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

}
