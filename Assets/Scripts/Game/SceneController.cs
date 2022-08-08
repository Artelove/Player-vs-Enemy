using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    private Scene _currentScene; 
    public void RestartLevel(Scene scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel(Scene scene)
    {
        Debug.Log("Win");
    }
}
