using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour {

    [SerializeField] MovementController _mc;

    [SerializeField] float _rotationSpeed = 30;
    [SerializeField] float _rotationOffset = 90;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (_mc.MoveInput != Vector2.zero) {
            float desiredAngle = Mathf.Atan2(_mc.MoveInput.y, -_mc.MoveInput.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.AngleAxis(desiredAngle - _rotationOffset, Vector3.up), _rotationSpeed * Time.deltaTime);

            transform.rotation = rotation;
        }


    }
}
