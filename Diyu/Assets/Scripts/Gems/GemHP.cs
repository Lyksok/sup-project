﻿using Abilities;
using Buffs;
using Entities;

namespace Gems
{
    public class GemHP : Gem
    {
        public float statBuff;
        public override int id { get => 6; }
        
        public GemHP(Rarities rarity,NewPlayer target) //Sets the stats according to Rarity of the Gem
        {
            displayName = "Health Gem";
            displayDesc = "Permanently boosts Max Health";
            switch (rarity)
            {
                case Rarities.COMMON:
                    statBuff = 25;
                    break;
                case Rarities.UNCOMMON:
                    statBuff = 30;
                    break;
                case Rarities.RARE:
                    statBuff = 35;
                    break;
                case Rarities.EPIC:
                    statBuff = 40;
                    break;
                case Rarities.LEGENDARY:
                    statBuff = 45;
                    break;
                case Rarities.MYTHIC:
                    statBuff = 50;
                    break;
            }

            Rarity = rarity;
            Target = target;
            if (target != null)
            {
                Target.maxHealth += statBuff;
                Target.health += statBuff;
                Target.hpBonus += statBuff;
            }
        }

        public override void SetRarity(Rarities rarity)
        {
            Target.maxHealth -= statBuff;
            Target.health -= statBuff;
            switch (rarity)
            {
                case Rarities.COMMON:
                    statBuff = 25;
                    break;
                case Rarities.UNCOMMON:
                    statBuff = 30;
                    break;
                case Rarities.RARE:
                    statBuff = 35;
                    break;
                case Rarities.EPIC:
                    statBuff = 40;
                    break;
                case Rarities.LEGENDARY:
                    statBuff = 45;
                    break;
                case Rarities.MYTHIC:
                    statBuff = 50;
                    break;
            }
            Rarity = rarity;
            Target.maxHealth += statBuff;
            Target.health += statBuff;
        }
    }
}