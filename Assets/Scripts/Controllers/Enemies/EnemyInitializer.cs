using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    public class EnemyInitializer
    {
        private List<EnemyHandler> _eHandlers;
        private List<GameObject> _enemyPrefabs;
        private EnemySpawnPoint[] _enemySpawnPoints;
        public EnemyInitializer(List<GameObject> enemyPrefabs)
        {
            _enemyPrefabs = enemyPrefabs;
            _eHandlers = new List<EnemyHandler>();
        }

        public void SetSpawnPoints(EnemySpawnPoint[] spawnPoints)
        {
            _enemySpawnPoints = spawnPoints;
        }

        public List<EnemyHandler> GetHandlers()
        {
            CreateHandlers();
            return _eHandlers;
        }

        private void CreateHandlers()
        {
            foreach (EnemySpawnPoint point in _enemySpawnPoints)
            {
                switch (point.EnemyType)
                {
                    case EnemyType.EnemyRolling:
                        var prefab = GetEnemyPrefab(typeof(EnemyRolling));
                        var rollingH = new EnemyRollingHandler(point, prefab);
                        _eHandlers.Add(rollingH);
                        break;
                    case EnemyType.EnemyCannon:
                        prefab = GetEnemyPrefab(typeof(EnemyCannon));
                        var prefabBall = GetEnemyPrefab(typeof(EnemyCannonBall));
                        var cannonH = new EnemyCannonHandler(point, prefab, prefabBall);
                        _eHandlers.Add(cannonH);
                        break;
                    default:
                        break;
                }
            }
        }

        private GameObject GetEnemyPrefab<T>(T type)
        {
            return _enemyPrefabs.Find
                                (
                                obj => obj
                                .GetComponent<LevelObjectView>()
                                .GetType()
                                .Equals(type)//***
                                );
        }

        /* ***Equals:
        private GameObject GetEnemyCannonPrefab()
        {
            return _enemyPrefabs.Find
                            (
                            obj => obj
                            .GetComponent<LevelObjectView>()
                            .GetType()
                            .Equals(typeof(EnemyCannon))
                            );
        }

        private GameObject GetEnemyRollingPrefab()
        {
            return _enemyPrefabs.Find
                            (
                            obj => obj
                            .GetComponent<LevelObjectView>()
                            .GetType()
                            .Equals(typeof(EnemyRolling))
                            );
        }
        
        or

        Predicate<GameObject> predicate1 = (GameObject obj) => { return obj.GetComponent<LevelObjectView>().GetType().Equals(typeof(EnemyRolling)); };

        or

        Predicate<GameObject> predicate2 = new Predicate<GameObject>(PredicateFunc); 
        private static bool PredicateFunc(GameObject obj)
        {
            var component = obj.GetComponent<LevelObjectView>();
            var type = component.GetType();
            bool result = type.Equals(typeof (EnemyRolling));
            return result;
        }
        */
    }
}