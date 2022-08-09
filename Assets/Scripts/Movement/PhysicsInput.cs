using System;
using UnityEngine;

namespace Movement
{
    public class PhysicsInput: MonoBehaviour
    {
        private MovementView.Direction _currentDirection;
        private MovementView.Direction _startDirection;

        public Action HorizontalDirectionChanged;

        public MovementView.Direction CurrentDirection
        {
            get => _currentDirection;
            set
            {
                if(value != CurrentDirection)
                    HorizontalDirectionChanged?.Invoke();
                _currentDirection = value;
            }
        }
        public float HorizontalAxis{ get; private set; }

        public float VerticalAxis { get; private set; }
        
        public void SetStartDirection(MovementView.Direction direction)
        {
            _startDirection = direction;
            _currentDirection = direction;
        }
        
        private void FixedUpdate()
        {
            HorizontalAxis = Input.GetAxis("Horizontal");
            if (HorizontalAxis > 0)
                CurrentDirection = _startDirection == MovementView.Direction.Right ? MovementView.Direction.Right : MovementView.Direction.Left;
            else if(HorizontalAxis < 0)
                CurrentDirection = _startDirection == MovementView.Direction.Right ? MovementView.Direction.Left : MovementView.Direction.Right;
            VerticalAxis = Input.GetAxis("Vertical");
        }


    }
}
