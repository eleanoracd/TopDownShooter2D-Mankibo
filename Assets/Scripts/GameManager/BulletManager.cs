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
    [SerializeField] private GameObject missileBulletPrefab;

    private BulletType currentBulletType = BulletType.Basic;
    private float nextFireTime = 0f;

    private void OnEnable()
    {
        InputManager.Instance.OnSwitchBullet += SwitchBullet;
        InputManager.Instance.OnShoot += Shoot;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnSwitchBullet -= SwitchBullet;
        InputManager.Instance.OnShoot -= Shoot;
    }

    private void SwitchBullet(int bulletIndex)
    {
        currentBulletType = (BulletType)(bulletIndex - 1);
        Debug.Log($"Switched to {currentBulletType} Bullet");
    }

    private void Shoot()
    {
        if (Time.time < nextFireTime) return;

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
            BulletType.Basic => basicBulletPrefab,
            BulletType.Laser => laserBulletPrefab,
            BulletType.Explosive => explosiveBulletPrefab,
            BulletType.Poison => poisonBulletPrefab,
            BulletType.Missile => missileBulletPrefab,
            _ => null,
        };
    }
}
