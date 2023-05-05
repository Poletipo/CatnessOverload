using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToward : MonoBehaviour {

    [SerializeField] Transform _transform;
    [SerializeField] float _angleOffset = 0;
    [SerializeField] float _rotationSpeed;
    private Plane _plane = new Plane(Vector3.up, 0);


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LookAtDirection(Vector2 direction)
    {
        float desiredAngle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;



        Quaternion rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.AngleAxis(desiredAngle, Vector3.up), _rotationSpeed);


        _transform.rotation = rotation;// Quaternion.AngleAxis(desiredAngle - 90, Vector3.up);
    }



    public void LookAtMouse(Vector2 mousePosition)
    {
        Vector3 worldMousePosition;
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (_plane.Raycast(ray, out distance)) {
            worldMousePosition = ray.GetPoint(distance);

            Vector3 localMousePosition = worldMousePosition - transform.position;

            float lookAtAngle = Mathf.Atan2(localMousePosition.z, localMousePosition.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.AngleAxis(lookAtAngle - _angleOffset, Vector3.down);
        }
    }


}
