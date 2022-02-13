using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Soulou
{
    public abstract class EnemyHandler
    {
        protected Vector3 _spawnPoint;
        public EnemyHandler(Vector3 spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }
        public Action OnPlayerHit;
        public abstract void Initialize();
        public abstract void Execute();
        public abstract void Cleanup();
        public abstract T GetSelf<T>() where T : EnemyHandler;
    }
}