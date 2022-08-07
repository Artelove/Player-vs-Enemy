using UnityEngine;

namespace Movement
{
    public class PhysicsModel
    {
        private PhysicsMovement _phisicsMovement;
        private PhysicsInput _input;

        public PhysicsModel(UnitPhysicsConfig physicsConfig, Rigidbody2D rigidbody2D, PhysicsInput input)
        {
            _input = input;
            _phisicsMovement = new PhysicsMovement(physicsConfig, rigidbody2D);
        }
        
        public void UpdatePosition(float fixedTime)
        {
            _phisicsMovement.Movement(_input, fixedTime);
        }
    }
}