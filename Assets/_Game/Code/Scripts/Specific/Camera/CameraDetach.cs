using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetach : MonoBehaviour
{

    [SerializeField] GameObject _cameraToDetach;


    // Start is called before the first frame update
    void Start()
    {
        _cameraToDetach.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
