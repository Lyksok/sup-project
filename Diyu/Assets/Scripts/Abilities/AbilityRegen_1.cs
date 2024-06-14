using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityRegen_1 : Ability
    {
        public float HealAmount; //Health healed each tick
        public float Delay; //Time in seconds between each tick
        public override int id { get => 1; }
        
        public AbilityRegen_1(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Regeneration";
            switch (rarity)
            {
                case Rarities.COMMON:
                    HealAmount = 1;
                    Delay = 2;
                    break;
                case Rarities.UNCOMMON:
                    HealAmount = 2;
                    Delay = 2;
                    break;
                case Rarities.RARE:
                    HealAmount = 3;
                    Delay = 1.5f;
                    break;
                case Rarities.EPIC:
                    HealAmount = 5;
                    Delay = 2;
                    break;
                case Rarities.LEGENDARY:
                    HealAmount = 4;
                    Delay = 1.25f;
                    break;
                case Rarities.MYTHIC:
                    HealAmount = 5;
                    Delay = 1;
                    break;
            }
            displayDesc = $"Passively regenerate {HealAmount} Health every {Delay} seconds.";
            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
            BuffRegen buff = new BuffRegen(HealAmount, Delay, null, 1, Target);
            Target.AddBuff(buff);
        }

        public override void OnEnd()
        {
            Target.RemoveBuff(new BuffRegen(HealAmount, Delay, null, 1, Target));
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
                    HealAmount = 1;
                    Delay = 2;
                    break;
                case Rarities.UNCOMMON:
                    HealAmount = 2;
                    Delay = 2;
                    break;
                case Rarities.RARE:
                    HealAmount = 3;
                    Delay = 1.5f;
                    break;
                case Rarities.EPIC:
                    HealAmount = 5;
                    Delay = 2;
                    break;
                case Rarities.LEGENDARY:
                    HealAmount = 4;
                    Delay = 1.25f;
                    break;
                case Rarities.MYTHIC:
                    HealAmount = 5;
                    Delay = 1;
                    break;
            }
            Rarity = rarity;
            Target.AddBuff(new BuffRegen(HealAmount, Delay, null, 1, Target));
            displayDesc = $"Passively regenerate {HealAmount} Health every {Delay} seconds.";
        }
    }
}