using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class DeviceIcon : MonoBehaviour
    {
        [SerializeField] private Sprite nullNothing;
        [SerializeField] private Sprite gamepad;
        [SerializeField] private Sprite mouse;
        [SerializeField] private Image theImage;
        
        public void ChangeDevice(Device deviceType)
        {
            switch (deviceType)
            {
                case Device.Null:
                    theImage.sprite = nullNothing;
                    break;
                case Device.Gamepad:
                    theImage.sprite = gamepad;
                    break;
                case Device.Mouse:
                    theImage.sprite = mouse;
                    break;
            }
            theImage.SetNativeSize();
        }
    }
}