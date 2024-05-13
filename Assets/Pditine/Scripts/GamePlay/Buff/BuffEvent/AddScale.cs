using System;
using UnityEngine;
using System.Collections;
using Pditine.Player;
using PurpleFlowerCore;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "AddScale",menuName = "AssAssIn/BuffEvent/AddScale")]
    public class AddScale : BuffEvent
    {
        [SerializeField] private float deltaScale;
        [SerializeField][Range(0,1)] private float speed;
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.targetScale += deltaScale;
            if (thePlayer.targetScale < 0)
            {
                PFCLog.Error("数值错误");
            }
            MonoSystem.Start_Coroutine(DoAddScale(thePlayer));
        }

        private IEnumerator DoAddScale(PlayerController thePlayer)
        { 
            while (Mathf.Abs(thePlayer.transform.localScale.x - thePlayer.targetScale)>0.001f)
            {
                float playerScale = thePlayer.transform.localScale.x;
                float res = Mathf.Lerp(playerScale, thePlayer.targetScale, speed);
                thePlayer.transform.localScale = new Vector3(res, res, res);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}