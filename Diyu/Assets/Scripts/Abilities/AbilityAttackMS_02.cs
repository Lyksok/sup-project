using System;
using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityAttackMS_02 : Ability
    {
        public float Speed; //Speed Buff
        public override int id { get => 202; }
        
        public AbilityAttackMS_02(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            Name = "Hunt";
            switch (rarity)
            {
                case Rarities.COMMON:
                    Speed = 0.1f;
                    break;
                case Rarities.UNCOMMON:
                    Speed = 0.15f;
                    break;
                case Rarities.RARE:
                    Speed = 0.2f;
                    break;
                case Rarities.EPIC:
                    Speed = 0.25f;
                    break;
                case Rarities.LEGENDARY:
                    Speed = 0.30f;
                    break;
                case Rarities.MYTHIC:
                    Speed = 0.35f;
                    break;
            }

            //Cooldown = 3;
            //CurrentCooldown = 0;
            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
        }

        public override void OnEnd()
        {
            Target.RemoveBuff(new BuffMS(Speed, 3, 4, Target));
        }

        public override void PassiveEffect()
        {
            if (Target.primaryWeapon.timeSinceLastAttack <= 0.1f)
            {
                BuffMS buff = new BuffMS(Speed, 1, 202, Target);
                Target.AddBuff(buff);
            }
        }

        public override void ActiveEffect()
        {
            
        }

        public override void SetupEffect()
        {
            
        }

        public override void SetRarity(Rarities rarity)
        {
            switch (rarity)
            {
                case Rarities.COMMON:
                    Speed = 0.1f;
                    break;
                case Rarities.UNCOMMON:
                    Speed = 0.15f;
                    break;
                case Rarities.RARE:
                    Speed = 0.2f;
                    break;
                case Rarities.EPIC:
                    Speed = 0.25f;
                    break;
                case Rarities.LEGENDARY:
                    Speed = 0.30f;
                    break;
                case Rarities.MYTHIC:
                    Speed = 0.35f;
                    break;
            }
            Rarity = rarity;
        }
    }
}