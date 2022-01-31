using UnityEngine;

namespace Soulou
{
    public class Main : MonoBehaviour
    {

        [SerializeField] private GameData _gameData;
        private ControllersDepo _controllers;
        private GameInitializer _gameInitializer;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _controllers = new ControllersDepo();
            _gameInitializer = new GameInitializer(_controllers, _gameData);
            _gameInitializer.Configure();
            _controllers.Configure();
        }

        private void Start()
        {
            _controllers.Initialize();
        }

        private void FixedUpdate()
        {
            _controllers.FixedExecute();
        }

        private void Update()
        {
            _controllers.Execute(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _controllers.LateExecute();
        }

        private void OnDestroy()
        {
            _gameInitializer.Cleanup();
            _controllers.Cleanup();
        }
    }
}
