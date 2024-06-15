using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
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

    [SerializeField]
    private Firespell firespell = null;

    [SerializeField]
    private Firespell firespellwave = null;

    [SerializeField]
    private Firespell firespellg1 = null;

    [SerializeField]
    private Firespell firespellg2 = null;

    [SerializeField]
    private Firespell firespellg3 = null;

    [SerializeField]
    private Firespell firespellg4 = null;

    [SerializeField]
    private Firespell firespellg5 = null;

    [SerializeField]
    private Firespell firespelltri1 = null;

    [SerializeField]
    private Firespell firespelltri2 = null;

    [SerializeField]
    private Firespell firespelltri3 = null;

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
        life.onEmpty += Die;
        firespell = GetComponentInChildren<Firespell>();
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

    private void Die()
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
        firespellg1.Attack();
        firespellg2.Attack();
        firespellg3.Attack();
        firespellg4.Attack();
        firespellg5.Attack();
    }

    IEnumerator WavRoutine()
    {
        anim.SetBool("vague", true);
        yield return new WaitForSeconds(4.3f);
        anim.SetBool("vague", false);
        firespellwave.Attack();
    }

    IEnumerator TriRoutine()
    {
        anim.SetBool("triple_projectile", true);
        yield return new WaitForSeconds(3.2f);
        anim.SetBool("triple_projectile", false);
        firespelltri1.Attack();
        firespelltri2.Attack();
        firespelltri3.Attack();
    }
}
