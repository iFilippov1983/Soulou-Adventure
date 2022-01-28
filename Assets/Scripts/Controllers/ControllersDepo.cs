using System.Collections.Generic;

namespace Soulou
{
    public class ControllersDepo : IInitialization, IExecute, IFixedExecute, ILateExecute, ICleanup
    {
        private readonly List<IConfigure> _configureController;
        private readonly List<IInitialization> _initializationControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<IFixedExecute> _fixedExecuteControllers;
        private readonly List<ILateExecute> _lateExecuteControllers;
        private readonly List<ICleanup> _cleanupControllers;

        public ControllersDepo()
        {
            _configureController = new List<IConfigure>();
            _initializationControllers = new List<IInitialization>();
            _fixedExecuteControllers = new List<IFixedExecute>();
            _executeControllers = new List<IExecute>();
            _lateExecuteControllers = new List<ILateExecute>();
            _cleanupControllers = new List<ICleanup>();
        }

        public void Add(IController controller)
        {
            if (controller is IConfigure configController)
            {
                _configureController.Add(configController);
            }

            if (controller is IInitialization initController)
            {
                _initializationControllers.Add(initController);
            }

            if (controller is IFixedExecute fixedExecuteController)
            {
                _fixedExecuteControllers.Add(fixedExecuteController);
            }

            if (controller is IExecute executeController)
            {
                _executeControllers.Add(executeController);
            }

            if (controller is ILateExecute lateExecuteController)
            {
                _lateExecuteControllers.Add(lateExecuteController);
            }

            if (controller is ICleanup cleanupController)
            {
                _cleanupControllers.Add(cleanupController);
            }
        }

        public void Configure()
        {
            foreach (IConfigure controller in _configureController)
                controller.Configure();
        }

        public void Initialize()
        {
            foreach (IInitialization controller in _initializationControllers) 
                controller.Initialize();
        }

        public void FixedExecute()
        {
            foreach (IFixedExecute controller in _fixedExecuteControllers)
                controller.FixedExecute();
        }

        public void Execute(float deltaTime)
        {
            foreach (IExecute controller in _executeControllers) 
                controller.Execute(deltaTime);
        }

        public void LateExecute()
        {
            foreach (ILateExecute controller in _lateExecuteControllers) 
                controller.LateExecute();
        }

        public void Cleanup()
        {
            foreach (ICleanup controller in _cleanupControllers) 
                controller.Cleanup();
        }
    }
}


