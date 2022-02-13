using System;
using UnityEngine;

namespace Soulou
{
    public sealed class CameraFollowController : IFixedExecute
    {
        private const float _cameraMoveSpeed = 1f;
        private Camera _camera;
        private Vector3 _cameraFollowPosition;
        private Vector3 _cameraMoveDirection;
        private float _distance;

        private Func<Vector3> GetCameraFollowPositionFunc;

        public void Setup(Func<Vector3> CameraFollowPositionFunc, Camera camera)
        {
            GetCameraFollowPositionFunc = CameraFollowPositionFunc;
            _camera = camera;
        }

        public void SetCameraFollowPosition(Vector3 cameraFollowPosition)
        {
            SetFollowFunc(() => cameraFollowPosition);
        }

        public void SetFollowFunc(Func<Vector3> CameraFollowPositionFunc)
        {
            GetCameraFollowPositionFunc = CameraFollowPositionFunc;
        }

        public void FixedExecute()
        {
            HandleMovement(Time.fixedDeltaTime);
        }

        private void HandleMovement(float deltaTime)
        {
            _cameraFollowPosition = GetCameraFollowPositionFunc();
            _cameraFollowPosition.z = _camera.transform.position.z;

            _cameraMoveDirection = (_cameraFollowPosition - _camera.transform.position).normalized;
            _distance = Vector3.Distance(_cameraFollowPosition, _camera.transform.position);

            _camera.transform.position += _cameraMoveDirection * _distance * _cameraMoveSpeed * deltaTime;
        }
    }
}
