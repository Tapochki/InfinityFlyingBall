using UnityEngine;

namespace TandC.FlyBall
{
    public class PlayerController : IController
    {
        private ILoadObjectsManager _loadObjectsManager;
        private IGameplayManager _gameplayManager;

        private Player _player;

        private Camera _followCamera;

        private float _smoothnessCamera = 0.025f;
        private Vector3 _offset;


        public void Init()
        {
            _gameplayManager = GameClient.Get<IGameplayManager>();
            _loadObjectsManager = GameClient.Get<ILoadObjectsManager>();

            _gameplayManager.GameplayStartedEvent += GameplayStartedEventHandler;
        }

        public void Update()
        {
            if (_player != null)
            {
                _player.Update();
                FollowCamera();

            }
        }

        public void FixedUpdate()
        {
        }

        public void Dispose()
        {
        }

        public void ResetAll()
        {
        }

        private void FollowCamera()
        {
            Vector3 targetPosition = _player.GetPlayerPosition() + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(_followCamera.transform.position, targetPosition, _smoothnessCamera);
            _followCamera.transform.position = smoothedPosition;
        }

        private void CalculateCameraOffset()
        {
            _offset = _followCamera.transform.position - _player.GetPlayerPosition();
        }

        private void GameplayStartedEventHandler()
        {
            _player = new Player(_loadObjectsManager, _gameplayManager.GameplayObject.transform, 10);
            _followCamera = _gameplayManager.GameplayCamera;

            CalculateCameraOffset();
        }
    }
}