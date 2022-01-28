using System;
using JoystickTools;

namespace InputSystem
{
    public sealed class AndroidFireInput: IUserInputProxy
    {
        private FloatingJoystick _joystick;
        public event Action<float> OnAxisChange;

        public AndroidFireInput(FloatingJoystick joystick)
        {
            _joystick = joystick;
        }
        
        public void GetAxis()
        {
            OnAxisChange?.Invoke(_joystick.ScreenTapped);
        }
    }
}