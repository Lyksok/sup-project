using System;
using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityLifesteal_08 : Ability
    {
        public float StealAmount;
        public override int id { get => 208; }
        
        public AbilityLifesteal_08(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Reaper's Mark";
            switch (rarity)
            {
                case Rarities.COMMON:
                    StealAmount = 0.1f;
                    break;
                case Rarities.UNCOMMON:
                    StealAmount = 0.12f;
                    break;
                case Rarities.RARE:
                    StealAmount = 0.14f;
                    break;
                case Rarities.EPIC:
                    StealAmount = 0.16f;
                    break;
                case Rarities.LEGENDARY:
                    StealAmount = 0.18f;
                    break;
                case Rarities.MYTHIC:
                    StealAmount = 0.2f;
                    break;
            }
            displayDesc = $"Gain +{Math.Round(StealAmount*100)}% Lifesteal.";
            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
            BuffLifesteal buff = new BuffLifesteal(StealAmount, null, 208, Target);
            Target.AddBuff(buff);
        }

        public override void OnEnd()
        {
            Target.RemoveBuff(new BuffLifesteal(StealAmount, null, 208, Target));
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
                    StealAmount = 0.1f;
                    break;
                case Rarities.UNCOMMON:
                    StealAmount = 0.12f;
                    break;
                case Rarities.RARE:
                    StealAmount = 0.14f;
                    break;
                case Rarities.EPIC:
                    StealAmount = 0.16f;
                    break;
                case Rarities.LEGENDARY:
                    StealAmount = 0.18f;
                    break;
                case Rarities.MYTHIC:
                    StealAmount = 0.2f;
                    break;
            }
            Rarity = rarity;
            Target.AddBuff(new BuffLifesteal(StealAmount, null, 208, Target));
            displayDesc = $"Gain +{Math.Round(StealAmount*100)}% Lifesteal.";
        }
    }
}