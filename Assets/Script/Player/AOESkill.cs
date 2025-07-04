using UnityEngine;

public class AOESkill : MonoBehaviour
{
    public int damage = 400;
    public float stunDuration = 2f;
    public float duration = 0.2f;

    void Start()
    {
        Destroy(gameObject, duration); // Destroy after short time
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject root = other.transform.root.gameObject;

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                Debug.Log($"AOE hit {other.name}");
            }
            enemyBehavior melee = root.GetComponent<enemyBehavior>();
            if (melee != null)
            {
                melee.Stun(stunDuration);
            }

            ProjectileShooterEnemy ranged = root.GetComponent<ProjectileShooterEnemy>();
            if (ranged != null)
            {
                ranged.Stun(stunDuration);
            }
        }
    }
}
