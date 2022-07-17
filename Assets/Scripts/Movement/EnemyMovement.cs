using UnityEngine;

[RequireComponent(typeof(MovementView))]
public class EnemyMovement : PhisicsMovement
{
    private MovementView _movementView;
    private void Awake()
    {
        _moveSpeed *= -1;
        _movementView = GetComponent<MovementView>();
        _movementView.SetStartDirection(MovementView.Direction.left);
    }
}
