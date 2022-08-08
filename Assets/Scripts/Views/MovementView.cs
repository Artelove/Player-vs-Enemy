using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementView : MonoBehaviour
{
    [SerializeField] private Direction moveDirection; 
    
    private Transform _targetObject;
    
    public enum Direction { right, left };
    private Direction previusMoveDirection;
    public Action ChangeVisual;
    
    private void Awake()
    { 
        _targetObject = transform;
        ChangeVisual += ChangeVisualDirection;
    }

    public void ChangeVisualDirection()
    {
        moveDirection = moveDirection == Direction.left ? Direction.right : Direction.left;
        _targetObject.rotation =
            moveDirection == Direction.left ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }
}

