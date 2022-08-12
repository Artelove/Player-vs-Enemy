using System;
using System.Collections.Generic;
using System.Linq;
using Save;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private LevelCellPresenter _template;
    [SerializeField] private Transform _container;
    [SerializeField] private List<LevelCellModel> _levelCellModels;

    
    private void Start()
    {
        FillContent(_levelCellModels);
    }
    private void FillContent(IEnumerable<LevelCellModel> levelCells)
    {
        SortByLevelNumber(levelCells);
        foreach (var cellModel in levelCells)
        {
            var levelSaveSystem = new LevelSaveSystem(cellModel.Level.name);
            var levelSaveData = levelSaveSystem.Load();
            if (levelSaveData.IsLevelDone)
            {
                var levelCell = Instantiate(_template, _container);
                levelCell.Init(cellModel);
            }
            else
            {
                var levelCell = Instantiate(_template, _container);
                levelCell.Init(cellModel);
                return;
            }
        }
    }

    private void SortByLevelNumber(IEnumerable<LevelCellModel> levelCellModels)
    {
        var cellModels = levelCellModels as LevelCellModel[] ?? levelCellModels.ToArray();
        for (int i = 0; i < cellModels.Length; i++)
        {
            for (int j = 0; j < cellModels.Length; j++)
            {
                if (cellModels[i].LevelNumber > cellModels[j].LevelNumber)
                    (cellModels[i], cellModels[j]) = (cellModels[j], cellModels[i]);
            }
        }
    }
}
