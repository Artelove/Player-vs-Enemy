using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Gate : MonoBehaviour, IActivable, IDeActivable
{
    [SerializeField] private List<GameObject> _activators = null;
    [SerializeField] private List<GameObject> _deActivators = null;
    [SerializeField] private GameObject gate;
    [SerializeField] private Transform _loweredGatePoint;
    [SerializeField] private float _changePositionDuration = 1.0f;
    private Transform _raiseGatePoint;
    
    private void Start()
    {
        _raiseGatePoint = transform;
        foreach (var activator in _activators)
            if(activator.TryGetComponent<IActivator>(out IActivator _activator))
                SetActivator(_activator);
        foreach (var deActivator in _deActivators)
            if(deActivator.TryGetComponent<IDeActivator>(out IDeActivator _deActivator))
                SetDeActivator(_deActivator);
    }
    public void RaiseGate()
    {
        gate.transform.DOMove(_raiseGatePoint.position, _changePositionDuration);
    }
    public void LowerGate()
    {
        gate.transform.DOMove(_loweredGatePoint.position, _changePositionDuration);
    }

    public void SetActivator(IActivator activator)
    {
        activator.Activated += LowerGate;
    }

    public void RemoveActivator(IActivator activator)
    {
        activator.Activated -= LowerGate;
    }
    public void SetDeActivator(IDeActivator deActivator)
    {
        deActivator.DeActivated += RaiseGate;
    }

    public void RemoveDeActivator(IDeActivator deActivator)
    {
        deActivator.DeActivated -= RaiseGate;
    }
}




