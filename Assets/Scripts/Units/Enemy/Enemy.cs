using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using Units;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy: Unit
{
    [SerializeField] private UnitPhysicsConfig _physicsConfig;
    public event Action PlayerTounched;

    private void Start()
    {
        _physicsConfig = new UnitPhysicsConfig(_physicsConfig);
        _physicsConfig.MoveSpeed *= -1;
        Init(_physicsConfig);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
            PlayerTounched?.Invoke();
    }
}

