using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject projectile;

    public float health = 3;

    // Patrolling
    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacking;
    bool alreadyAttacked;

    // states
    public float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;

    //for the boss
    public bool isBoss;

    //for no respawn, on veut finir le jeu quoiii
    public string enemyID;

    // Stun
    private bool isStunned = false;
    private Coroutine stunCoroutine;
    [Header("Stun VFX")]
    [SerializeField] private GameObject stunEffectPrefab;
    [SerializeField] private float headHeight = 2f;
    private GameObject currentStunEffect;

    private void Awake()
    {
        if (!player)
        {
            player = GameObject.Find("Player").transform;
        }
        if (!agent)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (isBoss)
        {
            //GetComponent<Renderer>().material.color = Color.red;
            var rend = GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                rend.material.color = Color.red;
            }
            else
            {
                Debug.LogError("fuck");
            }
        }

        if (GameplayManager.instance.IsEnemyDefeated(enemyID))
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (isStunned) return;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        if (!agent.hasPath)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);


        if (!alreadyAttacked)
        {
            // Attack
            FireballSpell fireballSpell = Instantiate(projectile, transform.position, transform.rotation).GetComponent<FireballSpell>();
            fireballSpell.Init(3, transform.forward, true,this.tag);
            gameObject.GetComponent<AudioSource>().Play();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacking);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;
        Debug.Log("Dégâts reçus" + health, this.gameObject);
        if (health <= 0)
        {
            GameplayManager.instance.MarkEnemyDefeated(enemyID);
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void Stun(float duration = 3f)
    {
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
        }
        stunCoroutine = StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        agent.isStopped = true;

        if (stunEffectPrefab != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * headHeight;
            currentStunEffect = Instantiate(stunEffectPrefab, spawnPosition, Quaternion.identity, transform);
        }

        yield return new WaitForSeconds(duration);

        agent.isStopped = false;
        isStunned = false;
        stunCoroutine = null;
    }
}
