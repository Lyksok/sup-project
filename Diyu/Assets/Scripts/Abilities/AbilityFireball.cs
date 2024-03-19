using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;


//Fires 3 Fireballs in a volley
[CreateAssetMenu]
public class AbilityFireball : AbilityFramework
{
    [SerializeField]
    private GameObject Fireballprefab = null;

    [SerializeField]
    private ParticleSystem Firelaunch = null;

    [SerializeField]
    private float FireSpeed = 50.0f;

    private float cd = 0;
    private float interval = 0.25f; //interval between each fireball
    private int cpt = 0; //# of fireballs

    private void Update()
    {
        
    }
    
    public override void ActiveEffect(PlayerBody parent)
    {
        if (Time.time > cd && cpt > 0)
        {
            GameObject NewFireball = Instantiate(Fireballprefab, parent.launcher.transform.position, Quaternion.identity);

            Rigidbody rb = NewFireball.GetComponent<Rigidbody>();

            rb.AddForce(FireSpeed * parent.launcher.transform.forward, ForceMode.VelocityChange);
        
            Firelaunch.Play();
            cd = Time.time + interval;
            cpt -= 1;
        }
    }
    
    public override void Activate(PlayerBody parent)
    {
        cpt = 3;
    }
    
    public override void End(PlayerBody parent)
    {
        
    }
    
    public override void Passive(PlayerBody parent)
    {
        
    }
}