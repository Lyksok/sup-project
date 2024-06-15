using System;
using System.Security.Cryptography;
using Abilities;
using JetBrains.Annotations;
using Managers;
using UnityEngine;

namespace Entities
{
    public class GemOrb : Loot
    {
        public ResourceManager resources;
        private int _gemId;
        public Rarities _rarity;
        public GameObject Sphere;
        
        public void Start()
        {
            _gemId = RandomNumberGenerator.GetInt32(1, 9);
            _rarity = resources.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
            UpdateInfo();
            Sphere.gameObject.GetComponent<Renderer>().material.color = resources.GetRarityColor(_rarity);
        }
        
        public override void OnPickup(NewPlayer player)
        {
            //Debug.LogError("Picked up");
            player.PickupGem(player.resources.GetGem(_gemId,_rarity,player));
            Collected();

        }

        public override void UpdateInfo()
        {
            text.text = $"{resources.GetGemName(_gemId)} - {_rarity}";
        }
    }
}