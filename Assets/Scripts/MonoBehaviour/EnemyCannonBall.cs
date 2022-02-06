using UnityEngine;

namespace Soulou
{
    [RequireComponent(typeof(TrailRenderer))]
    public class EnemyCannonBall : LevelObjectView 
    {
        public TrailRenderer TrailRenderer => GetComponent<TrailRenderer>();


    }
}
