using System;
using PurpleFlowerCore;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        // todo:支持动态数量玩家
        // List<InputHandler>
        public InputHandler Handler1 => handler1;
        public InputHandler Handler2 => handler2;
        
        [SerializeField] [ReadOnly] private InputHandler handler1;
        [SerializeField] [ReadOnly] private InputHandler handler2;
        
        public event Action<InputHandler> OnPlayerRegister;
        public event Action<InputHandler> OnPlayerUnRegister;

        // 当未来出现走不同逻辑玩家加入的情况时,考虑放弃持有引用,改为事件通知
        [SerializeField] private PlayerJoinProxy joinProxy;
        
        public static PlayerManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        public bool RegisterPlayer(InputHandler inputHandler)
        {
            if(!inputHandler)return false;
            
            if(inputHandler.Device != Device.Gamepad &&
               (inputHandler.Device ==  handler1?.Device || inputHandler.Device == handler2?.Device))
                return false;

            inputHandler.transform.SetParent(transform);
            
            if (handler1 == null)
            {
                handler1 = inputHandler;
                OnPlayerRegister?.Invoke(inputHandler);
                Debug.Log("Player1 - Joined");
                return true;
            }

            if (handler2 == null)
            {
                handler2 = inputHandler;
                OnPlayerRegister?.Invoke(inputHandler);
                Debug.Log("Player2 - Joined");
                return true;
            }
            
            joinProxy.canJoin = false;
            Debug.LogError("Player Connection Full - Deactivated Input");
            return false;
        }

        public void UnRegisterPlayer(InputHandler inputHandler)
        {
            var handler = inputHandler.GetComponent<InputHandler>();
            
            if (handler == null) return;

            if (handler1 == handler)
            {
                handler1 = null;
                joinProxy.canJoin = true;
                OnPlayerUnRegister?.Invoke(handler);
                Debug.Log("Player1 - Left");
            }
            else if (handler2 == handler)
            {
                handler2 = null;
                joinProxy.canJoin = true;
                OnPlayerUnRegister?.Invoke(handler);
                Debug.Log("Player2 - Left");
            }
        }
        
        public void SwitchMap(string map)
        {
            if (handler1) handler1.SwitchMap(map);
            if (handler2) handler2.SwitchMap(map);
        }

        public void Reset()
        {
            if(handler1)
                Destroy(handler1.gameObject);
            if(handler2)
                Destroy(handler2.gameObject);
            handler1 = null;
            handler2 = null;
            joinProxy.canJoin = true;
        }
    }
}