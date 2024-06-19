using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCountPowerup : Shop
{

    public override void Upgrade()
    {
        Firearm firearm = _player.WeaponManager.GetWeapon();

        firearm.nbBulletPerShot += 1;
        firearm.maxAngleOffset += 5;
        Cost = (int)(Cost * CostIncreaseMultiplier);
    }
}
