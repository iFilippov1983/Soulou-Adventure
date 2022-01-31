using UnityEngine;

namespace Soulou
{
    /// <summary>
    /// Contains and manages all references to Data-type ScriptableObjects
    /// </summary>
    [CreateAssetMenu(menuName = "GameData/GameData", fileName = "GameData")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private string _playerDataPath;
        [SerializeField] private string _enemyDataPath;
        [SerializeField] private string _gameProgressDataPath;

        private PlayerData _playerData;
        private EnemyData _enemyData;
        private GameProgressData _gameProgressData;

        public PlayerData PlayerData
        {
            get 
            {
                if (_playerData == null) _playerData = LoadPath<PlayerData>
                        (string.Concat(PathString.GameDataFolderPath, _playerDataPath));
                return _playerData;
            }
        }

        public EnemyData EnemyData
        {
            get
            {
                if (_enemyData == null) _enemyData = LoadPath<EnemyData>
                         (string.Concat(PathString.GameDataFolderPath, _enemyDataPath));
                return _enemyData;
            }
        }

        public GameProgressData GameProgressData
        {
            get
            {
                if (_gameProgressData == null) _gameProgressData = LoadPath<GameProgressData>
                        (string.Concat(PathString.GameDataFolderPath, _gameProgressDataPath));
                return _gameProgressData;
            }
        }

        private T LoadPath<T>(string path) where T : Object =>
            Resources.Load<T>(System.IO.Path.ChangeExtension(path, null));
    }
}

