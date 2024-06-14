using System;
using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityBulwark_01 : Ability
    {
        public float ArmorBuff;
        public override int id { get => 201; }
        
        public AbilityBulwark_01(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Bulwark";
            switch (rarity)
            {
                case Rarities.COMMON:
                    ArmorBuff = 5;
                    break;
                case Rarities.UNCOMMON:
                    ArmorBuff = 6;
                    break;
                case Rarities.RARE:
                    ArmorBuff = 7;
                    break;
                case Rarities.EPIC:
                    ArmorBuff = 8;
                    break;
                case Rarities.LEGENDARY:
                    ArmorBuff = 9;
                    break;
                case Rarities.MYTHIC:
                    ArmorBuff = 10; 
                    break;
            }
            displayDesc = $"Gain +{ArmorBuff} Armor and Magic Resistance after not attacking for 3 seconds";
            Cooldown = 3;
            CurrentCooldown = 0;
            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
        }

        public override void OnEnd()
        {
            //Target.RemoveBuff(new BuffRegen(HealAmount, Delay, 10, 9, Target));
        }

        public override void PassiveEffect()
        {
            if (Target.primaryWeapon.timeSinceLastAttack <= 0.1f)
            {
                CurrentCooldown = Cooldown;
                Target.RemoveBuff(new BuffArmor(ArmorBuff,null,201,Target));
            }

            if (Math.Abs(Target.primaryWeapon.timeSinceLastAttack - 3) < 0.1f)
            {
                Target.AddBuff(new BuffArmor(ArmorBuff,null,201,Target));
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
                    ArmorBuff = 5;
                    break;
                case Rarities.UNCOMMON:
                    ArmorBuff = 6;
                    break;
                case Rarities.RARE:
                    ArmorBuff = 7;
                    break;
                case Rarities.EPIC:
                    ArmorBuff = 8;
                    break;
                case Rarities.LEGENDARY:
                    ArmorBuff = 9;
                    break;
                case Rarities.MYTHIC:
                    ArmorBuff = 10; 
                    break;
            }
            Rarity = rarity;
            displayDesc = $"Gain +{ArmorBuff} Armor and Magic Resistance after not attacking for 3 seconds";
        }
    }
}