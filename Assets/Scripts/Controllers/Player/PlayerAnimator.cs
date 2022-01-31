using static Soulou.SpriteAnimatorData;

namespace Soulou
{
    public class PlayerAnimator
    {
        private LevelObjectViewModel _playerModel;
        private SpriteAnimatorController _spriteAnimator;
        private SpriteAnimatorData _spriteAnimatorData;
        private float _currentAnimationSpeed;
        private bool _isLoooping;
        private bool _isNotPaused = true;

        public PlayerAnimator(LevelObjectViewModel playerModel)
        {
            _playerModel = playerModel;
            var playerData = _playerModel.objectData as PlayerData;
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
                _playerModel.spriteRenderer, 
                _playerModel.state, 
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
                if (ss.state.Equals(_playerModel.state))
                {
                    _currentAnimationSpeed = ss.animationSpeed;
                    _isLoooping = ss.isLooping;
                }
            }
        }
    }
}
