using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityAssassin_05 : Ability
    {
        public float adBuff;
        public override int id { get => 205; }
        
        public AbilityAssassin_05(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            displayName = "Assassin's Mastery";
            switch (rarity)
            {
                case Rarities.COMMON:
                    adBuff = 5;
                    break;
                case Rarities.UNCOMMON:
                    adBuff = 7.5f;
                    break;
                case Rarities.RARE:
                    adBuff = 10;
                    break;
                case Rarities.EPIC:
                    adBuff = 12.5f;
                    break;
                case Rarities.LEGENDARY:
                    adBuff = 15;
                    break;
                case Rarities.MYTHIC:
                    adBuff = 17.5f;
                    break;
            }
            displayDesc = $"Gain +{adBuff} Attack Damage";
            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
            BuffAD buff = new BuffAD(adBuff, null, 205, Target);
            Target.AddBuff(buff);
        }

        public override void OnEnd()
        {
            Target.RemoveBuff(new BuffAD(adBuff, null, 205, Target));
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
                    adBuff = 5;
                    break;
                case Rarities.UNCOMMON:
                    adBuff = 7.5f;
                    break;
                case Rarities.RARE:
                    adBuff = 10;
                    break;
                case Rarities.EPIC:
                    adBuff = 12.5f;
                    break;
                case Rarities.LEGENDARY:
                    adBuff = 15;
                    break;
                case Rarities.MYTHIC:
                    adBuff = 17.5f;
                    break;
            }
            displayDesc = $"Gain +{adBuff} Attack Damage";
            Rarity = rarity;
            Target.AddBuff(new BuffAD(adBuff, null, 205, Target));
        }
    }
}