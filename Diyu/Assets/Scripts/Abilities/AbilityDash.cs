using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;


//Dashes towards mouse position
[CreateAssetMenu]
public class AbilityDash : AbilityFramework
{
    public float dashVelocity;
    private Vector3 dash = Vector3.zero;
    
    public override void Activate(PlayerBody parent)
    {
        Rigidbody rb = parent.rigidBody;
        dash = parent.Aim();
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
