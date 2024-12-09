// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_12_09
// -------------------------------------------------

using UnityEngine;

namespace Hmxs.Scripts
{
    public class MouseInputHandler : InputHandler
    {
        public override bool Confirm => false;
        public override Vector2 Select => new(0,0);
        public override bool Dash => Input.GetMouseButtonUp(0);
        public override bool Charge => Input.GetMouseButton(0);
        public override Vector2 Direction => Input.mousePosition;
        public override Device Device => Device.Mouse;
        public override void SwitchMap(string map)
        {
            
        }
    }
}