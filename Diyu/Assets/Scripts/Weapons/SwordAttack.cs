using System.Collections.Generic;
using System.Linq;
using Abilities;
using AOEs;
using Entities;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Weapons
{
    public class SwordAttack : Weapon
    {
        //public override string Name => "firespell";
        public override int id => 2;
        private readonly GameObject _conal;

        public SwordAttack(Rarities rarity,Entity user)
        {
            Name = "Sword";
            switch (rarity)
            {
                case Rarities.COMMON:
                    baseDamage = 10;
                    baseASPD = 0.75f;
                    attackSpeedPercent =  0.5f;
                    damagePercent = 0.5f;
                    break;
                case Rarities.UNCOMMON:
                    baseDamage = 12;
                    baseASPD = 0.8f;
                    attackSpeedPercent =  0.55f;
                    damagePercent = 0.6f;
                    break;
                case Rarities.RARE:
                    baseDamage = 14;
                    baseASPD = 0.85f;
                    attackSpeedPercent =  0.60f;
                    damagePercent = 0.7f;
                    break;
                case Rarities.EPIC:
                    baseDamage = 16;
                    baseASPD = 0.9f;
                    attackSpeedPercent =  0.65f;
                    damagePercent = 0.8f;
                    break;
                case Rarities.LEGENDARY:
                    baseDamage = 18;
                    baseASPD = 0.95f;
                    attackSpeedPercent =  0.70f;
                    damagePercent = 0.9f;
                    break;
            }
            type = DamageType.PHYSICAL;
            User = user;
            Cooldown = 1 / (baseASPD * (attackSpeedPercent * User.attackSpeed));
            CurrentCooldown = 0;
            _conal = User.resources.projectileList[2];
        }

        public override void SetRarity(Rarities rarity)
        {
            switch (rarity)
            {
                case Rarities.COMMON:
                    break;
                case Rarities.UNCOMMON:
                    break;
                case Rarities.RARE:
                    break;
                case Rarities.EPIC:
                    break;
                case Rarities.LEGENDARY:
                    break;
            }
        }
        
        [Command]
        public override void CmdAttack()
        {
            if (CanAttack)
            {
                Cooldown = 1 / (baseASPD * (attackSpeedPercent * User.attackSpeed));
                CurrentCooldown = Cooldown;
                AttackRpc();
            }
        }
        
        [ClientRpc]
        public override void AttackRpc()
        {
            var position = User.model.transform.position;
            SquareAOE aoe = new SquareAOE(User.model.transform.position + (User.model.transform.forward * 2.5f), Vector3.one * 1.5f, User.model.transform.rotation * Quaternion.AngleAxis(45,Vector3.up),User,(baseDamage + damagePercent * User.abilityPower),null,true,false,type);
            var hitList = aoe.FindTargets();
            AreaOfEffect aoe2 = new AreaOfEffect(position, 3.0f,User,0,null,true,false,type);
            var hitList2 = aoe2.FindTargets();
            List<Entity> hitFinal = new List<Entity>();
            //Debug
            //GameObject newConal = Object.Instantiate(_conal, User.model.transform.position + (User.model.transform.forward * 2.5f), User.model.transform.rotation * Quaternion.AngleAxis(45,Vector3.up));
            foreach (var entity in hitList)
            {
                foreach (var entity2 in hitList2)
                {
                    if (entity.netId == entity2.netId)
                    {
                        hitFinal.Add(entity);
                    }
                }
            }
            //var hitFinal = hitList.Intersect(hitList2);
            aoe.Effect(hitFinal);
            //Debug.LogError((hitList.Count,hitList2.Count,hitFinal.Count));
        }
        
    }
}