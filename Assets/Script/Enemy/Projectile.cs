using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifeTime = 5f;

    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime); // destroy after x seconds
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null && !player.isInvincible)
            {
                player.TakeDamage(damage);
                
            }
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject); // hits environment
        }
    }
}
