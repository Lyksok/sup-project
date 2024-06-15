using System;
using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityCharge_4 : Ability
    {
        public float Speed; //Speed Buff
        public override int id { get => 4; }
        
        public AbilityCharge_4(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Charge";
            switch (rarity)
            {
                case Rarities.COMMON:
                    Speed = 0.3f;
                    Cooldown = 15;
                    break;
                case Rarities.UNCOMMON:
                    Speed = 0.35f;
                    Cooldown = 14;
                    break;
                case Rarities.RARE:
                    Speed = 0.4f;
                    Cooldown = 13;
                    break;
                case Rarities.EPIC:
                    Speed = 0.45f;
                    Cooldown = 12;
                    break;
                case Rarities.LEGENDARY:
                    Speed = 0.5f;
                    Cooldown = 11;
                    break;
                case Rarities.MYTHIC:
                    Speed = 0.55f;
                    Cooldown = 10;
                    break;
            }
            displayDesc = $"Gain +{Math.Round(Speed * 100)}% speed for 3 seconds. Has a {Cooldown} seconds cooldown.";
            Rarity = rarity;
            State = States.READY;
            Target = target;
            
        }

        public override void OnEnd()
        {
            Target.RemoveBuff(new BuffMS(Speed, 3, 4, Target));
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
            if (State == States.READY)
            {
                State = States.COOLDOWN;
                CurrentCooldown = Cooldown;
                BuffMS buff = new BuffMS(Speed, 5, 4, Target);
                Target.AddBuff(buff);
            }
        }

        public override void SetRarity(Rarities rarity)
        {
            switch (rarity)
            {
                case Rarities.COMMON:
                    Speed = 0.1f;
                    Cooldown = 15;
                    break;
                case Rarities.UNCOMMON:
                    Speed = 0.15f;
                    Cooldown = 14;
                    break;
                case Rarities.RARE:
                    Speed = 0.2f;
                    Cooldown = 13;
                    break;
                case Rarities.EPIC:
                    Speed = 0.25f;
                    Cooldown = 12;
                    break;
                case Rarities.LEGENDARY:
                    Speed = 0.3f;
                    Cooldown = 11;
                    break;
                case Rarities.MYTHIC:
                    Speed = 0.35f;
                    Cooldown = 10;
                    break;
            }
            Rarity = rarity;
            displayDesc = $"Gain +{Math.Round(Speed * 100)}% speed for 3 seconds. Has a {Cooldown} seconds cooldown.";
        }
    }
}