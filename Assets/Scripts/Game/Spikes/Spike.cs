using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.Events;

public class Spike : MonoBehaviour
{
    public event Action<Unit> SpikesTouched;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit unit))
            SpikesTouched?.Invoke(unit);
    }
}
