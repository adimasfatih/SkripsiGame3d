using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isInvincible = false;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) { return; }
        currentHealth -= amount;
        healthBar.setHealth(currentHealth);
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
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
