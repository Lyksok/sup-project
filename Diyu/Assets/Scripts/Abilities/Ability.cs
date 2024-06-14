using Entities;
using Mirror;
using UnityEngine;

namespace Abilities
{
    //Abstract class used for managing Abilities
    public abstract class Ability : DescribableObject
    {
        public string Name { get; protected set; }
        public string Desc { get; protected set; }
        public abstract int id { get; }
        public Entity Target;
        public bool inUse = false;
        public Rarities Rarity { get; protected set; } //Rarity of the ability, changes stats
        public float CurrentCooldown { get; set; } = 0; //Current cooldown, if >= Cooldown the ability can be used, is set to 0 when used (c'est l'inverse en fait lol)
        protected float CurrentDuration { get; set; } = 0; //Current Duration, if == 0 the ability ends
        public float Cooldown { get; set; } //Base cooldown of the ability
        public States State;
        protected bool CanUse => State == States.READY;
        public abstract void OnEnd(); //Deletes the effect of the ability, called when the ability is removed / swapped out
        public abstract void PassiveEffect(); //Passive effect of the Ability, can be null
        public abstract void ActiveEffect(); //Active effect of the Ability, can be null, called on key release
        public abstract void SetupEffect(); //Called when ability key is pressed, to either preview ability or use it if it doesnt have a preview
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
        
        [Command]
        public void Tick(float delta)
        {
            if (State == States.ACTIVE)
            {
                if (CurrentDuration > 0)
                {
                    CurrentDuration -= delta;
                }
                else
                {
                    State = States.COOLDOWN;
                    CurrentCooldown = Cooldown;
                }
            } else if (State == States.COOLDOWN)
            {
                if (CurrentCooldown > 0)
                {
                    CurrentCooldown -= delta;
                }
                else
                {
                    State = States.READY;
                }
            }
        }
    }
}