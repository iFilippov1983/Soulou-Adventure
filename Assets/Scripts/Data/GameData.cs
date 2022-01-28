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

        public PlayerData PlayerData
        {
            get 
            {
                if (_playerData == null) _playerData = LoadPath<PlayerData>
                        (string.Concat(Path.GameDataFolderPath, _playerDataPath));
                return _playerData;
            }
        }

        private T LoadPath<T>(string path) where T : Object =>
            Resources.Load<T>(System.IO.Path.ChangeExtension(path, null));
    }
}

