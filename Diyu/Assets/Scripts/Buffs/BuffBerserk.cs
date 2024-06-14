using System;
using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffBerserk : Buff
    {
        public float AS;
        //public float timer;

        public BuffBerserk(float ASpeed, float? duration,int id, Entity target)
        {
            Name = "Berserk";
            AS = ASpeed;
            Duration = duration;
            Id = id;
            Desc = "Attack Delay reduced based on lost Health";
            Target = target;
            maxDuration = duration;
            iconId = 3;
        }

        public override void Effect()
        {
            Target.aspdModifiers[Id] = AS * (float)Math.Round(((1 - Target.health / Target.maxHealth) * 20));
        }

        public override void OnEnd()
        {
            Target.aspdModifiers.Remove(Id);
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