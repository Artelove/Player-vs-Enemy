using System;
using Movement;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        protected MovementView MovementView;
        protected PhysicsModel PhysicsModel;
        protected PhysicsInput Input;
        protected void Init(UnitPhysicsConfig unitPhysicsConfig)
        {
            MovementView = GetComponent<MovementView>();
            Input = GetComponent<PhysicsInput>();
            Input.SetStartDirection(MovementView.MoveDirection);
            Input.HorizontalDirectionChanged += MovementView.ChangeVisual;
            PhysicsModel = new PhysicsModel(unitPhysicsConfig, GetComponent<Rigidbody2D>(), Input);
        }
        private void FixedUpdate()
        {
            PhysicsModel.UpdatePosition(Time.deltaTime);
        }
    }
}
