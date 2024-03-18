using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityHeal : AbilityFramework
{
    public float healForce;
    public override void Activate(PlayerBody parent)
    {
        Debug.LogError("Healing");
        parent.life.ChangeHP(healForce);
    }

    public override void End(PlayerBody parent)
    {
        
    }
    
    public override void Passive(PlayerBody parent)
    {
        
    }
}
