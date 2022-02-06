using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Soulou
{
    public class EnemyRollingHandler : EnemyHandler
    {
        private GameObject _enemyPrefab;
        private float _minSpawnTime;
        private float _maxSpawnTime;
        private int _enemyAmount;

        private bool _canSpawn;
        private Coroutine _spawnTimer;
        private Stack<EnemyRolling> _enemyStackPassive;
        private List<EnemyRolling> _enemyListActive;

        public EnemyRollingHandler(EnemySpawnPoint spawnPoint, GameObject prefab) : base (spawnPoint.Position)
        {
            _enemyPrefab = prefab;
            _minSpawnTime = spawnPoint.MinSpawnTime;
            _maxSpawnTime = spawnPoint.MaxSpawnTime;
            _enemyAmount = spawnPoint.Amount;
            _enemyStackPassive = new Stack<EnemyRolling>();
            _enemyListActive = new List<EnemyRolling>();
        }

        public override void Initialize()
        {
            MakeEnemyStackPassive();
            _canSpawn = true;
        }

        public override void Execute()
        {
            Spawn();
        }

        public override void Cleanup()
        {
            foreach (EnemyRolling er in _enemyStackPassive)
            {
                er.CollisionEnterEnemyEvent -= CollisionEnter;
                if(er) Object.Destroy(er.gameObject);
            }
            foreach (EnemyRolling er in _enemyListActive)
            {
                er.CollisionEnterEnemyEvent -= CollisionEnter;
                if(er) Object.Destroy(er.gameObject);
            }

            _enemyListActive.Clear();
            _enemyStackPassive.Clear();
        }

        public override T GetSelf<T>()
        {
            return this as T;
        }

        private void Spawn()
        {
            if (_enemyStackPassive.Count != 0 & _canSpawn)
            {
                _canSpawn = false;

                var enemy = _enemyStackPassive.Pop();
                enemy.gameObject.SetActive(true);
                _enemyListActive.Add(enemy);
                enemy.transform.position = _spawnPoint;

                var spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
                _spawnTimer = CoroutinesController.StartRoutine(SpawnRateTimer(spawnTime));
            }
        }

        private void MakeEnemyStackPassive()
        {
            for (int index = 0; index < _enemyAmount; index++)
            {
                var obj = Object.Instantiate(_enemyPrefab);
                obj.SetActive(false);
                var enemy = obj.GetComponent<EnemyRolling>();

                enemy.CollisionEnterEnemyEvent += CollisionEnter;

                _enemyStackPassive.Push(enemy);
            }
        }

        private void CollisionEnter(EnemyRolling enemy, Collision2D collision)
        {

            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(LiteralString.Mask_Ground)))
            {
                enemy.Rigidbody2D.AddForce(collision.transform.right * enemy.Speed, ForceMode2D.Impulse);
            }

            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(LiteralString.Mask_Finish)))
            {
                _enemyListActive.Remove(enemy);
                enemy.gameObject.SetActive(false);
                _enemyStackPassive.Push(enemy);
            }
        }

        private IEnumerator SpawnRateTimer(float time)
        {
            yield return new WaitForSeconds(time);
            _canSpawn = true;
            CoroutinesController.StopRoutine(_spawnTimer);
        }
    }
}
