using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffMR : Buff
    {
        public float LS;

        public BuffMR(float ls, float? duration,int id, Entity target)
        {
            Name = "MR Buff";
            LS = ls;
            Duration = duration;
            Id = id;
            Desc = "You take reduced magical damage.";
            Target = target;
        }

        public override void Effect()
        {
            
        }

        public override void OnEnd()
        {
            Target.magicResist -= LS;
        }

        public override void OnAdd()
        {
            Target.magicResist += LS;
        }

        public override void Refresh(float duration)
        {
            Duration = duration;
        }

        public override void Refresh(Buff buff)
        {
            if (buff is BuffMR)
            {
                OnEnd();
                BuffMR buffMR = (BuffMR)buff;
                LS = buffMR.LS;
                Duration = buffMR.Duration;
                OnAdd();
            }
            
        }
    }
}