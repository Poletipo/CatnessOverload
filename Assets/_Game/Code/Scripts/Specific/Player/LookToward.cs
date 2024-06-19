using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToward : MonoBehaviour
{

    [SerializeField] float _angleOffset = 0;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _groundOffset = 0;
    private Plane _plane;
    private Quaternion _lookAtRotation;

    private Vector3 _worldMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        _plane = new Plane(Vector3.up, -_groundOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.LookAtControl == InputManager.LookAtControls.Mouse) {
            UpdateRotation();
        }
    }

    private void UpdateRotation()
    {
        Vector3 localMousePosition = _worldMousePosition - transform.position;

        float desiredAngle = Mathf.Atan2(localMousePosition.z, -localMousePosition.x) * Mathf.Rad2Deg;
        _lookAtRotation = Quaternion.AngleAxis(desiredAngle - _angleOffset, Vector3.up);
        //_lookAtRotation = Quaternion.RotateTowards(transform.rotation,
        //Quaternion.AngleAxis(desiredAngle - _angleOffset, Vector3.down), _rotationSpeed);
        transform.rotation = _lookAtRotation;
    }

    public void LookAtDirection(Vector2 direction)
    {
        float desiredAngle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
        _lookAtRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(desiredAngle - 90, Vector3.up), _rotationSpeed);
        transform.rotation = _lookAtRotation;// Quaternion.AngleAxis(desiredAngle - 90, Vector3.up);
    }



    public void LookAtMouse(Vector2 mousePosition)
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (_plane.Raycast(ray, out distance)) {
            _worldMousePosition = ray.GetPoint(distance);
        }
    }


}
