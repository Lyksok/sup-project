using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffRegen : Buff
    {
        public float HealAmount; //Health healed each tick
        public float Delay; //Time in seconds between each tick
        //public float timer;

        public BuffRegen(float healAmount, float delay, float? duration,int id, Entity target)
        {
            Name = "Regeneration";
            HealAmount = healAmount;
            Delay = delay;
            Duration = duration;
            Id = id;
            Name = "Regeneration";
            Desc = "Slowly regenerating health points.";
            Target = target;
            timer = Delay;
            //Debug.LogError(Delay);
        }

        public override void Effect()
        {
            if (timer <= 0)
            {
                timer = Delay;
                Target.CmdHeal(HealAmount);
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