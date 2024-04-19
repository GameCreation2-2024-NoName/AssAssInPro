using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hmxs.Scripts.Selection
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerManager : MonoBehaviour
    {
        public InputHandler Handler1 => handler1;
        public InputHandler Handler2 => handler2;

        [SerializeField] [ReadOnly] private InputHandler handler1;
        [SerializeField] [ReadOnly] private InputHandler handler2;

        private PlayerInputManager _playerInputManager;

        private static PlayerManager _instance;
        public static PlayerManager Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();

            _playerInputManager.onPlayerJoined += OnPlayerJoined;
            _playerInputManager.onPlayerLeft += OnPlayerLeft;
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            var handler = playerInput.GetComponent<InputHandler>();
            if (handler == null) return;

            playerInput.transform.SetParent(transform);

            if (playerInput.GetDevice<Mouse>() != null && playerInput.GetDevice<Keyboard>() != null)
                playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);

            if (handler1 == null)
            {
                handler1 = handler;
                Debug.Log("Player1 - Joined");
            }
            else if (handler2 == null)
            {
                handler2 = handler;
                Debug.Log("Player2 - Joined");
            }
            else
            {
                playerInput.DeactivateInput();
                Debug.Log("Player Connection Full - Deactivated Input");
            }
        }

        private void OnPlayerLeft(PlayerInput playerInput)
        {
            var handler = playerInput.GetComponent<InputHandler>();
            if (handler == null) return;

            if (handler1 == handler)
            {
                handler1 = null;
                Debug.Log("Player1 - Left");
            }
            else if (handler2 == handler)
            {
                handler2 = null;
                Debug.Log("Player2 - Left");
            }
        }

        public void SwitchMap(string map)
        {
            if (handler1) handler1.SwitchMap(map);
            if (handler2) handler2.SwitchMap(map);
        }
    }
}