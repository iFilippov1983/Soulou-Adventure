using System;
using UnityEngine;
using JoystickTools;

namespace InputSystem
{
    public sealed class AndroidInputAim: IUserInputProxy
    {
        private FloatingJoystick _joystick;
        public event Action<float> OnAxisChange;

        public AndroidInputAim(FloatingJoystick joystick)
        {
            _joystick = joystick;
        }
        public void GetAxis()
        {
            OnAxisChange?.Invoke(GetAimAngle());
        }

        private float GetAimAngle()
        {
            var aimAngle = -Mathf.Atan2(_joystick.Horizontal, _joystick.Vertical)*Mathf.Rad2Deg;
            return aimAngle;
        }
    }
}