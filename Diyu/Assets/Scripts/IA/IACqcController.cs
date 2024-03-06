using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AiCqcController : MonoBehaviour
{
    //where ai will return without target
    [SerializeField]
    private Transform spawn = null;

    [SerializeField]
    private float AttackCD = 1.0f;

    [SerializeField]
    private float TimeBetweenAttacks = 0.0f;

    [SerializeField]
    private NavMeshAgent ai = null;
    //sphere collider with trigger on
    [SerializeField]
    public SightZone sightZone = null;

    [SerializeField]
    private Life life = null;

    void FixedUpdate()
    {
        TimeBetweenAttacks += Time.deltaTime;
    }

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        //had to put it in parent to make the sightzone still
        sightZone = GetComponentInParent<SightZone>();
        sightZone.onStay += OnEnemySpotted;
        sightZone.onExit += OnEnemyLeft;
        life.onEmpty += Die;
    }

    private void OnEnemySpotted(GameObject enemy)
    {
        //ai follows player until it leaves
        ai.SetDestination(enemy.transform.position);
        Life life = enemy.gameObject.GetComponent<Life>();
        float distanceWithEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        if (distanceWithEnemy <= 3 && TimeBetweenAttacks >= AttackCD)
        {
            Debug.LogError("ATTAAAAAAAAAAAAAAAACK");
            life.ChangeHP(-1.0f);
            TimeBetweenAttacks = 0.0f;
        }
    }
    private void OnEnemyLeft(GameObject enemy)
    {
        //when player not in sightzone -> return to spawn
        ai.SetDestination(spawn.transform.position);
    }

    private void Die()
    {
        StartCoroutine(DestroAIRoutine());
    }

    IEnumerator DestroAIRoutine()
    {
        yield return new WaitForSeconds(0.0f);

        //Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
