﻿namespace TandC.FlyBall
{
    public interface IController
    {
        void Init();

        void Update();

        void FixedUpdate();

        void Dispose();

        void ResetAll();
    }
}
