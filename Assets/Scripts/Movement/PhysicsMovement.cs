
using System.Collections.Generic;
using Movement;
using Units;
using UnityEngine;

public class PhysicsMovement
{
    private LayerMask _layerMask;
    protected float _moveSpeed;
    protected float _jumpForce;
    private float _minGroundNormalY = .65f;
    private float _gravityModifier = 1f;

    private Vector2 _velocity;
    private Vector2 _targetVelocity; 

    protected bool grounded; 
    protected Vector2 groundNormal; 
    protected Rigidbody2D rb2d; 
    protected ContactFilter2D contactFilter; 
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16]; 
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f; 
    protected const float shellRadius = 0.01f;

    public Vector2 TargetVelocity { get => _targetVelocity; set => _targetVelocity = value; }

    public PhysicsMovement(UnitPhysicsConfig unitPhysicsConfig, Rigidbody2D rigidbody2D)
    {
        _layerMask = unitPhysicsConfig.LayerMask;
        _moveSpeed = unitPhysicsConfig.MoveSpeed;
        _jumpForce = unitPhysicsConfig.JumpForse;
        _minGroundNormalY = unitPhysicsConfig.MinGroundNormalY;
        _gravityModifier = unitPhysicsConfig.GravityModifier;
        rb2d = rigidbody2D;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(_layerMask);
        contactFilter.useLayerMask = true;
    }
    public void Movement(float horizontalAxis, float verticalAxis, float time)
    {
        TargetVelocity = new Vector2(horizontalAxis * _moveSpeed, 0);
        if(verticalAxis > 0 && grounded)
            _velocity.y = _jumpForce;
        
        _velocity += Physics2D.gravity * (_gravityModifier * time); 
        _velocity.x = TargetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = _velocity * time; 
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x); 
        Vector2 move = moveAlongGround * deltaPosition.x;

        Move(move, false);
        move = Vector2.up * deltaPosition.y;
        Move(move, true);
    }
    private void Move(Vector2 move, bool yMovement) {
        float distance = move.magnitude;
        if (distance > minMoveDistance) 
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                } 

                float modifiedDistance = hitBufferList[i].distance - shellRadius; 
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rb2d.position += move.normalized * distance;
    }
}
