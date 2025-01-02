using UnityEngine;

public class LaserBullet : BaseBullet
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private LineRenderer lineRenderer; 
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private float bulletDamage = 20f;
    [SerializeField] private float bulletCooldown = .5f;


    private void Awake()
    {
        Damage = bulletDamage;
        Cooldown = bulletCooldown;
    }

    public override void Initialize(Vector3 spawnPosition, Vector3 direction)
    {
        transform.position = spawnPosition;
        FireLaser(direction);
    }

    private void FireLaser(Vector3 direction)
    {
        // Raycast to detect objects along the laser's path
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, hitLayers);

        Vector3 laserEndPoint = transform.position + direction.normalized * maxDistance;

        // If the laser hits something
        if (hit.collider != null)
        {
            laserEndPoint = hit.point;

            // Deal damage to the enemy if applicable
            EnemyLogic enemy = hit.collider.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }
        }

        // Draw the laser using LineRenderer
        DrawLaser(transform.position, laserEndPoint);

        // Destroy the laser object after a short time
        Destroy(gameObject, 0.1f);
    }

    private void DrawLaser(Vector3 start, Vector3 end)
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
    }
}
