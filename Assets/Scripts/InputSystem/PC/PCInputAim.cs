using System;
using UnityEngine;

namespace InputSystem
{
    class PCInputAim : IUserInputProxy
    {
        private float _aimAngle;
        private Vector3 _mousePosition;
        private Vector3 _aimDirection;

        public event Action<float> OnAxisChange;

        public void GetAxis()
        {
            OnAxisChange?.Invoke(GetAimAngle());
        }

        private float GetAimAngle()
        {
            _mousePosition = GetMouseWorldPosition();

            _aimDirection = (_mousePosition - Camera.main.transform.position).normalized;
            _aimAngle = -Mathf.Atan2(_aimDirection.x, _aimDirection.y) * Mathf.Rad2Deg;

            return _aimAngle;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 vector = GetMoseWorldPositionV3(Input.mousePosition, Camera.main);
            vector.z = 0f;
            return vector;
        }

        private Vector3 GetMoseWorldPositionV3(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
    }
}
