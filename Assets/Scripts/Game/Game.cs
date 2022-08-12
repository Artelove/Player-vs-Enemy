
using System;
using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelController))]
public class Game : MonoBehaviour
{
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private EndLevelUIPresenter _endLevelUI;
    private LevelController _levelController;
    private LevelSaveSystem _levelSaveSystem;
    private LevelSaveData _levelSaveData;

    private void Awake()
    {
        _levelController = GetComponent<LevelController>();
    }

    private void Start()
    {
        _sceneController.CurrentScene = SceneManager.GetActiveScene();
        _levelSaveSystem = new LevelSaveSystem(_sceneController.CurrentScene.name);
        _levelSaveData = _levelSaveSystem.Load();
        _endLevelUI.View.NextLevel.onClick.AddListener(_sceneController.NextLevel);
        _endLevelUI.View.RestartLevel.onClick.AddListener(_sceneController.RestartLevel);
        _endLevelUI.View.LevelMenu.onClick.AddListener(_sceneController.LoadLevelMenu);
    }

    private void OnEnable()
    {
        _levelController.LevelLosed += Lose;
        _levelController.LevelDone += Win;
    }

    private void Lose()
    {
        _levelSaveData.AttemptsToWin[^1]++;
        _endLevelUI.Model.SetLevelData(_levelSaveData);
        EndLevel();
    }

    private void Win()
    {
        _levelSaveData.IsLevelDone = true;
        _endLevelUI.Model.SetLevelData(_levelSaveData);
        _levelSaveData.AttemptsToWin.Add(0);
        EndLevel();
    }

    private void EndLevel()
    {
        
        _levelSaveSystem.Save(_levelSaveData);
        _endLevelUI.Show();
    }
}
