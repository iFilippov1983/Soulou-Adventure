using System;
using UnityEngine;

namespace InputSystem
{
    class PCInputNumbers : IUserInputProxy
    {
        private float number;
        private readonly int[] Alpha = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };
        private KeyCode keyCode;

        public event Action<float> OnAxisChange = delegate { };

        public void GetAxis()
        {
            for (int keyCodeIndex = 0; keyCodeIndex < Alpha.Length; keyCodeIndex++)
            {
                keyCode = (KeyCode)Alpha[keyCodeIndex];

                if (Input.GetKeyDown(keyCode))
                {
                    number = keyCodeIndex;

                    OnAxisChange?.Invoke(number);
                }

                keyCode = KeyCode.None;
            }
        }
    }
}
