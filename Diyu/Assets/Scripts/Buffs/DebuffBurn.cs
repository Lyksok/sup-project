using Entities;
using UnityEngine;

namespace Buffs
{
    public class DebuffBurn : Buff
    {
        public float DamageAmount; //Health healed each tick
        public float Delay; //Time in seconds between each tick
        //public float timer;

        public DebuffBurn(float damageAmount, float delay, float? duration,int id, Entity target)
        {
            DamageAmount = damageAmount;
            Delay = delay;
            Duration = duration;
            Id = id;
            Name = "Wildfire";
            Desc = "You are burning.";
            Target = target;
            timer = Delay;
            //Debug.LogError(Delay);
        }

        public override void Effect()
        {
            if (timer <= 0)
            {
                timer = Delay;
                Target.CmdTakeDamage(DamageAmount,DamageType.PHYSICAL,null);
            }
        }

        public override void OnEnd()
        {
            
        }

        public override void OnAdd()
        {
            
        }

        public override void Refresh(float duration)
        {
            Duration = duration;
        }

        public override void Refresh(Buff buff)
        {
            
        }
    }
}