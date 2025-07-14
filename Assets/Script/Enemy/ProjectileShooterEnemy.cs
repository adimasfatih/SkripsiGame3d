using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ProjectileShooterEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float detectionRange = 15f;
    public float attackRange = 10f;
    public float attackCooldown = 2f;
    private Animator animator;

    public GameObject projectilePrefab;
    public Transform firePoint; // Empty child GameObject for spawn position

    private Transform player;
    private Transform target;
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
        target = GameObject.FindGameObjectWithTag("Target").transform;
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

        if (animator != null)
        {
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
        }

        if (distance > detectionRange + 2f)
        {
            currentState = State.Patrol;
            GoToNextPatrolPoint();
        }
        else if (distance <= attackRange)
        {
            agent.isStopped = true;
            currentState = State.Attack;
            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }

    void Attack(float distance)
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z)); // look at player horizontally

        if (distance > attackRange)
        {
            agent.isStopped = false;
            currentState = State.Chase;
            if (animator != null)
            {
                animator.SetBool("IsWalking", true);
            }
        }
        else
        {
            agent.isStopped = true;

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                FireProjectile();
                if (animator != null)
                {
                    animator.SetTrigger("Attack");

                }
                lastAttackTime = Time.time;
            }
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector3 direction = (target.position - firePoint.position).normalized;
            Projectile projectile = proj.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetDirection(direction);
            }
            SoundManager.Instance.PlayAttackKunti();
        }
    }

    public void Stun(float duration)
    {
        if (currentStunRoutine != null)
        {
            StopCoroutine(currentStunRoutine);
        }

        currentStunRoutine = StartCoroutine(UnstunAfterDelay(duration));
        isStunned = true;
        agent.isStopped = true;
        if (animator != null)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("stunned");
            animator.SetBool("IsWalking", false);
        }
        Debug.Log("Projectile Enemy Stunned");
    }

    private IEnumerator UnstunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isStunned = false;
        agent.isStopped = false;
        currentStunRoutine = null;
        Debug.Log("Projectile Enemy Unstunned");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
