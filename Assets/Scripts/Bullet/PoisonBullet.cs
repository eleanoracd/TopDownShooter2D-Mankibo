using System.Collections;
using UnityEngine;

public class PoisonBullet : BaseBullet
{
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletCooldown = 5f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float poisonDuration = 4.5f; // Duration of poison mist
    [SerializeField] private float poisonTickDamage = 5f; // Damage per tick
    [SerializeField] private float tickInterval = 1.5f; // Time between ticks
    [SerializeField] private float mistRadius = 20f; // Radius of the poison mist
    [SerializeField] private GameObject poisonEffectPrefab; // Optional visual effect for poison mist

    private Vector3 spawnPoint;

    private void Awake()
    {
        Damage = poisonTickDamage;
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
        EnemyLogic enemy = collision.GetComponent<EnemyLogic>();
        if (enemy != null)
        {
            SpreadPoison();
        }
    }

    private void SpreadPoison()
    {
        // Instantiate poison mist effect (optional)
        if (poisonEffectPrefab != null)
        {
            Instantiate(poisonEffectPrefab, transform.position, Quaternion.identity);
        }

        // Apply poison effect over time to all enemies within the mist radius
        StartCoroutine(ApplyPoisonEffect());

        // Destroy the bullet after spreading the poison
        Destroy(gameObject);
    }

    private IEnumerator ApplyPoisonEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < poisonDuration)
        {
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, mistRadius);
            foreach (var hit in hitObjects)
            {
                EnemyLogic enemy = hit.GetComponent<EnemyLogic>();
                if (enemy != null)
                {
                    enemy.TakeDamage(Damage);
                }
            }

            elapsedTime += tickInterval;
            yield return new WaitForSeconds(tickInterval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, mistRadius);
    }
}