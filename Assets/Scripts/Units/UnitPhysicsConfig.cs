using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Configs/UnitPhysicsConfig")]
public class UnitPhysicsConfig : ScriptableObject
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForse;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private float _minGroundNormalY;

    public UnitPhysicsConfig(LayerMask layerMask, float moveSpeed, float jumpForse, float gravityModifier, float minGroundNormalY)
    {
        _layerMask = layerMask;
        _moveSpeed = moveSpeed;
        _jumpForse = jumpForse;
        _gravityModifier = gravityModifier;
        _minGroundNormalY = minGroundNormalY;
    }
    public UnitPhysicsConfig(UnitPhysicsConfig unitPhysicsConfig)
    {
        _layerMask = unitPhysicsConfig.LayerMask;
        _moveSpeed = unitPhysicsConfig.MoveSpeed;
        _jumpForse = unitPhysicsConfig.JumpForse;
        _gravityModifier = unitPhysicsConfig.GravityModifier;
        _minGroundNormalY = unitPhysicsConfig.MinGroundNormalY;
    }

    public LayerMask LayerMask
    {
        get => _layerMask;
        set => _layerMask = value;
    }

    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public float JumpForse
    {
        get => _jumpForse;
        set => _jumpForse = value;
    }

    public float GravityModifier
    {
        get => _gravityModifier;
        set => _gravityModifier = value;
    }

    public float MinGroundNormalY
    {
        get => _minGroundNormalY;
        set => _minGroundNormalY = value;
    }
}
