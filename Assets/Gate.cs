
using DG.Tweening;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum GateState{ isUp, isDown }

    [SerializeField] private GateState _state = GateState.isUp;
    [SerializeField] private int _buttonTriggerId = -1;
    [SerializeField] private Vector3 _loweredGatePoint;
    [SerializeField] private Vector3 _raiseGatePoint;

    public int ButtonTriggerId { get => _buttonTriggerId; set => _buttonTriggerId = value; }


    private void Awake()
    {
        _raiseGatePoint = transform.position;
        _loweredGatePoint = transform.position + new Vector3(0, -2, 0);
    }
    public void RaiseGate()
    {
        transform.DOMove(_raiseGatePoint, 1);
    }
    public void LowerGate()
    {
        transform.DOMove(_loweredGatePoint, 1);
    } 
}
