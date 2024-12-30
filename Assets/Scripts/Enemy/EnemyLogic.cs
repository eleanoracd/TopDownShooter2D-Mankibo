using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private int enemyDamage = 1;

    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerLogic player = FindObjectOfType<PlayerLogic>();
        if (player != null)
        {
            target = player.transform;
        } else
        {
            Debug.LogError("No PlayerLogic component found!");
        }
    }

    private void Update()
    {
        PursuePlayer();
    }

    private void PursuePlayer()
    {
        if (target == null) return;

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        rb.velocity = direction * enemySpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLogic player = collision.gameObject.GetComponent<PlayerLogic>();
        if (player != null)
        {
            Debug.Log("Player takes damage: " + enemyDamage);
        }

        Destroy(gameObject);
    }
}
