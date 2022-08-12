using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCellPresenter:MonoBehaviour
{
    [SerializeField] private LevelCellView _view;
    private LevelCellModel _model;
    public LevelCellView View
    {
        get => _view;
    }
    public void Render(LevelCellModel model)
    {
        _model = model;
        _view.Render(model);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_model.Level.name);
    }

    public void Init(LevelCellModel model)
    {
        Render(model);
        _view.LoadSceneButton.onClick.AddListener(LoadScene);
    }
}
