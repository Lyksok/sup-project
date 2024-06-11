using System;
using System.Collections;
using System.Collections.Generic;
using AOEs;
using Buffs;
using Entities;
using UnityEngine;
using Weapons;
using Object = UnityEngine.Object;

namespace Abilities
{
    public class AbilityVolley_7 : Ability
    {
        public override int id { get => 6; }
        public float damage;
        public float timer;
        public int count;
        public int curCount;
        private readonly GameObject _fireball;
        private readonly ParticleSystem _firelaunch;
        private const float FireSpeed = 50.0f;
        
        public AbilityVolley_7(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            Name = "Fire Volley";
            switch (rarity)
            {
                case Rarities.COMMON:
                    damage = 5;
                    Cooldown = 15;
                    count = 2;
                    break;
                case Rarities.UNCOMMON:
                    damage = 7;
                    Cooldown = 14;
                    count = 3;
                    break;
                case Rarities.RARE:
                    damage = 9;
                    Cooldown = 13;
                    count = 3;
                    break;
                case Rarities.EPIC:
                    damage = 11;
                    Cooldown = 12;
                    count = 4;
                    break;
                case Rarities.LEGENDARY:
                    damage = 13;
                    Cooldown = 11;
                    count = 4;
                    break;
                case Rarities.MYTHIC:
                    damage = 15;
                    Cooldown = 10;
                    count = 5;
                    break;
            }
            Rarity = rarity;
            State = States.READY;
            Target = target;
            timer = 0;
            curCount = 0;
            _fireball = Target.resources.projectileList[0];
            _firelaunch = Target.resources.particleList[0];
        }
        
        public override void OnEnd(){}
        
        public override void PassiveEffect()
        {
            timer -= Time.deltaTime;
            if (State == States.ACTIVE && timer <= 0)
            {
                curCount--;
                timer = 0.3f;
                var position = Target.anchor.transform.position;
                GameObject newFireball = Object.Instantiate(_fireball, position, Quaternion.identity);
                newFireball.GetComponent<Fireball>().damage = (damage);
                Rigidbody rb = newFireball.GetComponent<Rigidbody>();

                rb.AddForce(FireSpeed * Target.anchor.transform.forward, ForceMode.VelocityChange);
                _firelaunch.transform.position = position;
                _firelaunch.Play();
            }
        }

        public override void ActiveEffect()
        {
            if (State == States.READY)
            {
                State = States.ACTIVE;
                curCount = count;
                CurrentDuration = curCount * 0.3f;
            }
        }

        public override void SetupEffect()
        {
            //ability preview
        }

        public override void SetRarity(Rarities rarity)
        {
            switch (rarity)
            {
                case Rarities.COMMON:
                    damage = 5;
                    Cooldown = 15;
                    count = 2;
                    break;
                case Rarities.UNCOMMON:
                    damage = 7;
                    Cooldown = 14;
                    count = 3;
                    break;
                case Rarities.RARE:
                    damage = 9;
                    Cooldown = 13;
                    count = 3;
                    break;
                case Rarities.EPIC:
                    damage = 11;
                    Cooldown = 12;
                    count = 4;
                    break;
                case Rarities.LEGENDARY:
                    damage = 13;
                    Cooldown = 11;
                    count = 4;
                    break;
                case Rarities.MYTHIC:
                    damage = 15;
                    Cooldown = 10;
                    count = 5;
                    break;
            }
            Rarity = rarity;
        }
    }
}