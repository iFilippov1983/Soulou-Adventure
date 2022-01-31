using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(menuName = "GameData/GameProgressData", fileName = "GameProgressData")]
    public class GameProgressData : ScriptableObject
    {
        [SerializeField] private int _lives = 3;
        [SerializeField] private int _scores = 0;
        [SerializeField] private int _currentLevel = 1;
        [SerializeField] private int _scoresForCompleteLevel = 1000;

        public ref int Lives => ref _lives;
        public int Scores => _scores;
        public int CurrentLevel => _currentLevel;
        public int ScoresForCompleteLevel => _scoresForCompleteLevel;
    }
}
