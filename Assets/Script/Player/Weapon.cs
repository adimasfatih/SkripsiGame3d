using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float stunDuration = 0.4f;
    BoxCollider triggerBox;

    private HashSet<GameObject> damagedEnemies = new HashSet<GameObject>();

    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
        triggerBox.enabled = false;
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
        damagedEnemies.Clear(); // Clear previously hit targets for new attack
    }

    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject root = other.transform.root.gameObject; // ensures consistency
            if (!damagedEnemies.Contains(root))
            {
                damagedEnemies.Add(root);

                EnemyHealth enemy = root.GetComponent<EnemyHealth>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    //Debug.Log($"Dealt {damage} to {root.name}");
                    enemyBehavior behavior = root.GetComponent<enemyBehavior>();
                    if (behavior != null)
                    {
                        behavior.Stun(stunDuration); // Freeze enemy for 2 seconds
                    }
                    ProjectileShooterEnemy behavior1 = root.GetComponent<ProjectileShooterEnemy>();
                    if (behavior1 != null)
                    {
                        behavior1.Stun(stunDuration); // Freeze enemy for 2 seconds
                    }
                }
            }
        }
    }
}
