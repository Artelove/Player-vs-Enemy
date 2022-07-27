using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PhisicsMovement>(out PhisicsMovement phisicsMovement))
            Destroy(collision.gameObject);
    }
}
