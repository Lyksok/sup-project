using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityDash : AbilityFramework
{
    public float dashVelocity;
    [SerializeField]
    private Transform SpawnTransform = null;
    private Vector3 dash = Vector3.forward;
    public override void Activate(PlayerBody parent)
    {
        Debug.LogError("Dashing");
        Rigidbody rb = parent.rigidBody;
        dash = parent.Aim();
        Debug.LogError($"{dash}, {dashVelocity}");
        rb.AddForce(dash.normalized * dashVelocity, ForceMode.VelocityChange);
    }

    public override void End(PlayerBody parent)
    {
        parent.rigidBody.velocity = Vector3.zero;
    }
    
    public override void Passive(PlayerBody parent)
    {
        
    }
}
