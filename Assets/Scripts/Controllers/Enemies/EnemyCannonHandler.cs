﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Soulou
{
    public class EnemyCannonHandler : EnemyHandler
    {
        private EnemyCannon _cannonView;
        private BallShooter _shooter;
        private GameObject _cannonPrefab;
        private GameObject _ballPrefab;
        private GameObject _cannon;
        private Transform _barrel;
        private Transform _aimTransform;
        private float _shotDistance;
        private float _minAngle;
        private float _maxAngle;
        private bool _playerInFOV;

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

            _cannonView = _cannon.GetComponent<EnemyCannon>();
            _barrel = _cannonView.Barrel;
            _shotDistance = _cannonView.ShotDistance;
            _minAngle = _cannonView.MinAimAngle;
            _maxAngle = _cannonView.MaxAimAngle;

            _shooter = new BallShooter(_cannonView, MakeBallList());
        }

        public override void Execute()
        {
            Aim();
            if (_playerInFOV) _shooter.Execute();
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
                    _playerInFOV = true;
                    _barrel.rotation = Quaternion.AngleAxis(angle, axis);
                }
                else _playerInFOV = false;
            }
        }

        private List<EnemyCannonBall> MakeBallList()
        {
            var list = new List<EnemyCannonBall>();
            for (int index = 0; index < _cannonView.AmmoAmount; index++)
            {
                var ballView = Object.Instantiate(_ballPrefab);
                var view = ballView.GetComponent<EnemyCannonBall>();
                list.Add(view);
            }
            return list;
        }
    }
}