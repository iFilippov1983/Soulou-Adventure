using UnityEngine;

namespace Soulou
{
    public sealed class SceneInitializer
    {
        private ControllersDepo _controllers;
        private GameData _gameData;
        //private MenuManagementController _menuManagementController;
        //private OnButtonEnterProxyController _onButtonEnterProxy;
        //public UIComponentInitializer uiComponentInitializer;
        //public GameStateController gameStateController;
#if UNITY_ANDROID
        public AndroidPlayerUIController androidPlayerUIController; 
#endif

        public SceneInitializer(ControllersDepo controllers, GameData gameData)
        {
            _controllers = controllers;
            _gameData = gameData;

            //var uiInitialize = new UIInitializer(gameData);
            //uiComponentInitializer = new UIComponentInitializer(gameData, uiInitialize);
            //gameStateController = new GameStateController(uiInitialize, uiComponentInitializer);
            //_menuManagementController = new MenuManagementController(gameData, uiComponentInitializer, gameStateController);
            //_onButtonEnterProxy = new OnButtonEnterProxyController(uiComponentInitializer);
            
            //controllers.Add(uiComponentInitializer);
            //controllers.Add(gameStateController);
            //controllers.Add(_menuManagementController);
            //controllers.Add(_onButtonEnterProxy);
        }

        public void LateInit
            (
            Transform player, 
            PlayerController playerController 
            //SpaceObjectsController spaceObjectsController
            )
        {
            //var sceneController = new SceneController(_gameData, player);

            //_controllers.Add(sceneController);

            //_controllers.Add(new AudioController(_gameData, _menuManagementController, playerController.ShootingController, spaceObjectsController, _onButtonEnterProxy));


#if UNITY_ANDROID
            androidPlayerUIController =
                new AndroidPLayerUIController(_uiComponentInitializer, _gameStateController, _gameData);
#endif
#if UNITY_ANDROID
            controllers.Add(androidPlayerUIController);      
#endif
        }
    }
}
