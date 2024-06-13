using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityArcanist_04 : Ability
    {
        public float apBuff;
        public override int id { get => 204; }
        
        public AbilityArcanist_04(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            Name = "Conjuration";
            switch (rarity)
            {
                case Rarities.COMMON:
                    apBuff = 5;
                    break;
                case Rarities.UNCOMMON:
                    apBuff = 7.5f;
                    break;
                case Rarities.RARE:
                    apBuff = 10;
                    break;
                case Rarities.EPIC:
                    apBuff = 12.5f;
                    break;
                case Rarities.LEGENDARY:
                    apBuff = 15;
                    break;
                case Rarities.MYTHIC:
                    apBuff = 17.5f;
                    break;
            }

            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
            BuffAP buff = new BuffAP(apBuff, null, 204, Target);
            Target.AddBuff(buff);
        }

        public override void OnEnd()
        {
            Target.RemoveBuff(new BuffAP(apBuff, null, 204, Target));
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
                    break;
                case Rarities.UNCOMMON:
                    apBuff = 7.5f;
                    break;
                case Rarities.RARE:
                    apBuff = 10;
                    break;
                case Rarities.EPIC:
                    apBuff = 12.5f;
                    break;
                case Rarities.LEGENDARY:
                    apBuff = 15;
                    break;
                case Rarities.MYTHIC:
                    apBuff = 17.5f;
                    break;
            }
            Rarity = rarity;
            Target.AddBuff(new BuffAP(apBuff, null, 204, Target));
        }
    }
}