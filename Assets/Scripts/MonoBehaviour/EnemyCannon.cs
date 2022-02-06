using UnityEngine;

namespace Soulou
{
    public class EnemyCannon : LevelObjectView
    {
        [SerializeField] private float _shotDistance = 15f;
        [SerializeField] private float _minAimAngle = 0f;
        [SerializeField] private float _maxAimAngle = 45f;
        [SerializeField] private float _fireRate = 2.5f;
        [SerializeField] private int _ammoAmount = 10;

        public float ShotDistance => _shotDistance;
        public float MinAimAngle => _minAimAngle;
        public float MaxAimAngle => _maxAimAngle;
        public float FireRate => _fireRate;
        public int AmmoAmount => _ammoAmount;
        public Transform Barrel => transform.Find(LiteralString.CannonBarrel);
    }
}
