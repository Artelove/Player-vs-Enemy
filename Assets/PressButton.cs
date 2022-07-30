using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PressButton : MonoBehaviour
{
    [SerializeField] private Sprite _pressedButton;
    [SerializeField] private Sprite _unPressedButton;
    [SerializeField] private int _buttonId;

    private SpriteRenderer _spriteButton;

    private UnityEvent<int> _buttonPressed = new UnityEvent<int>();
    public event UnityAction<int> ButtonPressed
    {
        add => _buttonPressed.AddListener(value);
        remove => _buttonPressed.RemoveListener(value);
    }
    private UnityEvent<int> _buttonUnPressed = new UnityEvent<int>();
    public event UnityAction<int> ButtonUnPressed
    {
        add => _buttonUnPressed.AddListener(value);
        remove => _buttonUnPressed.RemoveListener(value);
    }
    // Start is called before the first frame update
    void Start()
    {
        _spriteButton = GetComponent<SpriteRenderer>();
        _spriteButton.sprite = _unPressedButton;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _spriteButton.sprite = _pressedButton;
        _buttonPressed.Invoke(_buttonId);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteButton.sprite = _unPressedButton;
        _buttonUnPressed.Invoke(_buttonId);
    }
}
