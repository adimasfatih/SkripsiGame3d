using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isInvincible = false;
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.Instance.SetMaxHealth(maxHealth);
        
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) { return; }
        currentHealth -= amount;
        HealthBar.Instance.SetHealth(currentHealth);
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayRandomHurt(true, 1f); // true = player
        }
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        if (DeathManager.Instance != null)
        {
            DeathManager.Instance.PlayerDied();
        }
        gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Player healed. Current health: " + currentHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
