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
        protected Animator Animator;
        
        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int Killed = Animator.StringToHash("Killed");

        protected void Init(UnitPhysicsConfig unitPhysicsConfig)
        {
            MovementView = GetComponent<MovementView>();
            Input = GetComponent<PhysicsInput>();
            Animator = GetComponent<Animator>();
            Input.SetStartDirection(MovementView.MoveDirection);
            Input.HorizontalDirectionChanged += MovementView.ChangeVisual;
            PhysicsModel = new PhysicsModel(unitPhysicsConfig, GetComponent<Rigidbody2D>(), Input);
        }
        private void FixedUpdate()
        {
            PhysicsModel.UpdatePosition(Time.deltaTime);
            Animator.SetFloat(Speed,Math.Abs(Input.HorizontalAxis));
        }

        public void Destroy(float time)
        {
            PhysicsModel = null;
            Input = null;
            Destroy(gameObject, time);
        }
    }
}
