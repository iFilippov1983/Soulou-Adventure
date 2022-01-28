using static Soulou.SpriteAnimatorData;

namespace Soulou
{
    public class PlayerAnimator
    {
        private LevelObjectModel _playerModel;
        private SpriteAnimatorController _spriteAnimator;
        private SpriteAnimatorData _spriteAnimatorData;
        private float _currentAnimationSpeed;
        private bool _isLoooping;

        public PlayerAnimator(LevelObjectModel playerModel)
        {
            _playerModel = playerModel;
            var playerData = _playerModel.objectData as PlayerData;
            _spriteAnimatorData = playerData.SpriteAnimatorData;
            _spriteAnimator = new SpriteAnimatorController(_spriteAnimatorData);
        }

        public void Execute()
        {
            _spriteAnimator.Execute();
        }

        public void SetNewAnimation()
        {
            SetAnimationProperties();
            //_spriteAnimator.StopAnimation(_playerModel.spriteRenderer);
            _spriteAnimator.StartAnimation(_playerModel.spriteRenderer, _playerModel.state, _isLoooping, _currentAnimationSpeed);
        }

        private void SetAnimationProperties()
        {
            foreach (SpriteSequence ss in _spriteAnimatorData.Sequences)
            {
                if (ss.state == _playerModel.state)
                {
                    _currentAnimationSpeed = ss.animationSpeed;
                    _isLoooping = ss.isLooping;
                }
            }
        }
    }
}
