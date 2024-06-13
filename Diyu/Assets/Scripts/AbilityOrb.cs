using System;
using System.Security.Cryptography;
using Abilities;
using JetBrains.Annotations;
using Managers;
using UnityEngine;

namespace Entities
{
    public class AbilityOrb : Loot
    {
        public ResourceManager resources;
        private int _abilityId;
        private Rarities _rarity;
        
        public void Start()
        {
                _abilityId = RandomNumberGenerator.GetInt32(1, resources.abilityCount + 1);
                _rarity = resources.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
                UpdateInfo();
        }
        
        public override void OnPickup(NewPlayer player)
        {
            //Debug.LogError("Picked up");
            Ability replaced = player.PickupAbility(player.resources.GetAbility(_abilityId,_rarity,player));
            if (replaced is AbilityNone_0)
            {
                Collected();
            }
            else
            {
                _abilityId = replaced.id;
                _rarity = replaced.Rarity;
            }
            UpdateInfo();

        }

        public override void UpdateInfo()
        {
            text.text = $"{resources.GetAbilityName(_abilityId)} - {_rarity}";
        }
    }
}