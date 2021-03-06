using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementCameraTrigger : MonoBehaviour
{
    [SerializeField] private TriggerType triggerType;

    private Vector2 _triggerSize;
    public Vector2 TriggerSize { get => _triggerSize; }

    private UnityEvent _movementCameraTriggered = new UnityEvent();
    public event UnityAction MovementCameraTriggered
    {
        add => _movementCameraTriggered.AddListener(value);
        remove => _movementCameraTriggered.RemoveListener(value);
    }
    private enum TriggerType { onExit, onEnter, onStay};
    private MovementCamera2D _movementCamera;
    // Start is called before the first frame update
    void Start()
    {
        var collider = GetComponent<BoxCollider2D>();
        _triggerSize = new Vector2 (transform.localScale.x, transform.localScale.y);
        _movementCamera = GetComponentInParent<MovementCamera2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerType == TriggerType.onEnter)
            _movementCameraTriggered.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerType == TriggerType.onExit)
            _movementCameraTriggered.Invoke();
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (triggerType == TriggerType.onStay)
            _movementCameraTriggered.Invoke();
    }
}
