using Pditine.Player;
using UnityEngine;

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
        /// 游戏场景外,确认
        public abstract bool Confirm { get; }
        /// 游戏场景外,选择
        public abstract Vector2 Select { get; }
        /// 玩家冲刺
        public abstract bool Dash { get; }
        /// 玩家蓄力
        public abstract bool Charge { get; }
        /// 玩家选择方向
        public abstract Vector2 Direction { get; }
        /// 玩家设备
        public abstract Device Device { get; }

        // /// 玩家本体，战斗开始时初始化
        // protected PlayerController player;
        // public PlayerController Player => player;
        
        /// 更改状态
        public abstract void SwitchMap(string map);
        
    }
}