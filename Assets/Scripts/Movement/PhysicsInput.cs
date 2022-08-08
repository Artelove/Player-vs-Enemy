using System;
using UnityEngine;

namespace Movement
{
    public class PhysicsInput: MonoBehaviour
    {
        private MovementView.Direction _currentDirection;
        public event Action HorizintalDirectionChanged;

        private float _horizontalAxis;
        private float _verticalAxis;

        public MovementView.Direction Direction
        {
            get => _currentDirection;
            set
            {
                if(_currentDirection != value)
                    HorizintalDirectionChanged?.Invoke();
                _currentDirection = value;
            }
        }
        public float HorizontalAxis
        {
            get => _horizontalAxis;
            private set => _horizontalAxis = value;
        }

        public float VerticalAxis
        {
            get => _verticalAxis;
            private set => _verticalAxis = value;
        }

        private void FixedUpdate()
        {
            HorizontalAxis = Input.GetAxis("Horizontal");
            if (HorizontalAxis > 0)
                Direction = MovementView.Direction.left;
            else if(HorizontalAxis < 0)
                Direction = MovementView.Direction.right;
            VerticalAxis = Input.GetAxis("Vertical");
        }
    }
}
