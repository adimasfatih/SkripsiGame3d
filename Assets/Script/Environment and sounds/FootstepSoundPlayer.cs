using UnityEngine;

public class FootstepSoundPlayer : MonoBehaviour
{
    public float stepInterval = 0.5f;
    private float stepTimer;

    private PlayerMovement moveScript;
    private CharacterController controller;

    void Start()
    {
        moveScript = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && moveScript.moveDirection.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                SoundManager.Instance?.PlayFootstep(0.3f); // Play footstep at 50% volume
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // Reset when not moving or airborne
        }
    }
}
