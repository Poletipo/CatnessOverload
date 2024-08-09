using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMissile : MonoBehaviour
{

    [SerializeField] Transform _target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction = _target.position - transform.position;

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);

        transform.rotation = Quaternion.AngleAxis(angle-90,Vector3.down);

    }
}
