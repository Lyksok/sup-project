using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    public abstract class RangedWeapon : Weapon
    {
        //[Header("Shooter ammo")]
        //public GameObject projectile;
        
        public GameObject anchor;
        public abstract int? Ammo { get; } // Number of ammunition, null=> unlimited
    }
}
