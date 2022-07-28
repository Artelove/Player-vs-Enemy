using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spike : MonoBehaviour
{
    private UnityEvent<GameObject> _spikesTouched = new UnityEvent<GameObject>();

    public event UnityAction<GameObject> SpikesTouched
    {
        add => _spikesTouched.AddListener(value);
        remove => _spikesTouched.RemoveListener(value);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PhisicsMovement>(out PhisicsMovement phisicsMovement))
            _spikesTouched.Invoke(collision.gameObject);
    }
}
