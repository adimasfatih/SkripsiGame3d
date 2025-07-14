using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class enemyBehavior : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10;
    private Animator animator;

    private bool isPreparingAttack = false;
    private bool isAttacking = false;

    private Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;
    private int currentPatrolIndex;
    private bool isStunned = false;
    private Coroutine currentStunRoutine;

    private enum State { Patrol, Chase, Attack }
    private State currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrol;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (isStunned) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                if (distance <= detectionRange)
                {
                    currentState = State.Chase;
                }
                break;

            case State.Chase:
                Chase(distance);
                break;

            case State.Attack:
                Attack(distance);
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (animator != null) { 
            animator.SetBool("IsWalking", true);
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void Chase(float distance)
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            isPreparingAttack = false;
            isAttacking = false;
        }
        if (distance > detectionRange + 2f) // buffer to return to patrol
        {
            currentState = State.Patrol;
            GoToNextPatrolPoint();
            isPreparingAttack = false;
            isAttacking = false;
        }
        else if (distance <= attackRange)
        {
            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
            }
            agent.isStopped = true;
            currentState = State.Attack;

        }
    }

    void Attack(float distance)
    {
        if (isAttacking) return;


        // Face the player (optional)
        transform.LookAt(player);

        if (distance > attackRange)
        {
            agent.isStopped = false; // Resume movement if player is out of range
            currentState = State.Chase;

            if (animator != null) { 
                animator.SetBool("IsWalking", true);
            }
        }
        else
        {
            agent.isStopped = true; // Ensure the enemy stays still while attacking

            if (!isAttacking &&  Time.time >= lastAttackTime + attackCooldown)
            {
                isAttacking = true;
                isPreparingAttack = true;
                if (animator != null)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetTrigger("Attack");
                   
                }
                lastAttackTime = Time.time;
            }
        }
    }

    public void Stun(float duration)
    {
        // Stop the current stun coroutine if it's running
        if (currentStunRoutine != null)
        {
            StopCoroutine(currentStunRoutine);
        }

        // Start a new one
        currentStunRoutine = StartCoroutine(UnstunAfterDelay(duration));
        isStunned = true;
        agent.isStopped = true;
        if (animator != null)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("stunned");
            animator.SetBool("IsWalking", false);
            isPreparingAttack = false;
            isAttacking = false;
        }
        Debug.Log("Enemy Stunned");
    }

    private IEnumerator UnstunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isStunned = false;
        agent.isStopped = false;
        currentStunRoutine = null;
        Debug.Log("Enemy Unstunned");
    }

    public void DealDamageToPlayer()
    {
        if (!isPreparingAttack) return; //skipp jika ke stunned

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange + 0.5f) // buffer range for animation sync
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null && !playerHealth.isInvincible)
                {
                    playerHealth.TakeDamage(damage);
                    Debug.Log("Enemy deals damage via animation!");
                }
            }
        }
    }

    public void EndAttack()
    {
        isPreparingAttack = false;
        isAttacking = false;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
