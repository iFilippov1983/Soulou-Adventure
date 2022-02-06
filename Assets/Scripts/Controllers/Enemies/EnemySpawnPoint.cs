using System;
using UnityEngine;

namespace Soulou
{
    [Serializable]
    public class EnemySpawnPoint
    {
        [SerializeField] private bool _loopSpawn;
        [SerializeField] private int _amount;
        [SerializeField] private float _pointXCoord;
        [SerializeField] private float _pointYCoord;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _minSpawnTime;
        [SerializeField] private float _maxSpawnTime;

        public EnemyType EnemyType => _enemyType;
        public Vector2 Position => new Vector2(_pointXCoord, _pointYCoord);
        public int Amount => _loopSpawn ? _amount : 1;
        public float MinSpawnTime => _loopSpawn ? _minSpawnTime : 1f;
        public float MaxSpawnTime => _loopSpawn ? _maxSpawnTime : 1f;
    }
}