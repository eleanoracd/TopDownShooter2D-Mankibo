using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    public float Damage { get; protected set; }
    public float Cooldown { get; protected set; }

    public abstract void Initialize(Vector3 spawnPosition, Vector3 direction);
}
