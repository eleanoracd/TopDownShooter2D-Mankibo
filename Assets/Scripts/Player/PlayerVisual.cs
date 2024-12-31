using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private int currentDirection = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateDirection(Vector2 lookDirection)
    {
        int newDirection = DetermineDirection(lookDirection);

        if (newDirection != currentDirection)
        {
            currentDirection = newDirection;
            animator.SetInteger("Direction", currentDirection);
        }

        FlipSprite(lookDirection);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }

    private int DetermineDirection(Vector2 lookDirection)
    {
        if (Mathf.Abs(lookDirection.x) > Mathf.Abs(lookDirection.y))
        {
            return 2;
        }
        else
        {
            return lookDirection.y > 0 ? 1 : 0;
        }
    }

    private void FlipSprite(Vector2 lookDirection)
    {
        if (currentDirection == 2) // Side direction
        {
            Vector3 scale = transform.localScale;
            scale.x = lookDirection.x > 0 ? 1 : -1; // Flip horizontally for right-facing
            transform.localScale = scale;
        }
        else
        {
            // Reset flip for Up and Down
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
    }
}
