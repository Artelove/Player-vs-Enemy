using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController:MonoBehaviour
{
    private Scene _currentScene;

    public Scene CurrentScene
    {
        get => _currentScene;
        set => _currentScene = value;
    }

    [SerializeField] private SceneAsset _nextScene;

    public void Awake()
    {
        CurrentScene = SceneManager.GetActiveScene();
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(_currentScene.name);
    }

    public void NextLevel()
    {
        if(_nextScene!=null)
            SceneManager.LoadSceneAsync(_nextScene.name);
    }
}
