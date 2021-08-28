 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{

    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private EnemyController enemyController;

    public float health = 100f;

    public bool isPlayer;
    public bool isBoar;
    public bool isCannible;

    private bool isDead;

    private EnemyAudio enemyAudio;

    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Awake()
    {
        if (isBoar || isCannible)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            //get Enemy Audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }
        if (isPlayer)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }

    //apply Damage
    public void ApplyDamage(float damage)
    {
        
        //if player is dead don't exicute the reat of the code
        if (isDead)
        {
            return;
        }

        health -= damage;

        if (isPlayer)
        {
            //show player stats(display health UI value)
            playerStats.DisplayHealthStats(health);
        }

        if (isBoar || isCannible)
        {

            if (enemyController.enemyCurrentState == EnemyState.Patrol)
            {
                enemyController.chaseDistance = 50f;
            }

        }

        if (health <= 0)
        {
            PlayerDied();
            isDead = true;
        }

    }

    private void PlayerDied()
    {
        if (isCannible)
        {
            gameObject.AddComponent<Rigidbody>();
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50);

            enemyController.enabled = false;
            navMeshAgent.enabled = false;
            enemyAnimator.enabled = false;
            StartCoroutine(DeadSound());

            //EnemyManage Spawn more enemy
            EnemyManager.instance.EnemyDied(true);
        }

        if (isBoar)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
            enemyController.enabled = false;

            enemyAnimator.Dead();

            StartCoroutine(DeadSound());

            //EnemyManage Spawn more enemy
            EnemyManager.instance.EnemyDied(false);
        }

        //player dead
        if (isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.EnemyTag);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;

                //call enemy manager to stop spawning enemies
                EnemyManager.instance.StopSpawning();

                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            }

        }

        if (tag == Tags.PlayerTag)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }

    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeadSound();
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

}
