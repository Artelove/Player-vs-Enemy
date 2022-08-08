using System;
using Movement;
using Units;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private UnitPhysicsConfig _physicsConfig;
    public event Action Destroyed;

    private void Start()
    {
        Init(_physicsConfig);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}
