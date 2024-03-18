using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFramework : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(PlayerBody parent) {}
    
    public virtual void End(PlayerBody parent) {}
    
    public virtual void Passive(PlayerBody parent) {}
}
