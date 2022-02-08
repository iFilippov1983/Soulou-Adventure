using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    public class BallShooter
    {
        private float _fireRate;
        private float _startSpeed;
        private List<BallHandler> _balls = new List<BallHandler>();
        private Transform _transform;

        private int _currentIndex;
        private float _timeToReload;

        public BallShooter(EnemyCannon enemyCannon, List<EnemyCannonBall> balls)
        {
            _fireRate = enemyCannon.FireRate;
            _transform = enemyCannon.Barrel;
            _startSpeed = balls[0].StartSpeed;
            foreach (var ballView in balls)
            {
                _balls.Add(new BallHandler(ballView));
            }
        }

        public void Execute()
        {
            if (_timeToReload > 0)
            {
                _timeToReload -= Time.deltaTime;
            }
            else
            {
                _timeToReload = _fireRate;
                _balls[_currentIndex].Throw(_transform, _transform.right * _startSpeed);
                _currentIndex++;
                if (_currentIndex >= _balls.Count) _currentIndex = 0;
            }
            _balls.ForEach(b => b.Execute());
        }
    }
}