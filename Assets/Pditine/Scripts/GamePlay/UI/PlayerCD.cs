using Pditine.Player;
using Pditine.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class PlayerCD : MonoBehaviour
    {
        [SerializeField]private Image _cdUI;

        public void Init(PlayerController thePlayer)
        {
            thePlayer.OnChangeCD += UpdateCD;
        }

        private void UpdateCD(float fillCD)
        {
            _cdUI.fillAmount = fillCD;
        }
    }
}