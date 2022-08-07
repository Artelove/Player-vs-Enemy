using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PushButton : MonoBehaviour, IActivator, IDeActivator
{
    [SerializeField] private Sprite _pressedButton;
    [SerializeField] private Sprite _unPressedButton;
    
    private SpriteRenderer _spriteButton;

    public event Action Activated;
    public event Action DeActivated;
    
    private void Start()
    {
        _spriteButton = GetComponent<SpriteRenderer>();
        _spriteButton.sprite = _unPressedButton;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PressButton();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        UnPressButton();
    }

    private void PressButton()
    {
        _spriteButton.sprite = _pressedButton;
        Activated?.Invoke();
    }
    private void UnPressButton()
    {
        _spriteButton.sprite = _unPressedButton;
        DeActivated?.Invoke();
    }
}
