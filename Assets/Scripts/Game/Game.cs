
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelController))]
public class Game : MonoBehaviour
{
    private LevelController _levelController;
    private SceneController _sceneController;
    private void Awake()
    {
        _levelController = GetComponent<LevelController>();
        _sceneController = new SceneController();
    }

    private void OnEnable()
    {
        _levelController.LevelLosed += LosedLevel;
        _levelController.LevelDone += WinLevel;
    }

    private void LosedLevel()
    {
        _sceneController.RestartLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void WinLevel()
    {
        _sceneController.NextLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
