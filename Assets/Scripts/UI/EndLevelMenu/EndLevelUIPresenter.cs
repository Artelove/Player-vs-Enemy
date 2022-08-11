using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class EndLevelUIPresenter : MonoBehaviour, IShowable
{
    [SerializeField] private EndLevelUIView _view;
    private EndLeveUIModel _model;

    public EndLevelUIView View => _view;
    public EndLeveUIModel Model => _model;
    
    private EndLevelUIPresenter()
    {
        _model = new EndLeveUIModel();
    }

    public void Show() 
    {
        _view.Show();
        _view.Render(_model);
    }

    public void Hide()
    {
        _view.Hide();
    }
}
