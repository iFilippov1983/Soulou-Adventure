namespace InputSystem
{
#if UNITY_ANDROID
    public sealed class AndroidInputProxy
    {
        private FloatingJoystick _movementJoystick;
        private FloatingJoystick _targetingJoystick;

        private AndroidInputHorizontal _androidMovementInputHorizontal;
        private AndroidInputVertical _androidMovementInputVertical;
        private AndroidInputAim _androidInputAim;
        private AndroidFireInput _androidFireInput;

        public AndroidInputHorizontal AndroidMovementInputHorizontal => _androidMovementInputHorizontal;
        public AndroidInputVertical AndroidMovementInputVertical => _androidMovementInputVertical;
        public AndroidInputAim AndroidInputAim => _androidInputAim;
        public AndroidFireInput AndroidFireInput => _androidFireInput;
        
        public AndroidInputProxy(UIComponentInitializer uiComponentInitializer)
        {
            _movementJoystick = uiComponentInitializer.PlayerUIView.MovementJoystick;
            _targetingJoystick = uiComponentInitializer.PlayerUIView.TargetingJoystic;
            _androidMovementInputHorizontal = new AndroidInputHorizontal(_movementJoystick);
            _androidMovementInputVertical = new AndroidInputVertical(_movementJoystick);
            _androidInputAim = new AndroidInputAim(_targetingJoystick);
            _androidFireInput = new AndroidFireInput(_targetingJoystick);
        }
    }
#endif
}