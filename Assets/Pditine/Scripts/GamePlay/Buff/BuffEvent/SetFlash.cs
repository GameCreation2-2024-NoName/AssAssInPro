using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "SetFlash",menuName = "AssAssIn/BuffEvent/SetFlash")]
    public class SetFlash : BuffEvent
    {
        [SerializeField] private bool openFlash;
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            if(openFlash)
            {
                thePlayer.TheThorn.SpriteEffectFlash.FlashOn();
                thePlayer.TheAss.SpriteEffectFlash.FlashOn();
            }else
            {
                thePlayer.TheThorn.SpriteEffectFlash.FlashOff();
                thePlayer.TheAss.SpriteEffectFlash.FlashOff();
            }
            
        }
    }
}