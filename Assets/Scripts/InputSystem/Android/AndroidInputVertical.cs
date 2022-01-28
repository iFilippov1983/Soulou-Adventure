using System;
using JoystickTools;

namespace InputSystem
{
    public sealed class AndroidInputVertical: IUserInputProxy
    {
        private FloatingJoystick _joystick;
        public event Action<float> OnAxisChange;

        public AndroidInputVertical(FloatingJoystick joystick)
        {
            _joystick = joystick;
        }

        public void GetAxis()
        {
            OnAxisChange?.Invoke(_joystick.GetVertical());
        }
    }
}