using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using Entities;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BossController : Monster
{
    //where ai will return without target
    [SerializeField]
    private Transform spawn = null;

    [SerializeField]
    private NavMeshAgent ai = null;
    //sphere collider with trigger on
    [SerializeField]
    public SightZone sightZone = null;

    [SerializeField]
    private Life life = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBak = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBakwav = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBakg1 = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBakg2 = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBakg3 = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBakg4 = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBakg5 = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBaktri1 = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBaktri2 = null;

    [FormerlySerializedAs("firespell")]
    [SerializeField]
    private Firespell_bak firespellBaktri3 = null;

    [SerializeField]
    private float timeBetweenAttacks = 0.0f;

    [SerializeField]
    private float HealTime = 0.0f;

    [SerializeField]
    private Transform eyeTransform = null;

    [SerializeField]
    public GameObject Greenkey = null;

    [SerializeField]
    public bool Spotted = false;

    [SerializeField]
    public bool AtSpawn = true;

    [SerializeField]
    private Animator anim = null;

    public LayerMask projectileMask;

    void FixedUpdate()
    {
        float distanceWithSpawn = Vector3.Distance(transform.position, spawn.transform.position);
        if (!Spotted)
        {
            HealTime += Time.deltaTime;
            if (distanceWithSpawn < 3)
            {
                AtSpawn = true;
            }
        }
        else
        {
            AtSpawn = false;
        }
    }

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        //had to put it in parent to make the sightzone still
        sightZone = GetComponentInParent<SightZone>();
        sightZone.onStay += OnEnemySpotted;
        sightZone.onExit += OnEnemyLeft;
        life.onEmpty += OnDeath;
        firespellBak = GetComponentInChildren<Firespell_bak>();
    }

    private bool CanSeeObject(GameObject go)
    {
        RaycastHit hit;

        if (Physics.Raycast(eyeTransform.position, go.transform.position - eyeTransform.position, out hit))
        {
            return hit.collider.gameObject == go;
        }
        else if (Physics.Raycast(eyeTransform.position, (go.transform.position + new Vector3(0.0f, 1.0f, 0.0f)) - eyeTransform.position, out hit))
        {
            return hit.collider.gameObject == go;
        }
        return false;
    }

    private void OnEnemySpotted(GameObject enemy)
    {
        if (CanSeeObject(enemy))
        {
            Debug.Log("SEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
            anim.SetBool("isSitting", false);
            transform.LookAt(enemy.transform);
            timeBetweenAttacks += Time.deltaTime;
            if (timeBetweenAttacks >= 5.0f)
            {
                int rand = UnityEngine.Random.Range(1, 4);
                Debug.Log(rand);
                if (rand == 1)
                {
                    timeBetweenAttacks = -2.9f;
                    StartCoroutine(GeyRoutine());
                }
                if (rand == 2)
                {
                    timeBetweenAttacks = -4.3f;
                    StartCoroutine(WavRoutine());
                }
                if (rand == 3)
                {
                    timeBetweenAttacks = -3.2f;
                    StartCoroutine(TriRoutine());
                }
            }
        }
        else
        {
            anim.SetBool("isSitting", true);
            if (HealTime >= 0.5)
            {
                life.ChangeHP(1.0f);
                HealTime = 0;
            }
        }
    }
    private void OnEnemyLeft(GameObject enemy)
    {
        //when player not in sightzone -> return to spawn
        anim.SetBool("isSitting", true);
        Spotted = false;
        if (HealTime >= 0.5)
        {
            life.ChangeHP(1.0f);
            HealTime = 0;
        }
    }

    public override void OnDeath()
    {
        StartCoroutine(DestroAIRoutine());
    }

    IEnumerator DestroAIRoutine()
    {
        yield return new WaitForSeconds(0.0f);

        //Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);
        Instantiate(Greenkey, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator GeyRoutine()
    {
        anim.SetBool("geysers", true);
        yield return new WaitForSeconds(2.9f);
        anim.SetBool("geysers", false);
        firespellBakg1.Attack();
        firespellBakg2.Attack();
        firespellBakg3.Attack();
        firespellBakg4.Attack();
        firespellBakg5.Attack();
    }

    IEnumerator WavRoutine()
    {
        anim.SetBool("vague", true);
        yield return new WaitForSeconds(4.3f);
        anim.SetBool("vague", false);
        firespellBakwav.Attack();
    }

    IEnumerator TriRoutine()
    {
        anim.SetBool("triple_projectile", true);
        yield return new WaitForSeconds(3.2f);
        anim.SetBool("triple_projectile", false);
        firespellBaktri1.Attack();
        firespellBaktri2.Attack();
        firespellBaktri3.Attack();
    }
}
