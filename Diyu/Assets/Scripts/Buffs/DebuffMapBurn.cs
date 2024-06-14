using Entities;
using UnityEngine;

namespace Buffs
{
    public class DebuffMapBurn : Buff
    {
        public float DamageAmount; //Health healed each tick
        public float Delay; //Time in seconds between each tick
        //public float timer;

        public DebuffMapBurn(float damageAmount, float delay, float? duration,int id, Entity target)
        {
            DamageAmount = damageAmount;
            Delay = delay;
            Duration = duration;
            Id = id;
            Name = "Guardian's Wrath";
            Desc = "You are burning alive, get closer to the center of the labyrinth to lessen the effects.";
            Target = target;
            timer = Delay;
            //Debug.LogError(Delay);
        }

        public override void Effect()
        {
            if (timer <= 0)
            {
                timer = Delay;
                Target.CmdTakeDamage(DamageAmount,DamageType.TRUE_DAMAGE,null);
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