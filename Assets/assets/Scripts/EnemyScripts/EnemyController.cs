using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrol,
    Chase,
    Attack
}


public class EnemyController : MonoBehaviour
{

    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;

    private EnemyState enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;

    public float chaseDistance = 7f;
    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;
    public float patrolRadiusMin = 20f;
    public float patrolRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    public float waitBeforAttack = 2f;

    private float currentChaseDistance;
    private float patrolTimer = 15f;
    private float attackTimer;

    private Transform target;

    public GameObject attackPoint;

    private EnemyAudio enemyAudio;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PlayerTag).transform;

        enemyAudio = GetComponentInChildren<EnemyAudio>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Patrol;

        //
        patrolTimer = patrolForThisTime;

        //when the enemy first gets to the player attack right away
        attackTimer = waitBeforAttack;

        //memorize the velue of chase distance so that we can use that again
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.Patrol)
        {
            Petrol();
        }
        else if (enemyState == EnemyState.Chase)
        {
            Chase();
        }
        else if (enemyState == EnemyState.Attack)
        {
            Attack();
        }
    }

    private void Attack()
    {

        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;

        attackTimer += Time.deltaTime;

        if (attackTimer > waitBeforAttack)
        {
            enemyAnimator.Attack();

            attackTimer = 0f;

            //play attackSound
            enemyAudio.PlayAttackSound();
        }

        if (Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.Chase; 
        }

    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = runSpeed;

        //set PlayersPosition as destination
        navMeshAgent.SetDestination(target.position);

        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Run(true);
        }
        else
        {
            enemyAnimator.Run(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            //stopAnimation
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            enemyState = EnemyState.Attack;

            //reste the chase distance to previous
            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            //player run away from enemy

            //stop running
            enemyAnimator.Run(false);

            enemyState = EnemyState.Patrol;
            //reset the petrol time so that the function can calculate new petrol destinantion
            patrolTimer = patrolForThisTime;

            //reste the chase distance to previous
            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }

    }

    private void Petrol()
    {
        //tell nav agent that he can move
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;

        //add to the patrol timer
        patrolTimer += Time.deltaTime;

        if(patrolTimer > patrolForThisTime)
        {
            setNewRandomDestination();
            patrolTimer = 0f;
        }

        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Walk(true);
        }
        else
        {
            enemyAnimator.Walk(false);
        }

        //test the distrance bw player and enemy
        if (Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnimator.Walk(false);
            //enemyAnimator.Run(true);

            enemyState = EnemyState.Chase;

            //play spotedDistance
            enemyAudio.PlayScreamSound();
        }

    }

    private void setNewRandomDestination()
    {

        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);

        Vector3 ranDir = Random.insideUnitSphere * randRadius;
        ranDir += transform.position;

        NavMeshHit navMeshHit;

        //This will make sure that the random position is insde the movable are 
        NavMesh.SamplePosition(ranDir, out navMeshHit, randRadius, -1);

        //ignors the layrs outside the nevigational area
        navMeshAgent.SetDestination(navMeshHit.position);

    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    public EnemyState enemyCurrentState
    {
        get; set;
    }

}
