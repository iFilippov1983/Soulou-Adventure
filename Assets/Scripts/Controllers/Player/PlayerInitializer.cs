using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Soulou
{
    public sealed class PlayerInitializer : IDisposable
    {
        private PlayerData _playerData;
        private GameObject _playerObject;
        private LevelObjectView _playerView;
        private Coroutine _invinsibility;
        private Vector2 _playerFirstStartPosition;

        public LevelObjectView PlayerView => _playerView;

        public PlayerInitializer(GameData gameData)
        {
            _playerData = gameData.PlayerData;
            _playerFirstStartPosition = _playerData.StartPosition;
            _playerObject = Object.Instantiate(_playerData.PlayerPrefab, _playerFirstStartPosition, Quaternion.identity);

            _playerView = _playerObject.GetComponent<LevelObjectView>();
            _playerView.ObjectData = _playerData;
            _playerView.State = SubjectState.Idle;
        }

        public void RespawnPlayer()
        {
            _playerObject.transform.position = _playerData.StartPosition;
            _playerView.State = SubjectState.Idle;
            _invinsibility = CoroutinesController.StartRoutine(TemporaryInvincibility());
        }

        public void SetNewStartPosition(Vector3 start)
        {
            _playerData.StartPosition = start;
        }

        private IEnumerator TemporaryInvincibility()
        {
            _playerObject.layer = LayerMask.NameToLayer(LiteralString.Mask_IgnoreEnemies);
            yield return new WaitForSeconds(2f);
            _playerObject.layer = LayerMask.NameToLayer(LiteralString.Mask_Player);
            CoroutinesController.StopRoutine(_invinsibility);
        }

        public void Dispose()
        {
            _playerData.StartPosition = _playerFirstStartPosition;
        }
    }
}
