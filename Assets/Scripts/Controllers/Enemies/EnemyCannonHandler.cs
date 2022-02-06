using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Soulou
{
    public class EnemyCannonHandler : EnemyHandler
    {
        private GameObject _cannonPrefab;
        private GameObject _ballPrefab;
        private GameObject _cannon;
        private Transform _barrel;
        private Transform _aimTransform;
        private float _shotDistance;
        private float _minAngle;
        private float _maxAngle;
        private float _fireRate;

        private bool _reloaded;
        private Coroutine _reloadTimer;

        public EnemyCannonHandler
            (
            EnemySpawnPoint spawnPoint, 
            GameObject prefabCannon, 
            GameObject prefabBall
            ) : base(spawnPoint.Position)
        {
            _cannonPrefab = prefabCannon;
            _ballPrefab = prefabBall;
        }

        public override void Initialize()
        {
            _cannon = Object.Instantiate(_cannonPrefab, _spawnPoint, Quaternion.identity);
            if (_spawnPoint.x > 0) _cannon.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            var cannonView = _cannon.GetComponent<EnemyCannon>();
            _barrel = cannonView.Barrel;
            _shotDistance = cannonView.ShotDistance;
            _minAngle = cannonView.MinAimAngle;
            _maxAngle = cannonView.MaxAimAngle;
            _fireRate = cannonView.FireRate;
        }

        public override void Execute()
        {
            Aim();
        }

        public override void Cleanup()
        {
            
        }

        public void SetAimTransform(Transform aimTransform)
        {
            _aimTransform = aimTransform;
        }

        public override T GetSelf<T>()
        {
            return this as T;
        }

        private void Aim()
        {
            if (_aimTransform)
            {
                var direction = _aimTransform.position - _barrel.position;
                var angle = Vector3.Angle(Vector3.right, direction);

                if (angle < _minAngle) angle = _minAngle;
                else if (angle > _maxAngle) angle = _maxAngle;

                var axis = Vector3.Cross(Vector3.right, direction);
                if (direction.magnitude < _shotDistance)
                {
                    _barrel.rotation = Quaternion.AngleAxis(angle, axis);
                }
            }
        }
    }
}