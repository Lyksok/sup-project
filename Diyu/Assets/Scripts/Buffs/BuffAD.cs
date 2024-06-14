using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffAD : Buff
    {
        public float LS;

        public BuffAD(float ls, float? duration,int id, Entity target)
        {
            Name = "AD Buff";
            LS = ls;
            Duration = duration;
            Id = id;
            Desc = "Your physical attacks deal more damage.";
            Target = target;
            maxDuration = duration;
            iconId = 0;
        }

        public override void Effect()
        {
            
        }

        public override void OnEnd()
        {
            Target.attackDamage -= LS;
        }

        public override void OnAdd()
        {
            Target.attackDamage += LS;
        }

        public override void Refresh(float duration)
        {
            Duration = duration;
        }

        public override void Refresh(Buff buff)
        {
            if (buff is BuffAD)
            {
                OnEnd();
                BuffAD buffAP = (BuffAD)buff;
                LS = buffAP.LS;
                Duration = buffAP.Duration;
                OnAdd();
            }
            
        }
    }
}