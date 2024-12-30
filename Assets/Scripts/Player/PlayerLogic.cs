using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnShoot += HandleShoot;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnShoot -= HandleShoot;
    }

    private void Update()
    {
        HandleMovement();
        HandleLookAt();
    }

    private void HandleMovement()
    {
        Vector2 input = InputManager.Instance.MovementInput;
        rb.velocity = input.normalized * moveSpeed;
    }

    private void HandleLookAt()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);

        Vector2 lookDirection = (mouseWorldPosition - transform.position).normalized;

        float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) - 90;

        transform.rotation = Quaternion.Euler(0, 0 , angle);
    }

    private void HandleShoot()
    {
        Debug.Log("Shoot!");
    }
}
