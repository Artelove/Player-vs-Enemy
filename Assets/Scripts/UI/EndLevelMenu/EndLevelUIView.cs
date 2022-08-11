
using System;
using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using Button = UnityEngine.UI;

[Serializable]
public class EndLevelUIView
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _attemptsCount;
    [SerializeField] private Button.Button _nextLevel;
    [SerializeField] private Button.Button _restartLevel;
    [SerializeField] private Button.Button _levelMenu;

    
    public Button.Button NextLevel => _nextLevel;
    public Button.Button RestartLevel => _restartLevel;
    public Button.Button LevelMenu => _levelMenu;

    private void Start()
    {
        _canvasGroup.gameObject.SetActive(false);
        _canvasGroup.DOFade(0, 0);
    }

    public void Render(EndLeveUIModel model)
    {
        _attemptsCount.text = model.Attempts.ToString();
        _nextLevel.gameObject.SetActive(model.IsLevelDone);
    }

    public void Show()
    {
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 1);
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, 1);
        _canvasGroup.gameObject.SetActive(false);
    }
}
