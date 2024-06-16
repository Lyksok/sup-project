using System;
using System.Collections;
using System.Collections.Generic;
using AOEs;
using Buffs;
using Entities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Abilities
{
    public class AbilityGravity_12 : Ability
    {
        public override int id { get => 12; }
        public AreaOfEffect aoe;
        public float damage;
        public float range;
        private readonly GameObject _explosion;
        private readonly GameObject _indicator;
        
        private syncManager syncManager;

        private void Update()
        {
            syncManager = Object.FindObjectOfType<syncManager>();
            //Debug.LogError(syncManager != null);
        }
        
        public AbilityGravity_12(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Gravity";
            switch (rarity)
            {
                case Rarities.COMMON:
                    damage = 10;
                    Cooldown = 20;
                    break;
                case Rarities.UNCOMMON:
                    damage = 12;
                    Cooldown = 18;
                    break;
                case Rarities.RARE:
                    damage = 14;
                    Cooldown = 16;
                    break;
                case Rarities.EPIC:
                    damage = 16;
                    Cooldown = 14;
                    break;
                case Rarities.LEGENDARY:
                    damage = 18;
                    Cooldown = 12;
                    break;
                case Rarities.MYTHIC:
                    damage = 20;
                    Cooldown = 10;
                    break;
            }
            displayDesc = $"Release a burst of magic at your location, dealing {damage} damage to all enemies hit and slowing them down. Has a {Cooldown} seconds cooldown.";
            Rarity = rarity;
            State = States.READY;
            Target = target;
            range = 7.5f;
            _explosion = Target.resources.projectileList[1];
            _indicator = Object.Instantiate(Target.resources.indicatorList[0], GetPostion(), Quaternion.identity);
            _indicator.transform.localScale *= 3;
            _indicator.SetActive(false);
        }

        public void CmdAttack()
        {
            Update();
            var position = GetPostion();
            syncManager.CmdSpawnAoe(9,position);
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
        }

        public override void ActiveEffect()
        {
            _indicator.SetActive(false);
            if (State == States.READY)
            {
                State = States.COOLDOWN;
                CurrentCooldown = Cooldown;
                Vector3 pos = GetPostion();
                aoe = new AreaOfEffect(pos, 6.0f,Target,damage,new DebuffSlow(4,3,12,Target),false,false,DamageType.MAGICAL);
                aoe.Effect(aoe.FindTargets());
                CmdAttack();
            }
        }

        public IEnumerator VisualEffect(Vector3 pos)
        {
            GameObject newExplosion = Object.Instantiate(_explosion, pos, Quaternion.identity);
            newExplosion.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            newExplosion.SetActive(false);
            Object.Destroy(newExplosion);
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
                    damage = 10;
                    Cooldown = 20;
                    break;
                case Rarities.UNCOMMON:
                    damage = 12;
                    Cooldown = 18;
                    break;
                case Rarities.RARE:
                    damage = 14;
                    Cooldown = 16;
                    break;
                case Rarities.EPIC:
                    damage = 16;
                    Cooldown = 14;
                    break;
                case Rarities.LEGENDARY:
                    damage = 18;
                    Cooldown = 12;
                    break;
                case Rarities.MYTHIC:
                    damage = 20;
                    Cooldown = 10;
                    break;
            }
            Rarity = rarity;
            displayDesc = $"Creates a shockwave at your location, dealing {damage} damage to all enemies hit. Has a {Cooldown} seconds cooldown.";
        }
    }
}