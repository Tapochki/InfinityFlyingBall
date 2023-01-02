using System;
using TandC.FlyBall.Common;
using UnityEngine;

namespace TandC.FlyBall
{
    public interface IGameplayManager
    {
        event Action GameplayStartedEvent;
        event Action GameplayEndedEvent;
        event Action ControllerInitEvent;
        bool IsGameplayStarted { get; }

        GameObject GameplayObject { get; }

        bool IsGamePaused { get; }

        Camera GameplayCamera { get; }

        T GetController<T>() where T : IController;
        void StartGameplay();
        void StopGameplay();
        void RestartGameplay();
        void PauseGame(bool enablePause);
        void PauseOff();
    }
}