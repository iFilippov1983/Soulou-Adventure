using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(menuName = "GameData/EnemiesData", fileName = "EnemiesData")]
    public class EnemiesData : ScriptableObject
    {
        [SerializeField] private string[] _enemyPrefabsPath;

        private List<LevelObjectView> _enemies;
        public List<LevelObjectView> Enemies => LoadEnemies();
        private List<LevelObjectView> LoadEnemies()
        {
            if (_enemies == null)
            {
                for (int index = 0; index < _enemyPrefabsPath.Length; index++)
                {
                    var path = string.Concat(PathString.PrefabsFolderPath, _enemyPrefabsPath[index]);
                    var obj = Resources.Load<GameObject>(path).GetComponent<LevelObjectView>();
                    _enemies.Add(obj);
                }
            }
            return _enemies;
        }
        
    }
}
