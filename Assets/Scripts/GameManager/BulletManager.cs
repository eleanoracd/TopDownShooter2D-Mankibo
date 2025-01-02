using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject basicBulletPrefab;
    [SerializeField] private GameObject laserBulletPrefab;
    [SerializeField] private GameObject explosiveBulletPrefab;
    [SerializeField] private GameObject poisonBulletPrefab;
    [SerializeField] private Sprite laserBulletPlaceholderSprite; // Placeholder for bullets without a sprite renderer

    private BulletType currentBulletType = BulletType.Basic;
    private float nextFireTime = 0f;

    private void OnEnable()
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError("InputManager.Instance is null. Ensure InputManager is initialized before BulletManager.");
            return;
        }

        InputManager.Instance.OnSwitchBullet += SwitchBullet;
        InputManager.Instance.OnShoot += Shoot;
    }

    private void OnDisable()
    {
        if (InputManager.Instance == null) return;

        InputManager.Instance.OnSwitchBullet -= SwitchBullet;
        InputManager.Instance.OnShoot -= Shoot;
    }

    public void SwitchBullet(int bulletIndex)
    {
        currentBulletType = (BulletType)(bulletIndex - 1);
        Debug.Log($"Switched to {currentBulletType} Bullet");
    }

    public void Shoot()
    {
        if (Time.time < nextFireTime) return;

        if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned!");
            return;
        }

        GameObject bulletPrefab = GetBulletPrefab();
        if (bulletPrefab == null) return;

        BaseBullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<BaseBullet>();
        bullet.Initialize(firePoint.position, firePoint.up);

        nextFireTime = Time.time + bullet.Cooldown;
    }

    private GameObject GetBulletPrefab()
    {
        return currentBulletType switch
        {
            BulletType.Basic => basicBulletPrefab ?? LogPrefabError(BulletType.Basic),
            BulletType.Laser => laserBulletPrefab ?? LogPrefabError(BulletType.Laser),
            BulletType.Explosive => explosiveBulletPrefab ?? LogPrefabError(BulletType.Explosive),
            BulletType.Poison => poisonBulletPrefab ?? LogPrefabError(BulletType.Poison),
            _ => null,
        };
    }

    private GameObject LogPrefabError(BulletType bulletType)
    {
        Debug.LogError($"Prefab for {bulletType} is not assigned in the BulletManager.");
        return null;
    }

    public Sprite GetBulletSprite(BulletType bulletType)
    {
        GameObject bulletPrefab = bulletType switch
        {
            BulletType.Basic => basicBulletPrefab,
            BulletType.Laser => laserBulletPrefab,
            BulletType.Explosive => explosiveBulletPrefab,
            BulletType.Poison => poisonBulletPrefab,
            _ => null,
        };

        if (bulletPrefab == null)
        {
            Debug.LogWarning($"No prefab found for {bulletType} bullet!");
            return null; // Return null or a placeholder sprite
        }

        SpriteRenderer spriteRenderer = bulletPrefab.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            return spriteRenderer.sprite;
        }

        if (bulletType == BulletType.Laser)
        {
            return laserBulletPlaceholderSprite; // Placeholder for Laser Bullet
        }

        Debug.LogWarning($"No SpriteRenderer found for {bulletType} bullet!");
        return null;
    }

    public BulletType GetCurrentBulletType() => currentBulletType;
}