using UnityEngine;

public class ExplosiveBullet : BaseBullet
{
    [SerializeField] private float bulletSpeed = 10f; 
    [SerializeField] private float maxDistance = 25f; 
    [SerializeField] private float bulletDamage = 50f;
    [SerializeField] private float bulletCooldown = 1f;
    [SerializeField] private float explosionRadius = 3f; 
    [SerializeField] private GameObject explosionEffect;

    private Vector3 spawnPoint;

    private void Awake()
    {
        Damage = bulletDamage;
        Cooldown = bulletCooldown;
    }

    public override void Initialize(Vector3 spawnPosition, Vector3 direction)
    {
        transform.position = spawnPosition;
        spawnPoint = spawnPosition;
        GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
    }

    private void Update()
    {
        // Destroy the bullet if it exceeds the maximum range
        if (Vector3.Distance(spawnPoint, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy or an obstacle
        EnemyLogic enemy = collision.GetComponent<EnemyLogic>();
        if (enemy != null)
        {
            Explode();
        }

        
    }

    private void Explode()
    {
        // // Instantiate the explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Find all enemies within the explosion radius
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hitObjects)
        {
            EnemyLogic enemy = hit.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the explosion radius in the scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
