using UnityEngine;
using Pathfinding;
using System.Collections;
using UnityEngine.Assertions;

namespace Soulou
{
    public sealed class SceneController : IFixedExecute
    {
        private SceneData _sceneData;
        private LevelData _currenLevelData;
        private GameObject _level;
        private CameraFollowController _cameraFollowController;
        private Coroutine _cameraMove;
        public SceneController(SceneData sceneData)
        {
            _sceneData = sceneData;
            _cameraFollowController = new CameraFollowController();
            InitLevel(1);
        }

        public void FixedExecute()
        {
            _cameraFollowController.FixedExecute();
        }

        public void InitialzeScene()
        {
            _level = Object.Instantiate(_currenLevelData.LevelPrefab);
        }

        public async void InitLevel(int currentLevel)
        {
            var levelData = _sceneData.LevelsData;
            if (currentLevel > levelData.Count) currentLevel = levelData.Count;

            _currenLevelData = levelData[currentLevel - 1];
            _cameraFollowController.Setup(() => _currenLevelData.CameraPosition, Camera.main);
            Object.Destroy(_level);
            InitialzeScene();
        }

        //private IEnumerator MoveCamera()
        //{
        //    yield return new WaitWhile(CameraMoving);
        //    CoroutinesController.StopRoutine(_cameraMove);
        //}

        //private bool CameraMoving()
        //{
        //    var mainCameraPos = Camera.main.transform.position;
        //    var neededPos = _currenLevelData.CameraPosition;
        //    var tolerance = 0.1f;
        //    var gotPosition = Mathf.Abs(mainCameraPos.y - neededPos.y) > tolerance;
        //    return gotPosition;
        //}
    }
}
