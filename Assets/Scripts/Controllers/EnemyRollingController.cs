using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Soulou
{
    public class EnemyRollingController : IInitialization, IExecute, ICleanup
    {
        LevelObjectViewModel _playerModel;

        private GameObject _enemyPrefab;
        private float _minSpawnTime;
        private float _maxSpawnTime;
        private int _enemyAmount;
        private Vector3 _spawnPoint;

        private bool _canSpawn;
        private Coroutine _spawnTimer;
        private Stack<EnemyRolling> _enemyStack;

        public Action OnPlayerHit;

        public EnemyRollingController(GameData gameData)
        {
            var enemyData = gameData.EnemyData;
            _enemyPrefab = enemyData.EnemyPrefab;
            _minSpawnTime = enemyData.MinSpawnTime;
            _maxSpawnTime = enemyData.MaxSpawnTime;
            _enemyAmount = enemyData.Amount;
            _spawnPoint = enemyData.SpawnPoint;
            _enemyStack = new Stack<EnemyRolling>();
        }

        public void Initialize()
        {
            MakeEnemyStack();
            _canSpawn = true;
        }

        public void Execute(float deltaTime)
        {
            Spawn();
        }

        public void Cleanup()
        {
            foreach (EnemyRolling er in _enemyStack)
            {
                er.OnFinishTouched -= ReturnToStack;
                er.OnPlayerHit -= OnPlayerHitEvent;
            }
        }

        private void Spawn()
        {
            if (_enemyStack.Count != 0 & _canSpawn)
            {
                _canSpawn = false;
                var enemy = _enemyStack.Pop();
                enemy.gameObject.SetActive(true);
                enemy.transform.position = _spawnPoint;

                var spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
                _spawnTimer = CoroutinesController.StartRoutine(SpawnRateTimer(spawnTime));
            }
        }

        private void MakeEnemyStack()
        {
            for (int index = 0; index < _enemyAmount; index++)
            {
                var obj = Object.Instantiate(_enemyPrefab);
                obj.SetActive(false);
                var enemy = obj.GetComponent<EnemyRolling>();

                enemy.OnFinishTouched += ReturnToStack;
                enemy.OnPlayerHit += OnPlayerHitEvent;

                _enemyStack.Push(enemy);
            }
        }

        private void ReturnToStack(EnemyRolling enemy)
        {
            enemy.gameObject.SetActive(false);
            _enemyStack.Push(enemy);
        }

        private void OnPlayerHitEvent()
        {
            OnPlayerHit?.Invoke();
        }

        private IEnumerator SpawnRateTimer(float time)
        {
            yield return new WaitForSeconds(time);
            _canSpawn = true;
            CoroutinesController.StopRoutine(_spawnTimer);
        }
    }
}
