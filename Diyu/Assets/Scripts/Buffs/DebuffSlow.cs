using Entities;
using UnityEngine;

namespace Buffs
{
    public class DebuffSlow : Buff
    {
        public float MS;
        //public float timer;

        public DebuffSlow(float speed, float? duration,int id, Entity target)
        {
            Name = "Slowed Down";
            MS = speed;
            Duration = duration;
            Id = id;
            Desc = "You move slower.";
            Target = target;
            iconId = 1;
        }

        public override void Effect()
        {
            
        }

        public override void OnEnd()
        {
            Target.moveSpeed += MS;
        }

        public override void OnAdd()
        {
            Target.moveSpeed -= MS;
        }

        public override void Refresh(float duration)
        {
            Duration = duration;
        }
        
        public override void Refresh(Buff buff)
        {
            if (buff is DebuffSlow)
            {
                DebuffSlow _buff = (DebuffSlow)buff;
                MS = _buff.MS;
                Duration = _buff.Duration;
                Id = _buff.Id;
                Target = _buff.Target;
            }
        }
    }
}