using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(menuName = "GameData/LevelData", fileName = "Level_num_Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _LevelNumber;
        [SerializeField] private string _levelRefabPath;
        [SerializeField] private Vector2 _start;
        [SerializeField] private Vector2 _finish;
        [SerializeField] private Vector3 _cameraPosition;
        [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;

        private GameObject _levelPrefab;

        public int Number => _LevelNumber;
        public Vector2 Start => _start;
        public Vector2 Finsh => _finish;
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
