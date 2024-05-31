using System;
using Entities;
using Mirror;

namespace Buffs
{
    public abstract class Buff
    {
        public string Name;

        public string Desc;

        public int Id;

        public Entity Target;

        public bool Stackable; //True if the debuff can be applied multiple times to the target
        public float? Duration { get; set; } = 0; //Time left on the buff, null if permanent

        protected float BaseDuration { get; }
        public bool permanent
        {
            get => Duration == null;
        } //false if the buff has a set duration, true otherwise
        
        public abstract void Effect(); //Activated periodically while the buff is active (i.e. a Regen)
        
        public abstract void OnEnd(); //Activated only once when the buff is dispelled (to remove the effects) (Automatically called when buff timer runs out, or can be manually called)
        
        public abstract void OnAdd(); //Activated only once when the buff is applied

        public abstract void Refresh(float duration); //Used to change the stats of a buff if permanent, or to reset its duration if reapplied when the debuff is not stackable (instead of having twice the same buff)
        
        public abstract void Refresh(Buff buff);

        
        [Command]
        public void Tick(float delta)
        {
            if (!permanent)
            {
                Duration -= delta;
            }
        }
    }
}