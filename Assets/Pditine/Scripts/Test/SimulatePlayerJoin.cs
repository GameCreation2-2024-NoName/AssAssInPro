using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Pditine.Test
{
    public class SimulatePlayerJoin : MonoBehaviour
    {
        void Start()
        {
            // 模拟玩家加入
            SimulatePlayerJoinInput();
        }

        private void SimulatePlayerJoinInput()
        {
            // 创建一个虚拟设备
            var virtualDevice = InputSystem.AddDevice<Keyboard>();

            // 创建一个InputUser并将虚拟设备分配给它
            var user = InputUser.PerformPairingWithDevice(virtualDevice);

            // 使用虚拟设备触发玩家加入
            PlayerInputManager.instance.JoinPlayer(playerIndex: 1, pairWithDevice: virtualDevice);
        }
    }
}
