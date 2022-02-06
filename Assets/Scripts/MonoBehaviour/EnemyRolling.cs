using System;
using UnityEngine;

namespace Soulou
{
    public class EnemyRolling : LevelObjectView
    {
        [SerializeField] private float _speed = 1.2f;

        public float Speed => _speed;
        public Action<EnemyRolling, Collision2D> CollisionEnterEnemyEvent;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnterEnemyEvent?.Invoke(this, collision);
        }
    }
}
