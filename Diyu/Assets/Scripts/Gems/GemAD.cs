using Abilities;
using Buffs;
using Entities;

namespace Gems
{
    public class GemAD : Gem
    {
        public float statBuff;
        public override int id { get => 1; }
        
        public GemAD(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Gem
        {
            Name = "Attack Damage Gem";
            switch (rarity)
            {
                case Rarities.COMMON:
                    statBuff = 1;
                    break;
                case Rarities.UNCOMMON:
                    statBuff = 2;
                    break;
                case Rarities.RARE:
                    statBuff = 3;
                    break;
                case Rarities.EPIC:
                    statBuff = 4;
                    break;
                case Rarities.LEGENDARY:
                    statBuff = 5;
                    break;
                case Rarities.MYTHIC:
                    statBuff = 6;
                    break;
            }

            Rarity = rarity;
            Target = target;
            Target.attackDamage += statBuff;
        }

        public override void SetRarity(Rarities rarity)
        {
            Target.attackDamage -= statBuff;
            switch (rarity)
            {
                case Rarities.COMMON:
                    statBuff = 1;
                    break;
                case Rarities.UNCOMMON:
                    statBuff = 2;
                    break;
                case Rarities.RARE:
                    statBuff = 3;
                    break;
                case Rarities.EPIC:
                    statBuff = 4;
                    break;
                case Rarities.LEGENDARY:
                    statBuff = 5;
                    break;
                case Rarities.MYTHIC:
                    statBuff = 6;
                    break;
            }
            Rarity = rarity;
            Target.attackDamage += statBuff;
        }
    }
}