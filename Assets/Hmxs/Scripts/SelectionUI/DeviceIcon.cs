using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public enum DeviceType
    {
        Null,Gamepad,Mouse
    }
    public class DeviceIcon : MonoBehaviour
    {
        [SerializeField] private Sprite nullNothing;
        [SerializeField] private Sprite gamepad;
        [SerializeField] private Sprite mouse;
        [SerializeField] private Image theImage;
        
        public void ChangeDevice(DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.Null:
                    theImage.sprite = nullNothing;
                    break;
                case DeviceType.Gamepad:
                    theImage.sprite = gamepad;
                    break;
                case DeviceType.Mouse:
                    theImage.sprite = mouse;
                    break;
            }
            theImage.SetNativeSize();
        }
    }
}