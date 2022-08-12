using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class LevelCellView
{
    [SerializeField] private Image _backGround;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private TMP_Text _levelDescription;
    [SerializeField] private Button _loadSceneButton;

    public Button LoadSceneButton => _loadSceneButton;

    public void Render(LevelCellModel model)
    {
        _backGround.sprite = model.Background;
        _levelNumber.text = model.LevelNumber.ToString();
        _levelDescription.text = model.LevelDescription;
    }
    
}
