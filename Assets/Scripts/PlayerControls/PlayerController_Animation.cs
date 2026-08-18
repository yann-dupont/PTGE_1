using UnityEngine;

public partial class PlayerController
{
     private Animator animator;

    private void Awake_Animation()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void PlayMovementAnimation()
    {
        if (animator == null)
            return;

        Vector2 horizontalMovement = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z);
        animator.SetBool("isMoving", (int)horizontalMovement.magnitude != 0);
    }

    private void PlayNinjaSignAnimation(NinjaSignDescriptor sign)
    {
        if (animator == null || sign == null)
            return;

        if (string.IsNullOrEmpty(sign.AnimationTrigger))
            return;

        animator.SetTrigger(sign.AnimationTrigger);
    }
}
