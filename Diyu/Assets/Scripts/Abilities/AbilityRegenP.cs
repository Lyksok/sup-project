using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityRegenP : AbilityFramework
{
    public float regenForce;
    public float regenTick;
    private float nextTick;
    public override void Activate(PlayerBody parent)
    {
    }

    public override void End(PlayerBody parent)
    {
    }
    
    public override void Passive(PlayerBody parent)
    {
        if (nextTick < Time.time)
        {
            //parent.life.ChangeHP(regenForce);
            //nextTick = Time.time + regenTick;
            //en commentaire car sinn les particules de heal sont permanentes sur le joueur, de plus le joueur à une compétence de regen
        }
    }
}