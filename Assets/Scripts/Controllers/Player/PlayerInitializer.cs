using UnityEngine;

namespace Soulou
{
    public sealed class PlayerInitializer
    {
        private PlayerData _playerData;
        private GameObject _playerObject;
        private LevelObjectViewModel _playerModel;

        public LevelObjectViewModel PlayerModel => _playerModel;

        public PlayerInitializer(GameData gameData)
        {
            _playerData = gameData.PlayerData;
            _playerObject = Object.Instantiate(_playerData.PlayerPrefab, _playerData.StartPosition, Quaternion.identity);

            _playerModel = new LevelObjectViewModel();
            _playerModel.collider2D = _playerObject.GetComponent<Collider2D>();
            _playerModel.rigidbody2D = _playerObject.GetComponent<Rigidbody2D>();
            _playerModel.spriteRenderer = _playerObject.GetComponent<SpriteRenderer>();
            _playerModel.transform = _playerObject.transform;
            _playerModel.objectData = _playerData;
            _playerModel.state = SubjectState.Idle;
        }

        public void InitPlayer()
        {
            _playerObject = Object.Instantiate(_playerData.PlayerPrefab, _playerData.StartPosition, Quaternion.identity);

            _playerModel = new LevelObjectViewModel();
            _playerModel.collider2D = _playerObject.GetComponent<Collider2D>();
            _playerModel.rigidbody2D = _playerObject.GetComponent<Rigidbody2D>();
            _playerModel.spriteRenderer = _playerObject.GetComponent<SpriteRenderer>();
            _playerModel.transform = _playerObject.transform;
            _playerModel.objectData = _playerData;
            _playerModel.state = SubjectState.Idle;
        }
    }
}
