using Pditine.Player;
using Pditine.Scripts.Player;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.GamePlay.UI
{
    public class UIManager : SingletonMono<UIManager>
    {
        [SerializeField] private PlayerCD cd1;
        [SerializeField] private PlayerCD cd2;
        [SerializeField] private HeadPicture head1;
        [SerializeField] private HeadPicture head2;
        [SerializeField] private PlayerHP hp1;
        [SerializeField] private PlayerHP hp2;
        
        public void Init(PlayerController thePlayer)
        {
            if (thePlayer.ID == 1)
            {
                cd1.Init(thePlayer);
                head1.Init(thePlayer);
                hp1.Init(thePlayer);
            }else if (thePlayer.ID == 2)
            {
                cd2.Init(thePlayer);
                head2.Init(thePlayer);
                hp2.Init(thePlayer);
            }else PFCLog.Error("玩家ID错误");
        }
    }
}