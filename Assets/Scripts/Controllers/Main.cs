using UnityEngine;

namespace Soulou
{
    public class Main : MonoBehaviour
    {

        [SerializeField] private GameData _gameData;
        private ControllersDepo _controllers;

        private void Awake()
        {
            _controllers = new ControllersDepo();
            new GameInitializer(_controllers, _gameData);

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
            _controllers.Cleanup();
        }
    }
}
