using UnityEngine;

namespace Soulou
{
    public sealed class PlayerInitializer
    {
        private PlayerData _playerData;
        private GameObject _playerObject;
        private LevelObjectView _playerView;

        public LevelObjectView PlayerView => _playerView;

        public PlayerInitializer(GameData gameData)
        {
            _playerData = gameData.PlayerData;
            _playerObject = Object.Instantiate(_playerData.PlayerPrefab, _playerData.StartPosition, Quaternion.identity);

            _playerView = _playerObject.GetComponent<LevelObjectView>();
            _playerView.ObjectData = _playerData;
            _playerView.State = SubjectState.Idle;
        }

        public void RespawnPlayer()
        {
            _playerObject.transform.position = _playerData.StartPosition;
            _playerView.State = SubjectState.Idle;
        }
    }
}
