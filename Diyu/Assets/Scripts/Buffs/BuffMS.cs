using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffMS : Buff
    {
        public float MS;
        //public float timer;

        public BuffMS(float speed, float? duration,int id, Entity target)
        {
            Name = "Movement Speed Buff";
            MS = speed;
            Duration = duration;
            Id = id;
            Desc = "You can move faster.";
            Target = target;
            maxDuration = duration;
            iconId = 6;
            
        }

        public override void Effect()
        {
            
        }

        public override void OnEnd()
        {
            Target.moveSpeed -= MS;
        }

        public override void OnAdd()
        {
            Target.moveSpeed += MS;
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