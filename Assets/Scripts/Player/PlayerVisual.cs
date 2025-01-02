using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Animator animator; 
    [SerializeField] private Transform visual;
    private const string IdleAnimation = "Idle";
    private const string AttackAnimation = "Attack";

    private static readonly int AttackHash = Animator.StringToHash(AttackAnimation);

    public void RotateVisual(Vector2 lookDirection)
    {
        // Calculate angle from direction and apply rotation to the visual object
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        visual.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void PlayIdleAnimation()
    {
        if (animator != null)
        {
            animator.Play(IdleAnimation);
        }
    }

    public void PlayAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(AttackHash);
        }
    }

    public bool IsAttacking()
    {
        if (animator == null) return false;

        // Use GetCurrentAnimatorStateInfo and check if the current animation state is Attack
        return animator.GetCurrentAnimatorStateInfo(0).IsName(AttackAnimation);
    }
}   