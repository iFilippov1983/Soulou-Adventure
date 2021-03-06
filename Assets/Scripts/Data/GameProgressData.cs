using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(menuName = "GameData/GameProgressData", fileName = "GameProgressData")]
    public class GameProgressData : ScriptableObject
    {
        [SerializeField] private int _currentLevel = 1;
        [SerializeField] private int _scoresOnStart = 1000;
        [SerializeField] private int _scoresForCompleteLevel = 100;
        [SerializeField] private int _penaltyScores = 10;

        public int CurrentLevel { get { return _currentLevel; } set { _currentLevel = value; } }
        public int ScoresOnStart => _scoresOnStart;
        public int ScoresForCompleteLevel => _scoresForCompleteLevel;
        public int PenaltyScores => _penaltyScores;
    }
}
