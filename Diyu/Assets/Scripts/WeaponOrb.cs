using System;
using System.Security.Cryptography;
using Abilities;
using JetBrains.Annotations;
using Managers;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class WeaponOrb : Loot
    {
        public ResourceManager resources;
        private int _weaponId;
        private Rarities _rarity;
        public GameObject Sphere;
        
        public void Start()
        {
            _weaponId = RandomNumberGenerator.GetInt32(1, 8 + 1);
            _rarity = resources.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
            UpdateInfo();
            Sphere.gameObject.GetComponent<Renderer>().material.color = resources.GetRarityColor(_rarity);
        }
        
        public override void OnPickup(NewPlayer player)
        {
            //Debug.LogError("Picked up");
            Weapon replaced = player.PickupWeapon(player.resources.GetWeapon(_weaponId,_rarity,player));
            if (replaced is null)
            {
                Collected();
            }
            else
            {
                _weaponId = replaced.id;
                _rarity = replaced.Rarity;
            }
            UpdateInfo();

        }

        public override void UpdateInfo()
        {
            text.text = $"{resources.GetWeaponName(_weaponId)} - {_rarity}";
        }
    }
}