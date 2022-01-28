using System;
using UnityEngine;

namespace InputSystem
{
    class PCInputStrafe : IUserInputProxy
    {
        private float normal;
        
        public event Action<float> OnAxisChange = delegate { };

        public void GetAxis()
        {
            if (Input.GetKeyDown(KeyCode.E)) normal = 1f;
            if (Input.GetKeyUp(KeyCode.E)) normal = 0f;
            if (Input.GetKeyDown(KeyCode.Q)) normal = -1f;
            if (Input.GetKeyUp(KeyCode.Q)) normal = 0f;
             
            OnAxisChange?.Invoke(normal);
        }
    }
}
