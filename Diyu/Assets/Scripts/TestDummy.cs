using System.Collections.Generic;
using Abilities;
using Buffs;
using Mirror;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class TestDummy : Entity
    {
        [Header("Player characteristics")] 
        public Rigidbody playerRigidbody;
        //public Camera playerCamera;
        public float movementSpeed = 5f;
        private void Start()
        {
            //playerRigidbody = transform.GetComponent<Rigidbody>();
            buffList = new List<Buff>();
            debuffList = new List<Buff>();
            abilityList[0] = new AbilityRegen_1(Rarities.COMMON, this);
            abilityList[1] = new AbilityHeal_2(Rarities.LEGENDARY, this);
            primaryWeapon = new Firespell(Rarities.RARE,this);
            health = 50;
            maxHealth = 100;
        }

        // Update is called once per frame
        void Update()
        {
            if (isLocalPlayer)
            {
                HandleMovement();
                HandleBuffs();
                HandleDebuffs();
                //HandleAttack(primaryWeapon,primaryWeaponAttackKey);
                HandleAbility(abilityList[0], KeyCode.Alpha1);
                HandleAbility(abilityList[1], KeyCode.Alpha2);
                //SrvMovement();
            }
        }
        
        [Command(requiresAuthority = false)]
        private void SrvStopMovement()
        {
            playerRigidbody.velocity = Vector3.zero;
        }

        private void HandleMovement()
        {
            SrvStopMovement();
            playerRigidbody.velocity = Vector3.zero;
        }
    }
}