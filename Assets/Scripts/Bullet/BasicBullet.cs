using UnityEngine;

public class BasicBullet : BaseBullet
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float maxDistance = 50f;

    private Vector3 spawnPoint;

    private void Awake()
    {
        Damage = 10f;
        Cooldown = 0.25f;
    }

    public override void Initialize(Vector3 spawnPosition, Vector3 direction)
    {
        transform.position = spawnPosition;
        spawnPoint = spawnPosition;
        GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
    }

    private void Update()
    {
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
            enemy.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
