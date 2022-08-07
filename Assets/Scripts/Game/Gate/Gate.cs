
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Gate : MonoBehaviour, IActivable, IDeActivable
{
    [SerializeField] private List<GameObject> _activators;
    [SerializeField] private List<GameObject> _deActivators;
    [SerializeField] private GameObject gate;
    [SerializeField] private Transform _loweredGatePoint;
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
        gate.transform.DOMove(_raiseGatePoint.position, 1);
    }
    public void LowerGate()
    {
        gate.transform.DOMove(_loweredGatePoint.position, 1);
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




