using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{

    public Transform[] targets;

    [Range(0, 10)]
    public float smoothSpeed = 0.1f;

    private Vector3 _desiredPosition = new Vector3();
    private Vector3 _smoothPosition = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _desiredPosition = Vector3.zero;
        for (int i = 0; i < targets.Length; i++)
        {
            _desiredPosition += targets[i].position;
        }

        _desiredPosition /= targets.Length;


        _smoothPosition = (transform.position * (1 - smoothSpeed * Time.deltaTime)) + 
            (_desiredPosition * smoothSpeed * Time.deltaTime);

        transform.position = _smoothPosition;
    }
}
