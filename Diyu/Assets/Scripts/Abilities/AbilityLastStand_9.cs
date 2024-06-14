using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityLastStand_9 : Ability
    {
        public float HealAmount;
        public float Delay;
        public float ArmorBuff;
        public override int id { get => 9; }
        
        public AbilityLastStand_9(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Last Stand";
            switch (rarity)
            {
                case Rarities.COMMON:
                    HealAmount = 4;
                    Delay = 2;
                    ArmorBuff = 5;
                    Cooldown = 60;
                    break;
                case Rarities.UNCOMMON:
                    HealAmount = 5;
                    Delay = 2;
                    ArmorBuff = 6;
                    Cooldown = 55;
                    break;
                case Rarities.RARE:
                    HealAmount = 5;
                    Delay = 1.75f;
                    ArmorBuff = 7;
                    Cooldown = 50;
                    break;
                case Rarities.EPIC:
                    HealAmount = 5;
                    Delay = 1.50f;
                    ArmorBuff = 8;
                    Cooldown = 45;
                    break;
                case Rarities.LEGENDARY:
                    HealAmount = 5;
                    Delay = 1.25f;
                    ArmorBuff = 9;
                    Cooldown = 40;
                    break;
                case Rarities.MYTHIC:
                    HealAmount = 5;
                    Delay = 1f;
                    ArmorBuff = 10;
                    Cooldown = 35;
                    break;
            }
            displayDesc = $"Automatically triggers when below 30% Health. Heals you for {HealAmount} Health, and gives you +{ArmorBuff} Armor and Magic Resist for 10 seconds. Has a {Cooldown} seconds cooldown.";
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
            if (Target.health / Target.maxHealth <= 0.3f && CurrentCooldown <= 0)
            {
                CurrentCooldown = Cooldown;
                BuffRegen buff = new BuffRegen(HealAmount, Delay, 10, 9, Target);
                Target.AddBuff(buff);
                BuffArmor buff2 = new BuffArmor(ArmorBuff, 10, 10, Target);
                Target.AddBuff(buff2);
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
                    HealAmount = 4;
                    Delay = 2;
                    ArmorBuff = 5;
                    break;
                case Rarities.UNCOMMON:
                    HealAmount = 5;
                    Delay = 2;
                    ArmorBuff = 6;
                    break;
                case Rarities.RARE:
                    HealAmount = 5;
                    Delay = 1.75f;
                    ArmorBuff = 7;
                    break;
                case Rarities.EPIC:
                    HealAmount = 5;
                    Delay = 1.50f;
                    ArmorBuff = 8;
                    break;
                case Rarities.LEGENDARY:
                    HealAmount = 5;
                    Delay = 1.25f;
                    ArmorBuff = 9;
                    break;
                case Rarities.MYTHIC:
                    HealAmount = 5;
                    Delay = 1f;
                    ArmorBuff = 10;
                    break;
            }
            Rarity = rarity;
            displayDesc = $"Automatically triggers when below 30% Health. Heals you for {HealAmount} Health, and gives you +{ArmorBuff} Armor and Magic Resist for 10 seconds. Has a {Cooldown} seconds cooldown.";
        }
    }
}