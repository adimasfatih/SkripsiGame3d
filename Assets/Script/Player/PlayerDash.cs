using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    PlayerMovement moveScript;
    public float dashSpeed;
    public float dashTime;
    public DodgeBar dodgeBar;
    private bool canDash = true;
    public float dashCooldown = 2f;
    private float currentCooldown = 0f;
    public Animator animator;
    private bool isDashing = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<PlayerMovement>();
        if (dodgeBar != null)
        {
            dodgeBar.SetMaxCooldown(dashCooldown);
        }

    }

    void Update()
    {
        if (inputManager._Dodge && canDash)
        {
            StartCoroutine(Dash());
        }

        if (!isDashing) // ← Add this condition
        {
            moveScript.UpdateAnimationState();
        }

        if (!canDash && dodgeBar != null)
        {
            currentCooldown += Time.deltaTime;
            dodgeBar.SetCooldown(currentCooldown);
        }
    }

    IEnumerator Dash()
    {
        {
            canDash = false;
            isDashing = true;
            currentCooldown = 0f;

            // i-frame
            PlayerHealth health = GetComponent<PlayerHealth>();
            if (health != null)
                health.isInvincible = true;

            // animation
            if (animator != null)
                animator.SetBool("IsDashing", true);

            float startTime = Time.time;
            while (Time.time < startTime + dashTime)
            {
                moveScript.controller.Move(moveScript.moveDirection * dashSpeed * Time.deltaTime);
                yield return null;
            }

            // end dash
            if (animator != null)
                animator.SetBool("IsDashing", false);

            if (health != null)
                health.isInvincible = false;

            isDashing = false;

            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
            currentCooldown = dashCooldown;
            if (dodgeBar != null)
                dodgeBar.SetCooldown(dashCooldown);
        }
    }
}