using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Levels/Level", order = 1)]
public class LevelCellModel: ScriptableObject
{
    [SerializeField] private SceneAsset _level;
    [SerializeField] private  int _levelNumber;
    [SerializeField] private Sprite _background;
    [SerializeField] private string _levelDescription;

    public int LevelNumber
    {
        get => _levelNumber;
        set
        {
            if(value>0)
                _levelNumber = value;
            else
            {
                throw new InvalidDataException();
            }
        }
    }
    public SceneAsset Level
    {
        get => _level;
        set => _level = value;
    }

    public Sprite Background
    {
        get => _background;
        set => _background = value;
    }

    public string LevelDescription
    {
        get => _levelDescription;
        set => _levelDescription = value;
    }

}
