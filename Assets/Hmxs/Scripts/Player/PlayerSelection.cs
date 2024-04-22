using Hmxs.Scripts.Selection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts.Player
{
    public class PlayerSelection : MonoBehaviour
    {
        [SerializeField] private int id;

        [SerializeField] [ReadOnly] private bool isReady;
        [SerializeField] [ReadOnly] private bool canSelect = true;

        [SerializeField] [ReadOnly] private int assId;
        [SerializeField] [ReadOnly] private int thornId;

        private InputHandler InputHandler =>
            id == 1 ? PlayerManager.Instance.Handler1 : PlayerManager.Instance.Handler2;


        private void Update()
        {
            if (InputHandler == null) return;

            if (InputHandler.Confirm)
                OnConfirm();

            if (InputHandler.Select != Vector2.zero && !isReady && canSelect)
                OnSelect(InputHandler.Select);
            else if (InputHandler.Select == Vector2.zero)
                canSelect = true;
        }

        private void OnSelect(Vector2 select)
        {
            canSelect = false;
            var angle = Mathf.Atan2(select.y, select.x) * Mathf.Rad2Deg;
            switch (angle)
            {
                case >= -45 and <= 45:
                    // Right
                    Debug.Log("Right");
                    break;
                case >= 45 and <= 135:
                    // Up
                    Debug.Log("Up");
                    break;
                case >= -135 and <= -45:
                    // Down
                    Debug.Log("Down");
                    break;
                default:
                    // Left
                    Debug.Log("Left");
                    break;
            }
        }

        private void OnConfirm()
        {
            Debug.Log("Confirm");
            isReady = !isReady;
        }
    }
}