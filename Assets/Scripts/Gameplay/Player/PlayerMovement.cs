using UnityEngine;

namespace TandC.FlyBall
{
    public class PlayerMovement
    {
        private Vector2 _direction;

        private Rigidbody _rigidbody;
        private GameObject _playerObject;
        private float _speed;

        public PlayerMovement(Rigidbody rigidbody, float speed, GameObject playerObject)
        {
            _rigidbody = rigidbody;
            _speed = speed;
            _playerObject = playerObject;
        }

        public void Update()
        {
            UpdateDirection();

            UpdateMovement();
        }

        private void UpdateDirection()
        {
            _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        private void UpdateMovement()
        {
            _playerObject.transform.Translate(Vector3.forward * _speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                _playerObject.transform.Translate(_direction * _speed * Time.deltaTime);
            }
        }
    }
}