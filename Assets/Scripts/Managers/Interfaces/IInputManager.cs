using System;
using UnityEngine;

namespace TandC.FlyBall
{
    public interface IInputManager
    {
        event Action<Vector2> OnLeftMouseButtonClickEvent;
        event Action<Vector2> OnLeftMouseButtonOnUIClickEvent;
        public Vector2 ReturnCameraMovementVelocity();
        public Vector2 MouseWorldPosition { get; }
        public Vector2 MouseUiPosition { get; }
        event Action<float, float> OnMoveEvent;
    }
}
