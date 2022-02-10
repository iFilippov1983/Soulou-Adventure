using UnityEngine;

namespace Soulou
{
    public sealed class SceneController
    {
        private SceneData _sceneData;
        private LevelData _currenLevelData;
        
        public SceneController(SceneData sceneData)
        {
            _sceneData = sceneData;
            _currenLevelData = _sceneData.LevelsData[0];
        }

        public void InitialzeScene()
        {
            Camera.main.transform.position = _currenLevelData.CameraPosition;
        }

        public void ChangeLevel(int currentLevel)
        {
            Debug.Log(currentLevel);
            Debug.Log(_sceneData.LevelsData.Count);

            var levelData = _sceneData.LevelsData;
            if (currentLevel > levelData.Count) currentLevel = levelData.Count;

            _currenLevelData = levelData[currentLevel -1];
            InitialzeScene();
        }
    }
}
