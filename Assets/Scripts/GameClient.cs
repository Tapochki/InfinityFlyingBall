using System;
using System.Collections.Generic;
using UnityEngine;

namespace TandC.FlyBall
{
    public class GameClient
    {
        private static object _sync = new object();

        private static GameClient _Instance;
        public static GameClient Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_sync)
                    {
                        _Instance = new GameClient();
                    }
                }
                return _Instance;
            }
        }

        public static bool IsDebugMode = false;

        private IDictionary<Type, IService> _services;

        public GameClient()
        {
            _services = new Dictionary<Type, IService>();

#if UNITY_EDITOR
            IsDebugMode = true;
#endif
            AddService<ITimerManager>(new TimerManager());
            AddService<IAdvarismetnManager>(new AdvarismetnManager());
            AddService<ILoadObjectsManager>(new LoadObjectsManager());
            AddService<IAppStateManager>(new AppStateManager());
            AddService<ISoundManager>(new SoundManager());
            AddService<IUIManager>(new UIManager());
            AddService<IScenesManager>(new ScenesManager());
            AddService<IDataManager>(new DataManager());
            AddService<IGameplayManager>(new GameplayManager());
            AddService<IInputManager>(new InputManager());
        }

        public T GetService<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Service " + typeof(T) + " is not registered!");
            }
        }

        protected void AddService<T>(IService service)
        {
            if (service is T)
            {
                _services.Add(typeof(T), service);

            }
            else
            {
                throw new Exception("Service " + service.ToString() + " have not implemented interface: " + typeof(T));
            }
        }

        public void InitServices()
        {
            foreach (IService service in _services.Values)
                service.Init();
        }

        public void Update()
        {
            foreach (IService service in _services.Values)
                service.Update();
        }

        public void Dispose()
        {
            foreach (IService service in _services.Values)
                service.Dispose();
        }

        public static T Get<T>()
        {
            return Instance.GetService<T>();
        }
    }
}