using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffRegen : Buff
    {
        public float HealAmount; //Health healed each tick
        public float Delay; //Time in seconds between each tick
        public float timer;

        public BuffRegen(float healAmount, float delay, float? duration, bool stackable,int id, Entity target)
        {
            HealAmount = healAmount;
            Delay = delay;
            Duration = duration;
            Stackable = stackable;
            Id = id;
            Name = "Regeneration";
            Desc = "Slowly regenerating health points.";
            Target = target;
            timer = 0;
        }

        public override void Effect()
        {
            if (timer <= Time.time)
            {
                timer = Delay + Time.time;
                Target.Heal(HealAmount);
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
            
        }

        public override void Refresh(Buff buff)
        {
            
        }
    }
}