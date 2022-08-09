using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    private Scene _currentScene; 
    public void RestartLevel(int currentIndex)
    {
        SceneManager.LoadScene(currentIndex);
    }

    public void NextLevel(int currentIndex)
    {
        if(currentIndex+1==7) return;
        SceneManager.LoadScene(currentIndex+1);
    }
}
