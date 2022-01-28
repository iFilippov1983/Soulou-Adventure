using System;
using UnityEngine;

namespace InputSystem
{
    class PCInputJump : IUserInputProxy
    {
        public event Action<float> OnAxisChange;

        public void GetAxis()
        {
            OnAxisChange?.Invoke(Input.GetAxis(InputName.Jump));
        }
    }
}
