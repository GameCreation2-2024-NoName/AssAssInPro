// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_12_07
// -------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;

namespace Hmxs.Scripts
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerJoinProxy : MonoBehaviour
    {
        private PlayerInputManager _playerInputManager;

        private void Awake()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();
        }

        private void OnEnable()
        {
            _playerInputManager.onPlayerJoined += OnPlayerJoin;
            _playerInputManager.onPlayerLeft += OnPlayerLeft;
        }

        private void OnDisable()
        {
            _playerInputManager.onPlayerJoined -= OnPlayerJoin;
            _playerInputManager.onPlayerLeft -= OnPlayerLeft;
        }

        private void OnPlayerJoin(PlayerInput playerInput)
        {
            var handler = playerInput.GetComponent<InputHandler>();
            if (handler == null) return;

            playerInput.transform.SetParent(transform);

            if (playerInput.GetDevice<Mouse>() != null && playerInput.GetDevice<Keyboard>() != null)
                playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);

            if (handler is GamepadInputHandler gamepadInputHandler)
            {
                gamepadInputHandler.SwitchMap("Selection");
            }
        }

        private void OnPlayerLeft(PlayerInput playerInput)
        {
            var handler = playerInput.GetComponent<InputHandler>();
            if (handler == null) return;

            if (handler is GamepadInputHandler gamepadInputHandler)
            {
                gamepadInputHandler.SwitchMap("Gameplay");
            }
        }
    }
}