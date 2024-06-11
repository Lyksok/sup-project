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
    public class AbilityShockwave_6 : Ability
    {
        public override int id { get => 6; }
        public AreaOfEffect aoe;
        public float damage;
        private readonly GameObject _explosion;
        
        public AbilityShockwave_6(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            Name = "Shockwave";
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
            State = States.READY;
            Target = target;
            _explosion = Target.resources.projectileList[1];
        }

        public Vector3 GetPostion()
        {
            if (Target is NewPlayer)
            {
                NewPlayer target = (NewPlayer)Target;
                Ray ray = target.playerCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    Debug.DrawLine(ray.origin, hit.point);
                    return new Vector3(hit.point.x, target.model.transform.position.y, hit.point.z);
                }
            }
            
            return Vector3.one;
        }
        
        public override void OnEnd(){}
        
        public override void PassiveEffect()
        {
        }

        public override void ActiveEffect()
        {
            if (State == States.READY)
            {
                State = States.COOLDOWN;
                CurrentCooldown = Cooldown;
                Vector3 pos = Target.body.transform.position;
                aoe = new AreaOfEffect(pos, 6.0f,Target,damage,null,true,false,DamageType.PHYSICAL);
                aoe.Effect(aoe.FindTargets());
                GameObject newExplosion = Object.Instantiate(_explosion, pos, Quaternion.identity);
                //VisualEffect(pos);
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
            //ability preview
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
        }
    }
}