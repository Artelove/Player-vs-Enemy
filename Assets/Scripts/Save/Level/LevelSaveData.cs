using System.Collections.Generic;

namespace Save
{
    public class LevelSaveData
    {
        public string Name;
        public List<int> AttemptsToWin;
        public bool IsLevelDone;
        private int _lastAttemptCount;

        public int LastAttemptCount => AttemptsToWin[^1];

        public int LastWinAttemptCount
        {
            get { 
                if(AttemptsToWin.Count>=2)
                        return AttemptsToWin[^2];
                else
                    return 0;
            }
        }

        public LevelSaveData(string name, List<int> attemptsToWin = null, bool isLevelDone = false)
        {
            Name = name;
            AttemptsToWin = attemptsToWin;
            if (attemptsToWin == null)
            {
                AttemptsToWin = new List<int>();
                AttemptsToWin.Add(0);
            }

            IsLevelDone = isLevelDone;
        }
    }
}