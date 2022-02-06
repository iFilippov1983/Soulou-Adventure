using InputSystem;

namespace Soulou
{
    public class GameInitializer
    {
        private PlayerInitializer _playerInitializer;
        private PlayerController _playerController;
        private GameProgressController _gameProgressController;
        private EnemiesController _enemiesController;

        public GameInitializer(ControllersDepo controllers, GameData gameData)
        {
            var inputInitializer = new InputInitializer();
            var inputController = new InputController(inputInitializer);
            _enemiesController = new EnemiesController(gameData);
            _playerInitializer = new PlayerInitializer(gameData);
            _gameProgressController = new GameProgressController(gameData, _playerInitializer);
            _playerController = new PlayerController(gameData, _playerInitializer.PlayerView, inputInitializer);
            

            controllers.Add(inputController);
            controllers.Add(_gameProgressController);
            controllers.Add(_playerController);
            controllers.Add(_enemiesController);
        }

        public void Configure()
        {
            _playerController.OnMove += _enemiesController.SetTargetTransform;
            _playerController.OnFinishEnter += _gameProgressController.LevelComplete;
            _playerController.OnPlayerDeath += _gameProgressController.LevelFailed;
            _playerController.OnPlayerDeath += _playerInitializer.RespawnPlayer;
        }

        public void Cleanup()
        {
            _playerController.OnMove += _enemiesController.SetTargetTransform;
            _playerController.OnFinishEnter -= _gameProgressController.LevelComplete;
            _playerController.OnPlayerDeath -= _gameProgressController.LevelFailed;
            _playerController.OnPlayerDeath -= _playerInitializer.RespawnPlayer;
        }
    }
}
