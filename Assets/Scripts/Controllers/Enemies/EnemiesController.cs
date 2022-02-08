using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    public class EnemiesController : IInitialization, IExecute, ICleanup
    {
        private SceneData _sceneData;
        private EnemyInitializer _initializer;
        private List<EnemyHandler> _eHandlers;
        private int _currentLevel = 1;
        private EnemySpawnPoint[] _enemySpawnPoints;

        public EnemiesController(GameData gameData)
        {
            _sceneData = gameData.SceneData;
            _initializer = new EnemyInitializer(_sceneData.Enemies);
        }
        public void Initialize()
        {
            _enemySpawnPoints = _sceneData.LevelsData[_currentLevel - 1].EnemySpawnPoints;
            _initializer.SetSpawnPoints(_enemySpawnPoints);
            _eHandlers = _initializer.GetHandlers();
            InitControllers();
        }

        public void Execute(float deltaTime)
        {
            ExecuteControllers();
        }

        public void Cleanup()
        {
            CleanupControllers();
        }

        public void SetCurrentLevel(int level)
        {
            _currentLevel = level;
            Initialize();
        }

        public void SetTargetTransform(Transform playerTransform)
        {
            foreach (EnemyHandler eh in _eHandlers)
            {
                if (eh is EnemyCannonHandler) eh.GetSelf<EnemyCannonHandler>().SetAimTransform(playerTransform);
            }
        }

        private void InitControllers()
        {
            foreach (EnemyHandler eh in _eHandlers)
            {
                eh.Initialize();
            }
        }

        private void ExecuteControllers()
        {
            foreach (EnemyHandler eh in _eHandlers)
            {
                eh.Execute();
            }
        }

        private void CleanupControllers()
        {
            foreach (EnemyHandler eh in _eHandlers)
            {
                eh.Cleanup();
            }
        }
    }
}
