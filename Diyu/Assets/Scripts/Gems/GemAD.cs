using Abilities;
using Buffs;
using Entities;

namespace Gems
{
    public class GemAD : Gem
    {
        public float statBuff;
        public override int id { get => 1; }
        
        public GemAD(Rarities rarity,NewPlayer target) //Sets the stats according to Rarity of the Gem
        {
            displayName = "Attack Damage Gem";
            displayDesc = "Permanently boosts Attack Damage";
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
                Target.attackDamage += statBuff;
                Target.adBonus += statBuff;
            }
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