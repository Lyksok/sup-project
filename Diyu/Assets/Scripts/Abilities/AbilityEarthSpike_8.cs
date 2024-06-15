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
    public class AbilityEarthSpike_8 : Ability
    {
        public override int id { get => 8; }
        public AreaOfEffect aoe;
        public float damage;
        public float range;
        private readonly GameObject _explosion;
        private readonly GameObject _indicator;
        private readonly GameObject _rangeIndicator;
        
        public AbilityEarthSpike_8(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Earth Spike";
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
            displayDesc = $"Causes an earth spike to emerge at your cursor's location, dealing {damage} damage to all enemies hit. Has a {Cooldown} seconds cooldown.";
            Rarity = rarity;
            State = States.READY;
            Target = target;
            range = 7f;
            _explosion = Target.resources.projectileList[8];
            _indicator = Object.Instantiate(Target.resources.indicatorList[0], GetPostion(), Quaternion.identity);
            _indicator.transform.localScale *= 3;
            _indicator.SetActive(false);
            _rangeIndicator= Object.Instantiate(Target.resources.indicatorList[2], Target.model.transform.position - (Vector3.up * 0.95f), Quaternion.identity);
            _rangeIndicator.transform.localScale *= range;
            _rangeIndicator.SetActive(false);
        }

        public Vector3 GetPostion()
        {
            if (Target is NewPlayer)
            {
                NewPlayer target = (NewPlayer)Target;
                Ray ray = target.playerCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100,target.layerMask))
                {
                    Debug.DrawLine(ray.origin, hit.point);
                    var position = target.model.transform.position;
                    position.y -= 0.95f;
                    Vector3 hitPoint = new Vector3(hit.point.x, position.y, hit.point.z);
                    var hitPosDir = (hitPoint - position).normalized;
                    float dist = Vector3.Distance(hitPoint, position);
                    dist = Math.Min(dist,range);
                    var newHitPos = position + hitPosDir * dist;
                    return newHitPos;
                }
            }
            
            return Vector3.one;
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
            _rangeIndicator.SetActive(false);
            if (State == States.READY)
            {
                State = States.COOLDOWN;
                CurrentCooldown = Cooldown;
                Vector3 pos = GetPostion();
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
            _indicator.SetActive(true);
            _indicator.transform.position = GetPostion();
            var rotation = Target.model.transform.rotation;
            _indicator.transform.rotation = rotation;
            _rangeIndicator.SetActive(true);
            _rangeIndicator.transform.position = Target.model.transform.position - (Vector3.up * 0.95f);
            _rangeIndicator.transform.rotation = rotation;
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
            displayDesc = $"Causes an earth spike to emerge at your cursor's location, dealing {damage} damage to all enemies hit. Has a {Cooldown} seconds cooldown.";
        }
    }
}