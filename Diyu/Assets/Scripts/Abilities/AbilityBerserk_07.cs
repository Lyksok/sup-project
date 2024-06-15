using System;
using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityBerserk_07 : Ability
    {
        public float aspd;
        public override int id { get => 207; }
        
        public AbilityBerserk_07(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Berserk";
            switch (rarity)
            {
                case Rarities.COMMON:
                    aspd = 0.07f; //% of attack speed gained for every 5% missing HP
                    break;
                case Rarities.UNCOMMON:
                    aspd = 0.075f;
                    break;
                case Rarities.RARE:
                    aspd = 0.08f;
                    break;
                case Rarities.EPIC:
                    aspd = 0.085f;
                    break;
                case Rarities.LEGENDARY:
                    aspd = 0.09f;
                    break;
                case Rarities.MYTHIC:
                    aspd = 0.095f;
                    break;
            }
            displayDesc = $"Gain +{Math.Round(aspd * 100)}% Attack Speed for every 5% of your missing Health";
            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
            BuffBerserk buff = new BuffBerserk(aspd, null, 207, Target);
            Target.AddBuff(buff);
        }

        public override void OnEnd()
        {
            Target.RemoveBuff(new BuffBerserk(aspd, null, 207, Target));
        }

        public override void PassiveEffect()
        {
            //BuffRegen buff = new BuffRegen(HealAmount, Delay, null, 1, Target);
            //Target.AddBuff(buff);
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
                    aspd = 0.07f; //% of attack speed gained for every 5% missing HP
                    break;
                case Rarities.UNCOMMON:
                    aspd = 0.075f;
                    break;
                case Rarities.RARE:
                    aspd = 0.08f;
                    break;
                case Rarities.EPIC:
                    aspd = 0.085f;
                    break;
                case Rarities.LEGENDARY:
                    aspd = 0.09f;
                    break;
                case Rarities.MYTHIC:
                    aspd = 0.095f;
                    break;
            }
            displayDesc = $"Gain +{Math.Round(aspd * 100)}% Attack Speed for every 5% of your missing Health";
            Rarity = rarity;
            Target.AddBuff(new BuffBerserk(aspd, null, 207, Target));
        }
    }
}