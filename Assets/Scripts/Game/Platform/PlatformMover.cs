using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Units;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private Transform _endPosition;
    [SerializeField] private GameObject _platform;
    [SerializeField] private float _speedDuration;

    private void Start()
    {
        _platform.transform.DOPath(new Vector3[]{_endPosition.position, transform.position}, _speedDuration)
            .SetDelay(1f).SetEase(Ease.Linear).SetLoops(-1);
    }
}
