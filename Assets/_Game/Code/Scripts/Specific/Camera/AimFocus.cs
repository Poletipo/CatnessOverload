using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimFocus : MonoBehaviour
{

    [SerializeField] LookToward _lookToward;
    [SerializeField] Transform followObject;

    private Vector3 dir = new Vector3();

    float _distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = _lookToward.GetLookAtRotation() * transform.forward;

        if(InputManager.LookAtControl == InputManager.LookAtControls.Mouse){
            
            _distance = Mathf.Clamp(_lookToward.GetMouseDistance(),0,2);

        }
        else{
            _distance = _lookToward.GetJoystickMagnitude()*2;
        }
            transform.position = followObject.position + dir*_distance;

    }
}
