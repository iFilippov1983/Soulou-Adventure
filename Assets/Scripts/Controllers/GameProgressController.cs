using UnityEngine;
using UnityEngine.SceneManagement;

namespace Soulou
{
    public class GameProgressController : IInitialization
    {
        private GameProgressData _progressData;
        private PlayerInitializer _playerInitializer;
        private int _lives;
        private int _scores;
        private int _currentLevel;
        private int _scoresForCompleteLevel;

        public GameProgressController(GameData gameData, PlayerInitializer playerInitializer)
        {
            _progressData = gameData.GameProgressData;
            _playerInitializer = playerInitializer;
        }

        public void Initialize()
        {
            //NewGame();
        }

        public void LevelComplete()
        {
            _scores += _scoresForCompleteLevel;
            _currentLevel++;
            if (_currentLevel < SceneManager.sceneCountInBuildSettings)
            {
                LoadLevel(_currentLevel);
            }
            else
            {
                _currentLevel = 1;
                LoadLevel(_currentLevel);
            }
            
        }

        public void LevelFailed()
        {
            _lives--;

            Debug.Log(_lives);

            if (_lives <= 0)
            {
                NewGame();
            }
            else 
            {
                LoadLevel(_currentLevel);
            }
        }

        private void NewGame()
        {
            _lives = _progressData.Lives;
            _scores = _progressData.Scores;
            _currentLevel = _progressData.CurrentLevel;
            _scoresForCompleteLevel = _progressData.ScoresForCompleteLevel;

            LoadLevel(_currentLevel);
        }

        private void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
            _playerInitializer.InitPlayer();
        }
    }
}
