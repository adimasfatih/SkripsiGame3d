using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;

public class isometricMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public float horizontalInput;
    public float verticalInput;

    public Vector3 moveDirection;

    private Animator animator; // Reference to Animator component


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // Initialize Animator component

    }

    private void Update()
    {

        FaceCursor();
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small constant to keep the player grounded
        }

        // Handle input and movement

        MovePlayer();

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Update animation state
        UpdateAnimationState();
    }

    public void MovePlayer()
    {
        if (IsPlayingAttackAnimation() || IsPlayingSkillAnimation())
        {
            moveDirection = Vector3.zero;
            return;
        }

        // Get input
        Vector3 input = new Vector3(inputManager.Movement.x, 0f, inputManager.Movement.y).normalized;

        // Get camera-relative directions
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Calculate movement direction
        moveDirection = camForward * input.z + camRight * input.x;

        // Move
        if (moveDirection != Vector3.zero)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }



    private void FaceCursor()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Vector3 direction = (targetPoint - transform.position).normalized;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
            }
        }
    }


    public void UpdateAnimationState()
    {


        Vector3 input = new Vector3(inputManager.Movement.x, 0f, inputManager.Movement.y).normalized;

        // Convert input direction to local space
        Vector3 localInput = transform.InverseTransformDirection(input);

        animator.SetFloat("MoveX", localInput.x, 0.1f, Time.deltaTime);
        animator.SetFloat("MoveZ", localInput.z, 0.1f, Time.deltaTime);
    }

    private bool IsPlayingAttackAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Attack");
    }

    private bool IsPlayingSkillAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Skill");
    }
}
