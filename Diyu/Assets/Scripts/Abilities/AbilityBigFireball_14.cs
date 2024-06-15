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
    public class AbilityBigFireball_14 : Ability
    {
        public override int id { get => 14; }
        public float damage;
        public float timer;
        public int curCount;
        private readonly GameObject _fireball;
        private readonly ParticleSystem _firelaunch;
        private const float FireSpeed = 50.0f;
        
        private readonly GameObject _indicator;
        
        public AbilityBigFireball_14(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Big Fireball";
            switch (rarity)
            {
                case Rarities.COMMON:
                    damage = 5;
                    Cooldown = 15;
                    break;
                case Rarities.UNCOMMON:
                    damage = 7;
                    Cooldown = 14;
                    break;
                case Rarities.RARE:
                    damage = 9;
                    Cooldown = 13;
                    break;
                case Rarities.EPIC:
                    damage = 11;
                    Cooldown = 12;
                    break;
                case Rarities.LEGENDARY:
                    damage = 13;
                    Cooldown = 11;
                    break;
                case Rarities.MYTHIC:
                    damage = 15;
                    Cooldown = 10;
                    break;
            }
            displayDesc = $"Fires a big fireball, dealing {damage} damage. Has a {Cooldown} seconds cooldown.";
            Rarity = rarity;
            State = States.READY;
            Target = target;
            _fireball = Target.resources.projectileList[0];
            _firelaunch = Target.resources.particleList[0];
            _indicator = Object.Instantiate(Target.resources.indicatorList[1], GetPostion(), Quaternion.identity);
            _indicator.transform.localScale *= 3;
            _indicator.SetActive(false);
        }
        
        public Vector3 GetPostion()
        {
            NewPlayer target = (NewPlayer)Target;
            var position = target.model.transform.position;
            position.y -= 0.95f;
            return position;
            
        }

        public override void OnEnd()
        {
            Object.Destroy(_indicator);
        }
        
        public override void PassiveEffect()
        {
            if (State == States.ACTIVE)
            {
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
            _indicator.SetActive(false);
            if (State == States.READY)
            {
                State = States.ACTIVE;
                CurrentDuration = curCount * 0.3f;
            }
        }

        public override void SetupEffect()
        {
            _indicator.SetActive(true);
            _indicator.transform.position = GetPostion();
            _indicator.transform.rotation = Target.model.transform.rotation;
        }

        public override void SetRarity(Rarities rarity)
        {
            switch (rarity)
            {
                case Rarities.COMMON:
                    damage = 5;
                    Cooldown = 15;
                    break;
                case Rarities.UNCOMMON:
                    damage = 7;
                    Cooldown = 14;
                    break;
                case Rarities.RARE:
                    damage = 9;
                    Cooldown = 13;
                    break;
                case Rarities.EPIC:
                    damage = 11;
                    Cooldown = 12;
                    break;
                case Rarities.LEGENDARY:
                    damage = 13;
                    Cooldown = 11;
                    break;
                case Rarities.MYTHIC:
                    damage = 15;
                    Cooldown = 10;
                    break;
            }
            displayDesc = $"Fires a big fireball, dealing {damage} damage. Has a {Cooldown} seconds cooldown.";
            Rarity = rarity;
        }
    }
}