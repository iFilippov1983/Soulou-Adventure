using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(menuName = "GameData/SceneData", fileName = "SceneData")]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private string[] _enemyPrefabsPath;
        [SerializeField] private string[] _levelsDataPaths;
        
        private List<GameObject> _enemies = new List<GameObject>();
        private List<LevelData> _levelsData = new List<LevelData>();

        public List<GameObject> Enemies => LoadEnemies();
        public List<LevelData> LevelsData => GetLevelsData();

        private List<GameObject> LoadEnemies()
        {
            if (_enemies.Count == 0)
            {
                for (int index = 0; index < _enemyPrefabsPath.Length; index++)
                {
                    var path = string.Concat(PathString.PrefabsFolderPath, _enemyPrefabsPath[index]);
                    _enemies.Add(Resources.Load<GameObject>(path));
                }
            }
            return _enemies;
        }

        private List<LevelData> GetLevelsData()
        {
            if (_levelsData.Count == 0)
            {
                for (int index = 0; index < _levelsDataPaths.Length; index++)
                {
                    var path = string.Concat(PathString.LevelsDataFolderPath, _levelsDataPaths[index]);
                    _levelsData.Add(Resources.Load<LevelData>(path));
                }
                
            }
            return _levelsData;
        }


    }
}
