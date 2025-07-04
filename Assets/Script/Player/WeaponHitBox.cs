using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    public Weapon weapon; // Link to weapon that holds damage value

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(weapon.damage);
            }
        }
    }
}
