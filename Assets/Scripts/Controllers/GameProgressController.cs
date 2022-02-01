using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Soulou
{
    public class GameProgressController : IInitialization
    {
        private const string SCORES = "Scores: ";
        private GameProgressData _progressData;
        private PlayerData _playerData;
        private PlayerInitializer _playerInitializer;
        private int _scores;
        private int _currentLevel;
        private int _scoresForCompleteLevel;
        private int _penaltyScores;
        private Text _scoresText;

        public Action<int> ScoresChanged; 

        public GameProgressController(GameData gameData, PlayerInitializer playerInitializer)
        {
            _progressData = gameData.GameProgressData;
            _playerInitializer = playerInitializer;
        }

        public void Initialize()
        {
            _scoresText = UnityEngine.Object.FindObjectOfType<Text>();
            NewGame();
        }

        public void LevelComplete()
        {
            _scores += _scoresForCompleteLevel;
            _scoresText.text = string.Concat(SCORES, _scores);
            ScoresChanged?.Invoke(_scores);
            Debug.Log("LEVEL COMPLETE!");
        }

        public void LevelFailed()
        {
            _scores -= _penaltyScores;
            _scoresText.text = string.Concat(SCORES, _scores);
            ScoresChanged?.Invoke(_scores);
            if (_scores <= 0) GameOver();
        }

        private void NewGame()
        {
            _scores = _progressData.ScoresOnStart;
            _scoresText.text = string.Concat(SCORES, _scores);

            _currentLevel = _progressData.CurrentLevel;
            _penaltyScores = _progressData.PenaltyScores;
            ScoresChanged?.Invoke(_scores);

            LoadLevel();
        }

        private void LoadLevel()
        {

        }

        private void InitializeNewLevel()
        {
            _scores = _progressData.ScoresOnStart;
            _currentLevel = _progressData.CurrentLevel;
        }

        private void GameOver()
        {
            Debug.Log("GAME OVER!");
        }
    }
}
