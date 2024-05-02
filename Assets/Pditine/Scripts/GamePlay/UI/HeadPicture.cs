using Pditine.Player;
using Pditine.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class HeadPicture : MonoBehaviour
    {
        [SerializeField] private Image thornImage;
        [SerializeField] private Image assImage;
        
        public void Init(PlayerController thePlayer)
        {
            thornImage.sprite = thePlayer.TheThorn.Data.Portrait;
            assImage.sprite = thePlayer.TheAss.Data.Portrait;
        }
    }
}