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
        [SerializeField] private string _sceneDataPath;
        [SerializeField] private string _gameProgressDataPath;

        private PlayerData _playerData;
        private SceneData _sceneData;
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

        public SceneData SceneData
        {
            get
            {
                if (_sceneData == null) _sceneData = LoadPath<SceneData>
                         (string.Concat(PathString.GameDataFolderPath, _sceneDataPath));
                return _sceneData;
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

