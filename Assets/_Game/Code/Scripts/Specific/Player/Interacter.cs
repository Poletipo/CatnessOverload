using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{

    Shop _currentShop = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Interact()
    {
        if (_currentShop == null) {
            return false;
        }

        _currentShop.TryShopping();

        return true;
    }


    private void OnTriggerEnter(Collider other)
    {
        Shop shop;

        if (other.TryGetComponent<Shop>(out shop)) {
            _currentShop = shop;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Shop shop;

        if (other.TryGetComponent<Shop>(out shop)) {
            _currentShop = null;
        }
    }
}
