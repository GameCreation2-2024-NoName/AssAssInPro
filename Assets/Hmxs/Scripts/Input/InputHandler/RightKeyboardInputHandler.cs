// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_12_08
// -------------------------------------------------

using System;
using Pditine.Data;
using PurpleFlowerCore;
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
        // public override Vector2 Direction => _targetDirection;
        public override Device Device => Device.RightKeyboard;

        private bool _isGamePlay;

        private bool _isContinuous = true;
        
        public override void SwitchMap(string map)
        {
            _isGamePlay = map == "GamePlay";
        }

        private Vector2 _currentDirection;
        
        private Vector2 _targetDirection;

        public void Start()
        {
            DebugSystem.AddCommand("KeyBoard/RightKeyboard/Continuous\\EightDirDirection", () =>
            {
                _isContinuous = !_isContinuous;
                PFCLog.Info("RightKeyboard", $"IsContinuous:{_isContinuous}");
            });
        }

        private void Update()
        {
            _targetDirection = new Vector2(Input.GetAxisRaw("RightHorizontal"), Input.GetAxisRaw("RightVertical"));
        }

        private void FixedUpdate()
        {
            if(_isContinuous && _targetDirection != Vector2.zero)
                _currentDirection = new Vector2(Input.GetAxis("RightHorizontal"), Input.GetAxis("RightVertical"));
            else
                _currentDirection = _targetDirection;
        }
    }
}