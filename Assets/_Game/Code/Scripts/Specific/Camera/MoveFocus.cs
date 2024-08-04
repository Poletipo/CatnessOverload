using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFocus : MonoBehaviour
{


    [SerializeField] MovementController _mc;
    [SerializeField] Transform referenceTransform;

    private Vector3 direction = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = _mc.MoveInput.x;
        direction.z = _mc.MoveInput.y;

        Vector3 targetPosition = referenceTransform.position + direction * 4f;
        transform.position = Vector3.Lerp(transform.position, 
            targetPosition, 8f * Time.deltaTime);
    }
}
