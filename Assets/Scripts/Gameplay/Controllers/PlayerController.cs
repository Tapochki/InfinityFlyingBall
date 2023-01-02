using UnityEngine;

namespace TandC.FlyBall
{
    public class PlayerController : IController
    {
        private ILoadObjectsManager _loadObjectsManager;

        private Player _player;

        private Camera _followCamera;

        private float _smoothnessCamera = 0.025f;
        private Vector3 _offset;


        public void Init()
        {
            _loadObjectsManager = GameClient.Get<ILoadObjectsManager>();

            _followCamera = GameObject.Find("[CORE]/Camera_Gameplay").GetComponent<Camera>();

            _player = new Player(_loadObjectsManager, GameObject.Find("[GAMEPLAY]").transform, 10);

            CalculateCameraOffset();
        }

        public void Update()
        {
            _player.Update();
            FollowCamera();
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
    }