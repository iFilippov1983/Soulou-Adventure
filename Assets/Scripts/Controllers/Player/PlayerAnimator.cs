using UnityEngine;
using static Soulou.SpriteAnimatorData;

namespace Soulou
{
    public class PlayerAnimator
    {
        private LevelObjectView _playerView;
        private SpriteAnimatorController _spriteAnimator;
        private SpriteAnimatorData _spriteAnimatorData;
        private float _currentAnimationSpeed;
        private bool _isLoooping;
        private bool _isNotPaused = true;

        public PlayerAnimator(LevelObjectView playerView)
        {
            _playerView = playerView;
            var playerData = _playerView.ObjectData as PlayerData;
            _spriteAnimatorData = playerData.SpriteAnimatorData;
            _spriteAnimator = new SpriteAnimatorController(_spriteAnimatorData);
        }

        public void Execute()
        {
            if(_isNotPaused) _spriteAnimator.Execute();
        }

        public void SetNewAnimation()
        {
            SetAnimationProperties();
            _spriteAnimator.StartAnimation
                (
                _playerView.SpriteRenderer, 
                _playerView.State, 
                _isLoooping, 
                _currentAnimationSpeed
                );
        }

        public void PauseAnimation()
        {
            _isNotPaused = false;
        }

        public void UnpauseAnimation()
        {
            _isNotPaused = true;
        }

        private void SetAnimationProperties()
        {
            foreach (SpriteSequence ss in _spriteAnimatorData.Sequences)
            {
                if (ss.state.Equals(_playerView.State))
                {
                    _currentAnimationSpeed = ss.animationSpeed;
                    _isLoooping = ss.isLooping;
                }
            }
        }
    }
}
