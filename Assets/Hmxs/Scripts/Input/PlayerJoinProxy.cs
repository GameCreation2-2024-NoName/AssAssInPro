// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_12_07
// -------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hmxs.Scripts
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerJoinProxy : MonoBehaviour
    {
        [SerializeField]private PlayerInputManager playerInputManager;
        public bool canJoin;

        private void Awake()
        {
            playerInputManager = GetComponent<PlayerInputManager>();
        }

        private void OnEnable()
        {
            playerInputManager.onPlayerJoined += OnGamePadPlayerJoin;
            playerInputManager.onPlayerLeft += OnGamePadPlayerLeft;
        }

        private void OnDisable()
        {
            playerInputManager.onPlayerJoined -= OnGamePadPlayerJoin;
            playerInputManager.onPlayerLeft -= OnGamePadPlayerLeft;
        }

        private void Update()
        {
            CheckPlayerJoin();
        }

        private void CheckPlayerJoin()
        {
            // 对于不同设备玩家加入的查询逻辑，先写在业务侧
            if (!canJoin) return;
            InputHandler inputHandler = null;
            if (Input.GetKeyDown(KeyCode.F))
            {
                inputHandler = CreatePlayerInput(Device.LeftKeyboard);
            }

            if (Input.GetKeyDown(KeyCode.Slash))
            {
                inputHandler = CreatePlayerInput(Device.RightKeyboard);
            }

            if (Input.GetMouseButtonDown(0))
            {
                inputHandler = CreatePlayerInput(Device.Mouse);
            }
            
            if (Input.touchCount > 0)
            {
                inputHandler = CreatePlayerInput(Device.TouchScreen);
            }

            if (inputHandler)
            { 
                var ok = PlayerManager.Instance.RegisterPlayer(inputHandler);
                if(!ok)
                {
                    Destroy(inputHandler.gameObject);
                }
            }
        }
        
        private InputHandler CreatePlayerInput(Device device)
        {
            var obj = new GameObject();
            InputHandler inputHandler = null;
            switch (device)
            {
                case Device.LeftKeyboard:
                    obj.name = "LeftKeyBoardInputHandler";
                    inputHandler = obj.AddComponent<LeftKeyboardInputHandler>();
                    break;
                case Device.RightKeyboard:
                    obj.name = "RightKeyBoardInputHandler";
                    inputHandler = obj.AddComponent<RightKeyboardInputHandler>();
                    break;
                case Device.Mouse:
                    obj.name = "MouseInputHandler";
                    inputHandler = obj.AddComponent<MouseInputHandler>();
                    break;
                case Device.TouchScreen:
                    obj.name = "TouchScreenInputHandler";
                    inputHandler = obj.AddComponent<TouchScreenInputHandler>();
                    break;
                case Device.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(device), device, null);
            }

            return inputHandler;
        }

        // 手柄玩家的加入走不同的逻辑
        private void OnGamePadPlayerJoin(PlayerInput playerInput)
        {
            if (!canJoin) return;
            var handler = playerInput.GetComponent<InputHandler>();
            if (handler == null)
            {
                Debug.LogError("PlayerInput没有InputHandler组件");
                return;
            }

            // if (playerInput.GetDevice<Mouse>() != null && playerInput.GetDevice<Keyboard>() != null)
            //     playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);

            var ok = PlayerManager.Instance.RegisterPlayer(handler);
            if(!ok)
            {
                //playerInput.DeactivateInput();
                Destroy(playerInput.gameObject);
            }
        }

        private void OnGamePadPlayerLeft(PlayerInput playerInput)
        {
            var handler = playerInput.GetComponent<InputHandler>();
            
            if (handler == null) return;
            
            PlayerManager.Instance.UnRegisterPlayer(handler);
        }
    }
}