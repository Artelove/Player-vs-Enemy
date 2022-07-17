using UnityEngine;

[RequireComponent(typeof(MovementView))]
public class PlayerMovement : PhisicsMovement
{
    private MovementView _movementView;
    private void Awake()
    {
        _movementView = GetComponent<MovementView>();
        _movementView.SetStartDirection(MovementView.Direction.right);
    }
}
