using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffAP : Buff
    {
        public float LS;

        public BuffAP(float ls, float? duration,int id, Entity target)
        {
            Name = "AP Buff";
            LS = ls;
            Duration = duration;
            Id = id;
            Desc = "Your magical attacks deal more damage.";
            Target = target;
            maxDuration = duration;
            iconId = 1;
        }

        public override void Effect()
        {
            
        }

        public override void OnEnd()
        {
            Target.abilityPower -= LS;
        }

        public override void OnAdd()
        {
            Target.abilityPower += LS;
        }

        public override void Refresh(float duration)
        {
            Duration = duration;
        }

        public override void Refresh(Buff buff)
        {
            if (buff is BuffAP)
            {
                OnEnd();
                BuffAP buffAP = (BuffAP)buff;
                LS = buffAP.LS;
                Duration = buffAP.Duration;
                OnAdd();
            }
            
        }
    }
}