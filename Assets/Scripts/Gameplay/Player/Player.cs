using UnityEngine;

namespace TandC.FlyBall
{
    public class Player
    {
        private GameObject _selfObject;
        private Rigidbody _rigidBody;
        private Collider _collider;
        private OnBehaviourHandler _onBehaviourHandler;

        private float _speed;

        private PlayerMovement _playerMovement;

        public Player(ILoadObjectsManager loadObjectsManager, Transform parent, float speed)
        {
            _selfObject = MonoBehaviour.Instantiate(loadObjectsManager.GetObjectByPath<GameObject>("Prefabs/Gameplay/Player"), parent);

            _rigidBody = _selfObject.GetComponent<Rigidbody>();
            _collider = _selfObject.GetComponent<Collider>();
            _onBehaviourHandler = _selfObject.GetComponent<OnBehaviourHandler>();

            _speed = speed;

            _playerMovement = new PlayerMovement(_rigidBody, _speed, _selfObject);
        }

        public void Update()
        {
            _playerMovement.Update();
        }

        public Vector3 GetPlayerPosition() => _selfObject.transform.position;
    }
}