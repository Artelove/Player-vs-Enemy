using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneController
{
    [SerializeField] private SceneAsset _nextScene;
    private Scene _currentScene;

    public Scene CurrentScene
    {
        get => _currentScene;
        set => _currentScene = value;
    }
    
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
