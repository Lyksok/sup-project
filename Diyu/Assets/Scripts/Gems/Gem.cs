using Abilities;
using Entities;
using Mirror;

namespace Gems
{
    public abstract class Gem
    {
        public string Name { get; protected set; }
        public string Desc { get; protected set; }
        public abstract int id { get; }
        public Entity Target;
        public Rarities Rarity { get; protected set; } //Rarity of the ability, changes stats
        public abstract void SetRarity(Rarities rarity); //Sets the Rarity of the Ability to the input Rarity, changing stats
        public void ChangeRarity(int change) //Changes the Rarity of the ability by 1 tier, up or down (1 -> 1 tier up, -1 -> 1 tier down)
        {
            if (change == 1)
            {
                if (Rarity == Rarities.COMMON)
                {
                    SetRarity(Rarities.UNCOMMON);
                } else if (Rarity == Rarities.UNCOMMON)
                {
                    SetRarity(Rarities.RARE);
                } else if (Rarity == Rarities.RARE)
                {
                    SetRarity(Rarities.EPIC);
                } else if (Rarity == Rarities.EPIC)
                {
                    SetRarity(Rarities.LEGENDARY);
                } else if (Rarity == Rarities.LEGENDARY)
                {
                    SetRarity(Rarities.MYTHIC);
                }
            }
            else if (change == -1)
            {
                if (Rarity == Rarities.UNCOMMON)
                {
                    SetRarity(Rarities.COMMON);
                } else if (Rarity == Rarities.EPIC)
                {
                    SetRarity(Rarities.RARE);
                } else if (Rarity == Rarities.LEGENDARY)
                {
                    SetRarity(Rarities.EPIC);
                } else if (Rarity == Rarities.MYTHIC)
                {
                    SetRarity(Rarities.LEGENDARY);
                } else if (Rarity == Rarities.RARE)
                {
                    SetRarity(Rarities.UNCOMMON);
                }
            }
        }
    }
}