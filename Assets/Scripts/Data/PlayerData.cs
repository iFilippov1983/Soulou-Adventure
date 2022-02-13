using System;
using UnityEngine;

namespace Soulou
{
    /// <summary>
    /// Used to contain all player data references
    /// </summary>
    [CreateAssetMenu(menuName = "GameData/PlayerData", fileName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private string _playerPrefabPath;
        [SerializeField] private string _playerAnimatorDataPath;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _jumpStrength = 1f;
        [Header("Equals to MainObject position")]
        [SerializeField] private Vector2 _playerStartPosition = new Vector2(12, -7);
        [SerializeField] private Vector2 _finishPosition;

        private GameObject _playerPrefab;
        private SpriteAnimatorData _playeAnimatorData;

        public GameObject PlayerPrefab
        {
            get
            {
                if (_playerPrefab == null) _playerPrefab = 
                        Resources.Load<GameObject>(string.Concat(PathString.PrefabsFolderPath, _playerPrefabPath));
                return _playerPrefab;
            }
        }

        public SpriteAnimatorData SpriteAnimatorData
        {
            get
            {
                if (_playeAnimatorData == null) _playeAnimatorData =
                             Resources.Load<SpriteAnimatorData>(string.Concat(PathString.GameDataFolderPath, _playerAnimatorDataPath));
                return _playeAnimatorData;
            }
        }

        public float MovementSpeed => _moveSpeed;
        public float JumpStrength => _jumpStrength;
        public Vector2 FinishPosition => _finishPosition;
        public Vector3 StartPosition 
        { 
            get { return _playerStartPosition; } 
            set { _playerStartPosition = value; } 
        } 
    }
}

