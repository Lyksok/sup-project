using System;
using Buffs;
using Entities;

namespace Abilities
{
    public class AbilityElemental_03 : Ability
    {
        public float Speed; //Speed Buff
        public override int id { get => 203; }
        
        public AbilityElemental_03(Rarities rarity,Entity target) //Sets the stats according to Rarity of the Ability
        {
            Name = "Elemental";

            //Cooldown = 3;
            //CurrentCooldown = 0;
            Rarity = rarity;
            State = States.PASSIVE;
            Target = target;
        }

        public override void OnEnd()
        {
            
        }

        public override void PassiveEffect()
        {
            
        }

        public override void ActiveEffect()
        {
            
        }

        public override void SetupEffect()
        {
            
        }

        public override void SetRarity(Rarities rarity)
        {
            Rarity = rarity;
        }
    }
}