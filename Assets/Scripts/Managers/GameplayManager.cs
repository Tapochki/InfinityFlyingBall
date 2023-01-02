using System;
using System.Collections.Generic;
using TandC.FlyBall.Common;
using UnityEngine;

namespace TandC.FlyBall
{
    public class GameplayManager : IService, IGameplayManager
    {
        public event Action GameplayStartedEvent;

        public event Action ControllerInitEvent;

        public event Action GameplayEndedEvent;

        private List<IController> _controllers;

        private ILoadObjectsManager _loadObjectsManager;
        private ITimerManager _timerManager;
        private IDataManager _dataManager;

        public GameObject GameplayObject { get; private set; }

        public Camera GameplayCamera { get; private set; }

        public bool IsGameplayStarted { get; private set; }
        public bool IsGamePaused { get; private set; }

        private bool _isAfterPause;
        private float _afterPauseTimer;
        private const float _pauseTimeRecover = 0.5f;

        public void Dispose()
        {
            StopGameplay();

            if (_controllers != null)
            {
                foreach (var item in _controllers)
                    item.Dispose();
            }

            // _loadObjectsManager.BundlesDataLoadedEvent -= BundlesDataLoadedEventHandler;
            // _loadObjectsManager.BundlesDataLoadFailedEvent -= BundlesDataLoadFailedEventHandler;
        }

        private void OnDataManagerEndLoadCache() 
        {

        }

        public void Init()
        {
            _loadObjectsManager = GameClient.Get<ILoadObjectsManager>();
            _timerManager = GameClient.Get<ITimerManager>();
            _dataManager = GameClient.Get<IDataManager>();
            _dataManager.EndLoadCache += OnDataManagerEndLoadCache;
            _controllers = new List<IController>()
            {

            };

            foreach (var item in _controllers)
                item.Init();
        }

        public void Update()
        {
            if (!IsGamePaused)
            {
                if (_controllers != null)
                {
                    foreach (var item in _controllers)
                        item.Update();
                }
            }
            else 
            {
                if (_isAfterPause) 
                {
                    _afterPauseTimer -= Time.unscaledDeltaTime;
                    if (_afterPauseTimer <= 0)
                    {
                        PauseOff();
                    }
                }

            }
        }
        public void FixedUpdate()
        {
            if (!IsGamePaused)
            {
                if (_controllers != null)
                {
                    foreach (var item in _controllers)
                        item.FixedUpdate();
                }
            }
        }

        public T GetController<T>() where T : IController
        {
            foreach (var item in _controllers)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }

            throw new Exception("Controller " + typeof(T).ToString() + " have not implemented");
        }

        public void PauseGame(bool enablePause)
        {
            if (enablePause)
            {
                Time.timeScale = 0;
                IsGamePaused = true;
                _isAfterPause = false;
            }
            else
            {
                _afterPauseTimer = _pauseTimeRecover;
                _isAfterPause = true;
            }
        }

        public void PauseOff() 
        {
            _isAfterPause = false;
            Time.timeScale = 1;
            IsGamePaused = false;
        }

        public void StartGameplay()
        {
            if (IsGameplayStarted)
                return;
            GameplayObject = MonoBehaviour.Instantiate(_loadObjectsManager.GetObjectByPath<GameObject>("Prefabs/Gameplay/Gameplay"));
            GameplayCamera = GameplayObject.transform.Find("GameplayCamera").GetComponent<Camera>();
            MainApp.Instance.FixedUpdateEvent += FixedUpdate;
            IsGameplayStarted = true;
            _isAfterPause = false;
            ControllerInitEvent?.Invoke();
            GameplayStartedEvent?.Invoke();
        }

        public void StopGameplay()
        {
            if (!IsGameplayStarted)
                return;

            foreach (var item in _controllers)
                item.ResetAll();

            IsGameplayStarted = false;
            MainApp.Instance.FixedUpdateEvent -= FixedUpdate;
            MonoBehaviour.Destroy(GameplayObject);
        }

        public void RestartGameplay()
        {
            StopGameplay();
            StartGameplay();
        }
    }
}