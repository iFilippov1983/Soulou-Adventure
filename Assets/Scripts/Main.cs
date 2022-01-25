using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorData _playerAnimatorData;
        [SerializeField] LevelObjectView _playerView;
        private float _animationSpeed;
    }
}
