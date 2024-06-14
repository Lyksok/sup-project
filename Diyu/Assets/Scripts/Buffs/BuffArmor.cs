using Entities;
using UnityEngine;

namespace Buffs
{
    public class BuffArmor : Buff
    {
        public float Stats;

        public BuffArmor(float stats, float? duration,int id, Entity target)
        {
            Name = "Resistance Buff";
            Stats = stats;
            Duration = duration;
            Id = id;
            Desc = "Damage taken is reduced.";
            Target = target;
            maxDuration = duration;
            iconId = 2;
        }

        public override void Effect()
        {
            
        }

        public override void OnEnd()
        {
            Target.armor -= Stats;
            Target.magicResist -= Stats;
        }

        public override void OnAdd()
        {
            Target.armor += Stats;
            Target.magicResist += Stats;
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