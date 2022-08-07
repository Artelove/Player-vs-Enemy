using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy: MonoBehaviour
{
    [SerializeField] private UnitPhysicsConfig _physicsConfig;
    private MovementView _movementView;
    private PhysicsModel _physicsModel;
    private PhysicsInput _input;

    public event Action PlayerTounched;

    private void Start()
    {
        _input = GetComponent<PhysicsInput>();
        _movementView = GetComponent<MovementView>();
        _input.HorizintalDirectionChanged += _movementView.ChangeVisual;
        _physicsConfig = new UnitPhysicsConfig(_physicsConfig);
        
        _physicsConfig.MoveSpeed *= -1;
        _physicsModel = new PhysicsModel(_physicsConfig, GetComponent<Rigidbody2D>(), _input);
    }

    private void FixedUpdate()
    {
        _physicsModel.UpdatePosition(Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
            PlayerTounched?.Invoke();
    }
}

