using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour {

    [SerializeField] Firearm _firearm;


    public void ActivatePrincipalAction()
    {
        _firearm.ActivatePrincipalAction();
    }

    public void DeactivatePrincipalAction()
    {
        _firearm.DeactivatePrincipalAction();
    }

}
