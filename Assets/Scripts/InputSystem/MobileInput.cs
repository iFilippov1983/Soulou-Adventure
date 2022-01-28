using System;
using UnityEngine;

namespace InputSystem
{
    class MobileInput : IUserInputProxy
    {
        public event Action<float> OnAxisChange;

        public void GetAxis()
        {
            OnAxisChange.Invoke(Input.GetTouch(0).deltaPosition.sqrMagnitude);
        }
    }
}
