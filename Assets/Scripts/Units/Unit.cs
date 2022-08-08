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
            Input = GetComponent<PhysicsInput>();
            MovementView = GetComponent<MovementView>();
            Input.HorizintalDirectionChanged += MovementView.ChangeVisual;
            PhysicsModel = new PhysicsModel(unitPhysicsConfig, GetComponent<Rigidbody2D>(), Input);
        }
        private void FixedUpdate()
        {
            PhysicsModel.UpdatePosition(Time.deltaTime);
        }
    }
}
