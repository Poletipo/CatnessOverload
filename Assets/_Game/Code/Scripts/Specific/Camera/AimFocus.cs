using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimFocus : MonoBehaviour
{

    [SerializeField] LookToward _lookToward;




    // Start is called before the first frame update
    void Start()
    {
        
        transform.SetLocalPositionAndRotation(new Vector3(0,0,10),
         Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = _lookToward.GetLookAtRotation();
    }
}
