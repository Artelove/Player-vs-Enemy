using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MovementView : MonoBehaviour
{
    [SerializeField] private Direction _moveDirection;
    
    public Direction MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }

    private Transform _targetObject;
    
    public enum Direction { Right, Left };

    public Action ChangeVisual;
    
    private void Awake()
    { 
        _targetObject = transform;
        ChangeVisual += ChangeVisualDirection;
        _targetObject.rotation =
            _moveDirection == Direction.Right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }

    public void ChangeVisualDirection()
    {
        _moveDirection = _moveDirection == Direction.Left ? Direction.Right : Direction.Left;
        _targetObject.rotation =
            _moveDirection == Direction.Right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }
}

