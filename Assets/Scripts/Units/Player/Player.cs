using System;
using Movement;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private UnitPhysicsConfig _physicsConfig;
    private MovementView _movementView;
    private PhysicsModel _physicsModel;
    private PhysicsInput _input;
    
    public event Action Destroyed;
    private void Awake()
    {
        _input = GetComponent<PhysicsInput>();
        _movementView = GetComponent<MovementView>();
        _input.HorizintalDirectionChanged += _movementView.ChangeVisual;
        _physicsModel = new PhysicsModel(_physicsConfig, GetComponent<Rigidbody2D>(), _input);
    }
    private void FixedUpdate()
    {
        _physicsModel.UpdatePosition(Time.deltaTime);
    }
    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}
