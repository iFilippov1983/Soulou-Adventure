using System;
using UnityEngine;
using UnityEngine.UI;

namespace Soulou
{
    public class GameProgressController : IInitialization
    {
        private const string SCORES = "Scores: ";
        private GameProgressData _progressData;
        private SceneData _sceneData;
        private PlayerData _playerData;
        private PlayerInitializer _playerInitializer;
        private int _scores;
        private int _scoresForCompleteLevel;
        private int _penaltyScores;
        private Text _scoresText;

        public Action<int> ScoresChanged;
        public Action<int> LevelChanged;

        public GameProgressController(GameData gameData, PlayerInitializer playerInitializer)
        {
            _sceneData = gameData.SceneData;
            _progressData = gameData.GameProgressData;
            _scoresForCompleteLevel = _progressData.ScoresForCompleteLevel;
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
            
            _progressData.CurrentLevel++;
            LevelChanged?.Invoke(_progressData.CurrentLevel);

            var playerStartPosition = _sceneData.LevelsData[_progressData.CurrentLevel - 1].PlayerStart;
            _playerInitializer.SetNewStartPosition(playerStartPosition);
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
            _progressData.CurrentLevel = 1;
            _scores = _progressData.ScoresOnStart;
            _scoresText.text = string.Concat(SCORES, _scores);

            _penaltyScores = _progressData.PenaltyScores;
            ScoresChanged?.Invoke(_scores);

            LoadLevel();
        }

        private void LoadLevel()
        { 
        
        }

        private void GameOver()
        {
            Debug.Log("GAME OVER!");
        }
    }
}
