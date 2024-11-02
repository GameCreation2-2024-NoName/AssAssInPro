using System;
using System.Collections.Generic;
using Pditine.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    [Obsolete]
    public class PlayerHP : MonoBehaviour
    {
        // private List<HeartUI> hearts = new();
        // [SerializeField] private HeartUI heartPrototype;
        private float hp;
        
        [SerializeField]private Image hpBar0;
        [SerializeField]private Image hpBar1;
        [SerializeField]private Image hpBar2;
        
        public void Init(PlayerController thePlayer)
        {
            thePlayer.OnChangeHP += ChangeHp;
            hp = thePlayer.HP;
        }
        
        
        
        private void ChangeHp(float currentHP, int _)
        {
            hpBar2.fillAmount = currentHP / hp;
        }
        
        // public void CreateHearts(int hp)
        // {
        //     foreach (var heart in hearts)
        //     {
        //         Destroy(heart);
        //     }
        //     int heartNum = hp / 2;
        //     for (int i = 0; i < heartNum; i++)
        //     {
        //         var theHeart = Instantiate(heartPrototype.gameObject, transform).GetComponent<HeartUI>();
        //         hearts.Add(theHeart);
        //         theHeart.SetHp(2);
        //     }
        //     if(hp%2>0)
        //     {
        //         var theHeart = Instantiate(heartPrototype.gameObject, transform).GetComponent<HeartUI>();
        //         hearts.Add(theHeart);
        //         theHeart.SetHp(1);
        //     }
        // }
        
        // private void ChangeHp(flaot hp,int _)
        // {
        //     int num1 = hp / 2;
        //     int num2 = hp % 2;
        //     foreach (var h in hearts)
        //     {
        //         if(num1>0)
        //         {
        //             h.SetHp(2);
        //             num1--;
        //         }
        //         else if (num2 > 0)
        //         {
        //             h.SetHp(1);
        //             num2--;
        //         }else 
        //             h.SetHp(0);
        //     }
        // }
    }
}