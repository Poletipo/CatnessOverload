using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBouncePowerup : Shop
{

    public override void Upgrade()
    {
        Firearm firearm = _player.WeaponManager.GetWeapon();

        firearm.ReboundCount += 1;
        Cost = (int)(Cost * CostIncreaseMultiplier);
    }
}
