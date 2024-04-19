using UnityEngine;
using UnityEngine.InputSystem;

namespace Hmxs.Scripts.Selection
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        public bool Confirm => _confirmAction.triggered;
        public Vector2 Select => _selectAction.ReadValue<Vector2>();

        public bool Dash => _dashAction.triggered;
        public Vector2 Direction => _directionAction.ReadValue<Vector2>();

        private PlayerInput _playerInput;
        // Map: Selection
        private InputAction _selectAction;
        private InputAction _confirmAction;
        // Map: Gameplay
        private InputAction _dashAction;
        private InputAction _directionAction;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();

            _playerInput.onDeviceLost += _ => Destroy(gameObject);

            _selectAction = _playerInput.actions["Select"];
            _confirmAction = _playerInput.actions["Confirm"];
            _dashAction = _playerInput.actions["Dash"];
            _directionAction = _playerInput.actions["Direction"];
        }

        public void SwitchMap(string map) =>
            _playerInput.SwitchCurrentActionMap(map);
    }
}