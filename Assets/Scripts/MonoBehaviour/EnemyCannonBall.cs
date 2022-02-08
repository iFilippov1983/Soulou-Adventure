using UnityEngine;

namespace Soulou
{
    public class EnemyCannonBall : LevelObjectView 
    {
        [SerializeField] private float _startSpeed = 5;

        public float StartSpeed => _startSpeed;
    }
}
