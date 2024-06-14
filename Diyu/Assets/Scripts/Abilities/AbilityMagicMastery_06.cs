using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityMagicMastery_06 : Ability
    {
        public float apBuff;
        public float mrBuff;
        public override int id { get => 206; }
        
        public AbilityMagicMastery_06(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Magic Mastery";
            switch (rarity)
            {
                case Rarities.COMMON:
                    apBuff = 5;
                    mrBuff = 5;
                    break;
                case Rarities.UNCOMMON:
                    apBuff = 6;
                    mrBuff = 6;
                    break;
                case Rarities.RARE:
                    apBuff = 7;
                    mrBuff = 7;
                    break;
                case Rarities.EPIC:
                    apBuff = 8;
                    mrBuff = 8;
                    break;
                case Rarities.LEGENDARY:
                    apBuff = 9;
                    mrBuff = 9;
                    break;
                case Rarities.MYTHIC:
                    apBuff = 10;
                    mrBuff = 10;
                    break;
            }
            displayDesc = $"Gain +{apBuff} Ability Power and +{mrBuff} Magic Resistance.";
            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
            BuffAP buff = new BuffAP(apBuff, null, 206, Target);
            Target.AddBuff(buff);
            BuffMR buff2 = new BuffMR(mrBuff, null, 216, Target);
            Target.AddBuff(buff2);
        }

        public override void OnEnd()
        {
            Target.RemoveBuff(new BuffAP(apBuff, null, 206, Target));
            Target.RemoveBuff(new BuffMR(mrBuff, null, 207, Target));
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
                    apBuff = 5;
                    mrBuff = 5;
                    break;
                case Rarities.UNCOMMON:
                    apBuff = 6;
                    mrBuff = 6;
                    break;
                case Rarities.RARE:
                    apBuff = 7;
                    mrBuff = 7;
                    break;
                case Rarities.EPIC:
                    apBuff = 8;
                    mrBuff = 8;
                    break;
                case Rarities.LEGENDARY:
                    apBuff = 9;
                    mrBuff = 9;
                    break;
                case Rarities.MYTHIC:
                    apBuff = 10;
                    mrBuff = 10;
                    break;
            }
            Rarity = rarity;
            displayDesc = $"Gain +{apBuff} Ability Power and +{mrBuff} Magic Resistance.";
            Target.AddBuff(new BuffAP(apBuff, null, 206, Target));
            Target.AddBuff(new BuffMR(mrBuff, null, 216, Target));
        }
    }
}