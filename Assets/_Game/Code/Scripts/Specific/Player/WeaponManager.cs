using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] Firearm _firearm;


    public Firearm GetWeapon()
    {
        return _firearm;
    }


    public void ActivatePrincipalAction()
    {
        _firearm.ActivatePrincipalAction();
    }

    public void DeactivatePrincipalAction()
    {
        _firearm.DeactivatePrincipalAction();
    }

}
