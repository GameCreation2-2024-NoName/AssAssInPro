// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_12_08
// -------------------------------------------------

using Pditine.Data;
using UnityEngine;

namespace Hmxs.Scripts
{
    public class RightKeyboardInputHandler : InputHandler
    {
        public override bool Confirm => Input.GetKeyDown(KeyCode.Slash);
        public override Vector2 Select => _targetDirection;
        public override bool Dash => Input.GetKeyUp(KeyCode.Slash);
        public override bool Charge => Input.GetKey(KeyCode.Slash);
        public override Vector2 Direction => _currentDirection;
        public override Device Device => Device.RightKeyboard;

        private bool _isGamePlay;
        
        public override void SwitchMap(string map)
        {
            _isGamePlay = map == "GamePlay";
        }

        private Vector2 _currentDirection;
        private Vector2 _targetDirection;
        
        private void Update()
        {
            _targetDirection = new Vector2(Input.GetAxis("RightHorizontal"), Input.GetAxis("RightVertical"));
        }

        private void FixedUpdate()
        {
            if(_isGamePlay)
                _currentDirection = Vector2.Lerp(_currentDirection, _targetDirection, DataManager.Instance.OtherGameData.keyBoardRotateSpeed);
        }
    }
}