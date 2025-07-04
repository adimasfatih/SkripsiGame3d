using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [Header("References")]
    public Camera cam; // Assign your main camera here
    public LayerMask groundLayer;

    private PlayerCombat playerCombat;
    private Animator animator;

    private void Start()
    {
        playerCombat = GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsPlayingAttackAnimation())
        {
            RotateTowardsMouse();
        }
    }

    private bool IsPlayingAttackAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsTag("Attack");
    }

    void RotateTowardsMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 targetPoint = hit.point;
            Vector3 direction = (targetPoint - transform.position).normalized;
            direction.y = 0;

            if (direction.magnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
            }
        }
    }
}
