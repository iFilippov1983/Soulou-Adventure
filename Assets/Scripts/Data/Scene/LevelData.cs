using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(menuName = "GameData/LevelData", fileName = "Level_num_Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _LevelNumber;
        [SerializeField] private string _levelRefabPath;
        [SerializeField] private Vector3 _playerStart;
        [SerializeField] private Vector3 _playerFinish;
        [SerializeField] private Vector3 _cameraPosition;
        [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;

        private GameObject _levelPrefab;

        public int Number => _LevelNumber;
        public Vector2 PlayerStart =>
            _levelPrefab.gameObject.transform.position + _playerStart;
        public Vector2 PlayerFinsh => 
            _levelPrefab.gameObject.transform.position + _playerFinish;
        public Vector3 CameraPosition => _cameraPosition;
        public EnemySpawnPoint[] EnemySpawnPoints => _enemySpawnPoints;
        public GameObject LevelPrefab
        {
            get
            {
                if (_levelPrefab == null) _levelPrefab = Resources.Load<GameObject>
                        (string.Concat(PathString.PrefabsFolderPath, _levelRefabPath));
                return _levelPrefab;
            }
        }
    }
}
