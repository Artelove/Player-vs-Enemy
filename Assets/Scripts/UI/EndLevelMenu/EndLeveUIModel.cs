using System.Collections;
using System.Collections.Generic;
using Save;
using UnityEngine;

public class EndLeveUIModel
{
    private int _attempts;
    private bool _isLevelDone;

    public int Attempts
    {
        get => _attempts;
        set => _attempts = value;
    }

    public bool IsLevelDone
    {
        get => _isLevelDone;
        set => _isLevelDone = value;
    }

    public void SetLevelData(LevelSaveData data)
    {
        _attempts = data.AttemptsToWin[^1];
        _isLevelDone = data.IsLevelDone;
    }
}
