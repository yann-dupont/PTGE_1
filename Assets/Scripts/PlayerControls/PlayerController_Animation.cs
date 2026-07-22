using UnityEngine;

public partial class PlayerController
{
     private Animator animator;

    private void Awake_Animation()
    {
        animator = GetComponentInChildren<Animator>();
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
