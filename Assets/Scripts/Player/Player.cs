using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private UnityEvent _objectDestroyed = new UnityEvent();

    public event UnityAction ObjectDestroyed
    {
        add => _objectDestroyed.AddListener(value);
        remove => _objectDestroyed.RemoveListener(value);
    }
    private void OnDestroy()
    {
        _objectDestroyed.Invoke();
    }
}
