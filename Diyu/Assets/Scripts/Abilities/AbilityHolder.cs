using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public AbilityFramework ability;
    private float cooldownTime;
    private float activeTime;

    enum AbilityState
    {
        READY,
        ACTIVE,
        COOLDOWN
    }

    private AbilityState state = AbilityState.READY;
    public PlayerBody body;
    public KeyCode key;
    
    void Update()
    {
        ability.Passive(body);
        switch (state)
        {
            case AbilityState.READY:
                if (Input.GetKeyDown(key))
                {
                    Debug.LogError("Activated");
                    ability.Activate(body);
                    state = AbilityState.ACTIVE;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.ACTIVE:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.COOLDOWN;
                    cooldownTime = ability.cooldownTime;
                    ability.End(body);
                }
                break;
            case AbilityState.COOLDOWN:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.READY;
                }
                break;
        }
    }
}
