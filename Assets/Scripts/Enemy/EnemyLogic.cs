using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private float enemyHealth = 1f;
    [SerializeField] private float enemyDamage = 5f;

    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
       TowerLogic tower = FindObjectOfType<TowerLogic>();
        if (tower != null)
        {
            target = tower.transform;
        } else
        {
            Debug.LogError("NoTowerLogic component found!");
        }
    }

    private void Update()
    {
        Pursuetower();
    }

    private void Pursuetower()
    {
        if (target == null) return;

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        rb.velocity = direction * enemySpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyLogic>() != null)
        {
            // Resolve overlap without destroying the enemy
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }

       TowerLogic tower = collision.gameObject.GetComponent<TowerLogic>();
        if (tower != null)
        {
            tower.TakeDamage(enemyDamage);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
