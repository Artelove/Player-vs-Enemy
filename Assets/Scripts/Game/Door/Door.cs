using System;
using Units;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public event Action<Unit> ReachedDoor;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit unit))
            ReachedDoor?.Invoke(unit);
    }
}
