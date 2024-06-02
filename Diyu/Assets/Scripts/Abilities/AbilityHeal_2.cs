using Entities;

namespace Abilities
{
    public class AbilityHeal_2 : Ability
    {
        public override int id { get => 2; }
        public float HealAmount; //Health healed
        
        public AbilityHeal_2(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            switch (rarity)
            {
                case Rarities.COMMON:
                    HealAmount = 10;
                    Cooldown = 10;
                    break;
                case Rarities.UNCOMMON:
                    HealAmount = 11;
                    Cooldown = 9;
                    break;
                case Rarities.RARE:
                    HealAmount = 12;
                    Cooldown = 8;
                    break;
                case Rarities.EPIC:
                    HealAmount = 13;
                    Cooldown = 7;
                    break;
                case Rarities.LEGENDARY:
                    HealAmount = 14;
                    Cooldown = 6;
                    break;
                case Rarities.MYTHIC:
                    HealAmount = 15;
                    Cooldown = 5;
                    break;
            }
            Target = target;
        }
        public override void PassiveEffect()
        {
            
        }

        public override void ActiveEffect()
        {
            
        }

        public override void SetupEffect()
        {
            if (State == States.READY)
            {
                State = States.COOLDOWN;
                CurrentCooldown = Cooldown;
                Target.Heal(HealAmount);
            }
        }

        public override void SetRarity(Rarities rarity)
        {
            switch (rarity)
            {
                case Rarities.COMMON:
                    HealAmount = 10;
                    Cooldown = 10;
                    break;
                case Rarities.UNCOMMON:
                    HealAmount = 11;
                    Cooldown = 9;
                    break;
                case Rarities.RARE:
                    HealAmount = 12;
                    Cooldown = 8;
                    break;
                case Rarities.EPIC:
                    HealAmount = 13;
                    Cooldown = 7;
                    break;
                case Rarities.LEGENDARY:
                    HealAmount = 14;
                    Cooldown = 6;
                    break;
                case Rarities.MYTHIC:
                    HealAmount = 15;
                    Cooldown = 5;
                    break;
            }
        }
    }
}