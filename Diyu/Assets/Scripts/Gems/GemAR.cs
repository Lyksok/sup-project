using Abilities;
using Buffs;
using Entities;

namespace Gems
{
    public class GemAR : Gem
    {
        public float statBuff;
        public override int id { get => 2; }
        
        public GemAR(Rarities rarity,NewPlayer target) //Sets the stats according to Rarity of the Gem
        {
            displayName = "Armor Gem";
            displayDesc = "Permanently boosts Armor";
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
            if (target != null)
            {
                Target.armor += statBuff;
                Target.arBonus += statBuff;
            }
        }

        public override void SetRarity(Rarities rarity)
        {
            Target.armor -= statBuff;
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
            Target.armor += statBuff;
        }
    }
}