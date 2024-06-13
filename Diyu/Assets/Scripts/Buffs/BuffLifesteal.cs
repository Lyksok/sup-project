using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffLifesteal : Buff
    {
        public float LS;

        public BuffLifesteal(float ls, float? duration,int id, Entity target)
        {
            Name = "Lifesteal Buff";
            LS = ls;
            Duration = duration;
            Id = id;
            Desc = "Your attacks heal you.";
            Target = target;
        }

        public override void Effect()
        {
            
        }

        public override void OnEnd()
        {
            Target.lifesteal -= LS;
        }

        public override void OnAdd()
        {
            Target.lifesteal += LS;
        }

        public override void Refresh(float duration)
        {
            Duration = duration;
        }

        public override void Refresh(Buff buff)
        {
            if (buff is BuffLifesteal)
            {
                OnEnd();
                BuffLifesteal buffLifesteal = (BuffLifesteal)buff;
                LS = buffLifesteal.LS;
                Duration = buffLifesteal.Duration;
                OnAdd();
            }
            
        }
    }
}