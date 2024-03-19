using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Melee circular cleave auto attack
public class MeleeAuto : AutoFramework
{

    private Life stats;

    [Header("Melee Attack Stats")]
    private float autoInterval;
    private float nextAutoTime = 0;
    private float attackDamage;
    
    public GameObject Sphere;
    public PlayerBody Body;
    
    // Start is called before the first frame update
    void Start()
    {
        autoInterval = 0.0f;
        attackDamage = -1;
    }

    private IEnumerator AttackSequence()
    {
        Sphere.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(Body.transform.position,4f);
        foreach (var c in colliders)
        { 
            if (c.GetComponent<AiCqcController>() || c.GetComponent<AiRangedController>())
            {
                if (c.gameObject.GetComponent<Life>() != null)
                {
                    c.gameObject.GetComponent<Life>().ChangeHP(attackDamage);
                }
            }
        }
        yield return new WaitForSeconds(0.25f);
        Sphere.SetActive(false);
    }

    public override void Attack()
    {
        if (Time.time > nextAutoTime)
        {
            nextAutoTime = Time.time + autoInterval;
            StartCoroutine(AttackSequence());
        }
    }

    public void CheckForEnemies()
    {

        Collider[] colliders = Physics.OverlapSphere(Body.transform.position,400f);

        foreach (var c in colliders)
        {
            if (c.GetComponent<Life>() != null)
            {
                c.GetComponent<Life>().ChangeHP(attackDamage);
            }

        }
    }
}
