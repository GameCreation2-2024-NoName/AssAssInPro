using System;
using Pditine.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Hmxs.Scripts
{
    public enum Device
    {
        LeftKeyboard,
        RightKeyboard,
        Mouse,
        Gamepad,
        TouchScreen,
        Null
    }
    
    public abstract class InputHandler : MonoBehaviour
    {
        // 游戏场景外,确认
        public abstract bool Confirm { get; }
        // 游戏场景外,选择
        public abstract Vector2 Select { get; }
        // 玩家冲刺
        public abstract bool Dash { get; }
        // 玩家蓄力
        public abstract bool Charge { get; }
        // 玩家选择方向
        public abstract Vector2 Direction { get; }
        // 玩家设备
        public abstract Device Device { get; }

        protected PlayerController player;
        public PlayerController Player => player;
        
        public abstract void SwitchMap(string map);
        
        // private PlayerInput _playerInput;
        // public PlayerInput PlayerInput => _playerInput;
        
        // public ReadOnlyArray<InputDevice> Devices => _playerInput.devices;
        // public ReadOnlyArray<InputDevice> PairedDevices => _playerInput.user.pairedDevices;
        // public bool IsKeyboard => _playerInput.currentControlScheme == InputScheme.KeyboardMouse;
        // public bool IsGamepad => _playerInput.currentControlScheme == InputScheme.GamePad;

        // // Map: Selection
        // private InputAction _selectAction;
        // private InputAction _confirmAction;
        // // Map: Gameplay
        // private InputAction _dashAction;
        // private InputAction _directionAction;
        //
        // public InputAction DashAction => _dashAction;
        // public InputAction DirectionAction => _directionAction;
        //
        // private void Awake()
        // {
        //     _playerInput = GetComponent<PlayerInput>();
        //
        //     _playerInput.onDeviceLost += _ => Destroy(gameObject);
        //
        //     _selectAction = _playerInput.actions["Select"];
        //     _confirmAction = _playerInput.actions["Confirm"];
        //     _dashAction = _playerInput.actions["Dash"];
        //     _directionAction = _playerInput.actions["Direction"];
        // }
        
        // public void SwitchMap(string map) =>
        //     _playerInput.SwitchCurrentActionMap(map);
    }
}