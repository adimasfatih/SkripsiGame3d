using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyHealtkunti : MonoBehaviour
{
    public float health;
    public float hitFreezeDuration = 0.1f;
    public float flashDuration = 0.1f;

    private bool isHit = false;
    private Renderer[] enemyRenderers; // Changed from single Renderer to an array of Renderers
    private Color originalColor;

    private NavMeshAgent agent;
    

    void Start()
    {
        // Get all the renderers in the children
        enemyRenderers = GetComponentsInChildren<Renderer>(); // Changed to get all child renderers

        // Store the original color from the first renderer (optional)
        if (enemyRenderers.Length > 0)
            originalColor = enemyRenderers[0].material.color;

        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (!isHit)
            StartCoroutine(HitFlashRoutine());

        if (health <= 0)
            Die();
    }

    IEnumerator HitFlashRoutine()
    {
        isHit = true;

        // Flash red for all child renderers
        foreach (Renderer enemyRenderer in enemyRenderers) // Iterate through all child renderers
        {
            enemyRenderer.material.color = Color.red; // Change color to red
        }

        yield return new WaitForSeconds(flashDuration);

        // Revert color for all child renderers
        foreach (Renderer enemyRenderer in enemyRenderers) // Iterate through all child renderers
        {
            enemyRenderer.material.color = originalColor; // Revert to original color
        }

        yield return new WaitForSeconds(hitFreezeDuration);

        isHit = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
