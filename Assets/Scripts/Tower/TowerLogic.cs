using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : MonoBehaviour
{
    [SerializeField] private float towerHealth = 100f;

    public void TakeDamage(float damage)
    {
        towerHealth -= damage;
        Debug.Log($"Tower Health: {towerHealth}");

        if (towerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("GameOver!");
    }
}
