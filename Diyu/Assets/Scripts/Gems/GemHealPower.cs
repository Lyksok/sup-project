using Abilities;
using Buffs;
using Entities;

namespace Gems
{
    public class GemHealPower : Gem
    {
        public float statBuff;
        public override int id { get => 5; }
        
        public GemHealPower(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Gem
        {
            displayName = "Healing Power Gem";
            displayDesc = "Permanently boosts Healing Power";
            switch (rarity)
            {
                case Rarities.COMMON:
                    statBuff = 0.05f;
                    break;
                case Rarities.UNCOMMON:
                    statBuff = 0.06f;
                    break;
                case Rarities.RARE:
                    statBuff = 0.07f;
                    break;
                case Rarities.EPIC:
                    statBuff = 0.08f;
                    break;
                case Rarities.LEGENDARY:
                    statBuff = 0.09f;
                    break;
                case Rarities.MYTHIC:
                    statBuff = 0.1f;
                    break;
            }

            Rarity = rarity;
            Target = target;
            Target.healingPower += statBuff;
        }

        public override void SetRarity(Rarities rarity)
        {
            Target.healingPower -= statBuff;
            switch (rarity)
            {
                case Rarities.COMMON:
                    statBuff = 0.05f;
                    break;
                case Rarities.UNCOMMON:
                    statBuff = 0.06f;
                    break;
                case Rarities.RARE:
                    statBuff = 0.07f;
                    break;
                case Rarities.EPIC:
                    statBuff = 0.08f;
                    break;
                case Rarities.LEGENDARY:
                    statBuff = 0.09f;
                    break;
                case Rarities.MYTHIC:
                    statBuff = 0.1f;
                    break;
            }
            Rarity = rarity;
            Target.healingPower += statBuff;
        }
    }
}