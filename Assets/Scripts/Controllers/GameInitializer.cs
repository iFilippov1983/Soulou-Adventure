﻿using InputSystem;

namespace Soulou
{
    public class GameInitializer
    {
        private PlayerController _playerController;
        private GameProgressController _gameProgressController;
        private EnemyRollingController _enemyRollingController;

        public GameInitializer(ControllersDepo controllers, GameData gameData)
        {
            var playerInitializer = new PlayerInitializer(gameData);
            var inputInitializer = new InputInitializer();
            var inputController = new InputController(inputInitializer);
            _gameProgressController = new GameProgressController(gameData, playerInitializer);
            _playerController = new PlayerController(gameData, playerInitializer.PlayerModel, inputInitializer);
            _enemyRollingController = new EnemyRollingController(gameData);

            controllers.Add(inputController);
            controllers.Add(_gameProgressController);
            controllers.Add(_playerController);
            controllers.Add(_enemyRollingController);
        }

        public void Configure()
        {
            //_playerController.OnFinishEnter += _gameProgressController.LevelComplete;
            //_playerController.OnPlayerDeath += _gameProgressController.LevelFailed;
            //_enemyRollingController.OnPlayerHit += _playerController.OnPlayerHit;

        }

        public void Cleanup()
        {
            //_playerController.OnFinishEnter -= _gameProgressController.LevelComplete;
            //_playerController.OnPlayerDeath -= _gameProgressController.LevelFailed;
            //_enemyRollingController.OnPlayerHit -= _playerController.OnPlayerHit;
        }
    }
}