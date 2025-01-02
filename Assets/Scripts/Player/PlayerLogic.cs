using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private float playerMaxHealth = 100;

    private float playerCurrentHealth;

    private void Awake()
    {
        bulletManager = GetComponent<BulletManager>();
        playerVisual = GetComponentInChildren<PlayerVisual>(); // Ensure PlayerVisual is assigned
    }

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnShoot += HandleShoot;
            InputManager.Instance.OnSwitchBullet += HandleSwitchBullet;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnShoot -= HandleShoot;
            InputManager.Instance.OnSwitchBullet -= HandleSwitchBullet;
        }
    }

    private void Update()
    {
        HandleLookAt();
    }

    private void HandleLookAt()
    {
        if (InputManager.Instance == null) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
        mouseWorldPosition.z = 0;

        Vector2 lookDirection = (mouseWorldPosition - transform.position).normalized;

        // Rotate Player Visual
        playerVisual?.RotateVisual(lookDirection);

        float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void HandleShoot()
    {
        if (bulletManager != null)
        {
            bulletManager.Shoot();

            // Play attack animation
            playerVisual?.PlayAttackAnimation();
            Debug.Log("Shot!");
        }
    }

    private void HandleSwitchBullet(int bulletIndex)
    {
        bulletManager?.SwitchBullet(bulletIndex);
    }

    public void TakeDamage(float damage)
    {
        playerCurrentHealth -= damage;
        Debug.Log($"Player Health: {playerCurrentHealth}");

        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }

    public float GetCurrentHealth()
    {
        return playerCurrentHealth;
    }

    private void Die()
    {
        Debug.Log("Game Over!");
    }
}