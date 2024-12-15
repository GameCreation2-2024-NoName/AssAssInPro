// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_12_15
// -------------------------------------------------

using UnityEngine;

namespace Hmxs.Scripts
{
    public class TouchScreenInputHandler : InputHandler
    {
        public override bool Confirm => false;
        public override Vector2 Select => new(0,0);

        public override bool Dash
        {
            get
            {
                if(Input.touchCount>0)
                    return Input.GetTouch(0).phase == TouchPhase.Ended;
                return false;
            }
        }
        public override bool Charge => Input.touchCount > 0;
        public override Vector2 Direction
        {
            get
            {
                if(Input.touchCount>0)
                    return Input.GetTouch(0).position;
                return new Vector2(0,0);
            }
        }
        public override Device Device => Device.TouchScreen;
        public override void SwitchMap(string map)
        {
            
        }
    }
}