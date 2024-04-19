using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hmxs.Scripts
{
    public class Test : MonoBehaviour
    {
        public float speed = 3f;

        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody;

        private Vector2 _direction;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _direction = _playerInput.actions["Move"].ReadValue<Vector2>();
            _rigidbody.velocity = _direction * speed;
        }
    }
}