using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(menuName = "GameData/EnemyData", fileName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string _enemyPrefabPath;
        [SerializeField] private float _minSpawnTime;
        [SerializeField] private float _maxSpawnTime;
        [SerializeField] private int _amountOnLevel;
        [SerializeField] private Vector3 _spawnPoint;

        private GameObject _enemyPrefab;

        public float MinSpawnTime => _minSpawnTime;
        public float MaxSpawnTime => _maxSpawnTime;
        public int Amount => _amountOnLevel;
        public Vector3 SpawnPoint => _spawnPoint;

        public GameObject EnemyPrefab
        {
            get
            {
                if (_enemyPrefab == null) _enemyPrefab = Resources.Load<GameObject>
                        (string.Concat(PathString.PrefabsFolderPath, _enemyPrefabPath));
                return _enemyPrefab;
            }
        }
    }
}
