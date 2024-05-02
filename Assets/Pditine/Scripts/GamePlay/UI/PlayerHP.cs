using System.Collections.Generic;
using Pditine.Player;
using Pditine.Scripts.Player;
using UnityEngine;

namespace Pditine.GamePlay.UI
{
    public class PlayerHP : MonoBehaviour
    {
        private List<HeartUI> hearts = new();
        [SerializeField] private HeartUI heartPrototype;
        
        public void Init(PlayerController thePlayer)
        {
            thePlayer.OnChangeHP += ChangeHp;
            ChangeHp(thePlayer.HP);
        }
        
        /// <param name="hp">以0.5心为最小单位,如3颗心即6点hp</param>
        private void ChangeHp(int hp)
        {
            foreach (var heart in hearts)
            {
                Destroy(heart);
            }
            
            int heartNum = hp / 2;
            for (int i = 0; i < heartNum; i++)
            {
                var theHeart = Instantiate(heartPrototype.gameObject, transform).GetComponent<HeartUI>();
                hearts.Add(theHeart);
                theHeart.SetHp(2);
            }
            if(hp%2>0)
            {
                var theHeart = Instantiate(heartPrototype.gameObject, transform).GetComponent<HeartUI>();
                hearts.Add(theHeart);
                theHeart.SetHp(1);
            }
        }
    }
}