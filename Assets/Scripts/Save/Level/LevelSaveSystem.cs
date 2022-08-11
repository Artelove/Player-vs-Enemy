using System.IO;
using UnityEngine;

namespace Save
{
    public class LevelSaveSystem : ISaveSystem<LevelSaveData>
    {
        private string _filePath;
        private string _levelName;
        public LevelSaveSystem(string levelName)
        {
            _levelName = levelName; 
            _filePath = Application.persistentDataPath + levelName + ".json";
        }

        public void Save(LevelSaveData data)
        {
            var json = JsonUtility.ToJson(data);
            if (File.Exists(_filePath))
            {
                using (var writter = new StreamWriter(_filePath))
                {
                    writter.WriteLine(json);
                }
            }
            else
            {
                using (StreamWriter writter = File.CreateText(_filePath))
                {
                    writter.WriteLine(json);
                }
            }
        }

        public LevelSaveData Load()
        {
            string json = "";
            if (File.Exists(_filePath))
            {
                using (var reader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        json += line;
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(_filePath))
                {
                    
                }
            }

            if (string.IsNullOrEmpty(json))
            {
                return new LevelSaveData(_levelName);
            }

            return JsonUtility.FromJson<LevelSaveData>(json);
        }
        
    }
}