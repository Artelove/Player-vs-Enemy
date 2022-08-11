using System.Collections.Generic;

namespace Save
{
    public class LevelSaveData
    {
        public string Name;
        public List<int> AttemptsToWin;
        public bool IsLevelDone;

        public int LastAttemptCount
        {
            get
            {
                if (AttemptsToWin == null)
                {
                    AttemptsToWin = new List<int>();
                    AttemptsToWin.Add(0);
                }

                return AttemptsToWin[^1];
            }
        }

        public LevelSaveData(string name, List<int> attemptsToWin = null, bool isLevelDone = false)
        {
            Name = name;
            AttemptsToWin = attemptsToWin;
            IsLevelDone = isLevelDone;
        }
    }
}