// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_12_07
// -------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Hmxs.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class GamepadInputHandler : InputHandler
    {
        public override  bool Confirm => _confirmAction.triggered;
        public override Vector2 Select => _selectAction.ReadValue<Vector2>();
        public override bool Dash => _dashAction.WasReleasedThisFrame();
        public override bool Charge => _dashAction.IsPressed();
        public override Vector2 Direction => _directionAction.ReadValue<Vector2>();
        
        public override Device Device => Device.Gamepad;

        private PlayerInput _playerInput;
        public PlayerInput PlayerInput => _playerInput;
        public ReadOnlyArray<InputDevice> Devices => _playerInput.devices;
        public ReadOnlyArray<InputDevice> PairedDevices => _playerInput.user.pairedDevices;
        
        // public bool IsKeyboard => _playerInput.currentControlScheme == InputScheme.KeyboardMouse;
        // public bool IsGamepad => _playerInput.currentControlScheme == InputScheme.GamePad;
        public string ControlScheme => _playerInput.currentControlScheme;

        // Map: Selection
        private InputAction _selectAction;
        private InputAction _confirmAction;
        // Map: Gameplay
        private InputAction _dashAction;
        private InputAction _directionAction;

        public InputAction DashAction => _dashAction;
        public InputAction DirectionAction => _directionAction;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();

            _playerInput.onDeviceLost += _ => Destroy(gameObject);

            _selectAction = _playerInput.actions["Select"];
            _confirmAction = _playerInput.actions["Confirm"];
            _dashAction = _playerInput.actions["Dash"];
            _directionAction = _playerInput.actions["Direction"];
        }
        
        public override void SwitchMap(string map) =>
            _playerInput.SwitchCurrentActionMap(map);
    }
}