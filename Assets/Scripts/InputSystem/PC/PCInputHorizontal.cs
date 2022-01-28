using System;
using UnityEngine;

namespace InputSystem
{
    public class PCInputHorizontal : IUserInputProxy
    {
        public event Action<float> OnAxisChange = delegate { };

        public void GetAxis()
        {
            OnAxisChange?.Invoke(Input.GetAxis(InputName.Horizontal));
        }
    }
}
