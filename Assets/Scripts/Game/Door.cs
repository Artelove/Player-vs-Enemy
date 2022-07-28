using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    private UnityEvent<GameObject> reachedDoor = new UnityEvent<GameObject>();

    public event UnityAction<GameObject> ReachedDoor
    {
        add => reachedDoor.AddListener(value);
        remove => reachedDoor.RemoveListener(value);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicsMovement phisicsMovement))
            reachedDoor.Invoke(collision.gameObject);

    }
}
