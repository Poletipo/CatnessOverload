using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWheels : MonoBehaviour
{

    [SerializeField] MovementController _mc;
    [SerializeField] MeshRenderer _mr;
    [SerializeField] float _speed = 1;

    Material _mat;


    // Start is called before the first frame update
    void Start()
    {
        _mat = _mr.material;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = _mc.MoveInput.magnitude * _speed;

        _mat.SetFloat("_Speed",speed);
    }
}
