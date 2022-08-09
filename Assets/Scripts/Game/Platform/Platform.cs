using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Units;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    private Dictionary<Unit,Transform> _parents;
    private void Start()
    {
        _parents = new Dictionary<Unit, Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Unit unit))
        {
            _parents.Add(unit, unit.gameObject.transform.parent);
            unit.gameObject.transform.parent = transform;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            if (_parents.TryGetValue(unit, out Transform parent))
            {
                unit.gameObject.transform.parent = parent;
                _parents.Remove(unit);
            }
        }
    }
}
