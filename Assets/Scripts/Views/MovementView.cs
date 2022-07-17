using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhisicsMovement))]
public class MovementView : MonoBehaviour
{
    private PhisicsMovement _phisicsMovement;

    public enum Direction { right, left };

    

    private Direction moveDirection;
    private Direction previusMoveDirection;

    private UnityEvent directionMoveChanged = new UnityEvent();

    public Direction MoveDirection { get => moveDirection; set => moveDirection = value; }

    public event UnityAction DirectionMoveChanged
    {
        add => directionMoveChanged.AddListener(value);
        remove => directionMoveChanged.RemoveListener(value);
    }

    private void Awake()
    {
        _phisicsMovement = GetComponentInParent<PhisicsMovement>();
        DirectionMoveChanged += ChangeVisualDirection;
    } 

    private void Update()
    {
        if (_phisicsMovement.TargetVelocity.x != 0)
        {
            moveDirection = _phisicsMovement.TargetVelocity.x > 0 ? Direction.right : Direction.left;
            if (moveDirection != previusMoveDirection)
            {
                previusMoveDirection = moveDirection;
                directionMoveChanged.Invoke();
            }
        }
    }

    private void ChangeVisualDirection()
    {
        if (transform.rotation == Quaternion.identity)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.identity;
    }

    public void SetStartDirection(Direction direction)
    {
        previusMoveDirection = direction;
        moveDirection = direction;
    }


}
