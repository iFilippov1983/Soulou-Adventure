using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    public class Main : MonoBehaviour
    {
        private const string AnimationDataFolderPath = "GameData/";
        private const string PrefabsFolderPath = "Prefabs/";

        [SerializeField] private string _playerPrefabFileName;
        [SerializeField] private string _playerAnimatorDataFileName;
        [SerializeField]private LevelObjectView _playerView;

        private SpriteAnimatorData _playerAnimatorData;
        private SpriteAnimatorController _playerAnimator;
        private float _animationSpeed;
        //private Vector3 _playerStartPosotion;

        private void Awake()
        {
            _playerAnimatorData = Resources.Load<SpriteAnimatorData>
                (string.Concat(AnimationDataFolderPath, _playerAnimatorDataFileName));
            //_playerView = Resources.Load<LevelObjectView>(string.Concat(PrefabsFolderPath, _playerPrefabFileName));

            if (_playerAnimatorData) _playerAnimator = new SpriteAnimatorController(_playerAnimatorData);

            //_playerStartPosotion = gameObject.transform.position;
            
            _animationSpeed = _playerAnimatorData.Sequences[0].animationSpeed;
        }

        private void Start()
        {
            //Instantiate(_playerView, _playerStartPosotion, Quaternion.identity);

            _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimationStatePlayer.Run, true, _animationSpeed);
        }

        private void Update()
        {
            _playerAnimator.Execute();
        }
    }
}
