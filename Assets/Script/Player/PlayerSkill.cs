using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject aoeHitboxPrefab;
    public float skillCooldown = 15f;
    private float cooldownTimer = 0f;
    private bool canUseSkill = true;

    public Animator animator;
    public SkillBar skillBar;


    void Start()
    {
        if (skillBar != null)
        {
            skillBar.SetMaxCooldown(skillCooldown);
            skillBar.SetCooldown(0); // Skill is ready at start
        }
    }

    void Update()
    {
        if (canUseSkill && inputManager._Skill) // Or your input system
        {
            TriggerSkill();
        }

        // Cooldown logic
        if (!canUseSkill)
        {
            cooldownTimer += Time.deltaTime;
            if (skillBar != null)
            {
                skillBar.SetCooldown(skillCooldown - cooldownTimer);
            } // Update UI to count down


            if (cooldownTimer >= skillCooldown)
            {
                cooldownTimer = 0f;
                canUseSkill = true;
                if (skillBar != null)
                {
                    skillBar.SetCooldown(0f);
                }
            }
        }
    }

    void TriggerSkill()
    {
        canUseSkill = false;
        if (animator != null)
        {
            animator.SetTrigger("Skill"); // Animation will call TriggerAOEBlast via event
        }
        if (skillBar != null) { 
            skillBar.SetCooldown(skillCooldown);
        }

    }

    // Called by animation event!
    public void TriggerAOEBlast()
    {
        Vector3 spawnPos = transform.position + transform.forward * 1.5f;
        Instantiate(aoeHitboxPrefab, spawnPos, Quaternion.identity);
        // Trigger camera shake
        if (CameraShakeController.Instance != null)
        {
            CameraShakeController.Instance.ShakeCamera(15f, 0.3f);
        }
        //Instantiate(aoeHitboxPrefab, spawnPos, Quaternion.LookRotation(transform.forward));
    }
}
