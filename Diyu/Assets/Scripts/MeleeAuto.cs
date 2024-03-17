using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAuto : AutoFramework
{

    private Life stats;

    [Header("Melee Attack Stats")] 
    public bool performMeleeAuto = true;
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

    // Update is called once per frame
    void Update()
    {
        //autoInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);
        //attackDamage = stats.attackDamage;
        /*if (performMeleeAuto /*&& Time.time > nextAutoTime)
        {
            nextAutoTime = Time.time + autoInterval;
            //performMeleeAuto = false;
            StartCoroutine(AttackSequence());
        }*/
    }

    private IEnumerator AttackSequence()
    {
        Sphere.SetActive(true);
        //Body.GetComponent<Life>().ChangeHP(-1f);
        Collider[] colliders = Physics.OverlapSphere(Body.transform.position,4f);
        //Debug.LogError("Attacking");
        foreach (var c in colliders)
        { 
            if (c.GetComponent<AiCqcController>() || c.GetComponent<AiRangedController>())
            {
                //c.GetComponent<Life>().ChangeHP(attackDamage);
                //Debug.LogError($"{c.gameObject.GetComponent<Life>() != null}");
                if (c.gameObject.GetComponent<Life>() != null)
                {
                    //Debug.LogError($"{attackDamage}");
                    //Debug.LogError($"{c.gameObject.GetComponent<Life>().currentHp}"); 
                    c.gameObject.GetComponent<Life>().ChangeHP(attackDamage);
                    //c.gameObject.GetComponent<Life>().currentHp+=attackDamage;
                    //Debug.LogError($"{c.gameObject.GetComponent<Life>().currentHp}\n");
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
            //performMeleeAuto = false;
            StartCoroutine(AttackSequence());
        }
    }

    public void CheckForEnemies()
    {

        Collider[] colliders = Physics.OverlapSphere(Body.transform.position,400f);

        foreach (var c in colliders)
        {
            //if (c.GetComponent<AiCqcController>() || c.GetComponent<AiRangedController>())
            //{
                //c.GetComponent<Life>().ChangeHP(attackDamage);

                if (c.GetComponent<Life>() != null)
                {
                    c.GetComponent<Life>().ChangeHP(attackDamage);
                }
            //}
        }
    }
}
