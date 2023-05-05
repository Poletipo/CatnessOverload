using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;

    private Vector2 _moveInput;

    public Vector2 MoveInput {
        get { return _moveInput; }
        set { _moveInput = value; }
    }

    Vector3 _direction = new Vector3();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = MoveInput.x;
        _direction.z = MoveInput.y;


        _rb.velocity = _direction * _speed;
    }
}
