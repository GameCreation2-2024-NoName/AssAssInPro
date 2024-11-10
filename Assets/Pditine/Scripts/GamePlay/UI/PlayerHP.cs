using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Pditine.Player;
using PurpleFlowerCore.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class PlayerHP : MonoBehaviour
    {
        // private List<HeartUI> hearts = new();
        // [SerializeField] private HeartUI heartPrototype;
        [SerializeField]private PropertyBar Bar1;
        [SerializeField]private PropertyBar Bar2;
        [SerializeField] private Image edge;
        [SerializeField]private MMF_Player player1;
        [SerializeField]private MMF_Player player2;
        [SerializeField]private float edgeOffset;
        private MMF_Player thePlayer;
        private float _maxHP;

        private void Update()
        {
            edge.transform.position = Bar1.EdgePosition + Bar1.transform.right * edgeOffset;
        }

        public void Init(PlayerController player)
        {
            thePlayer = player.ID == 1? player1 : player2;
            _maxHP = player.HP;
            player.OnChangeHP += (hp, _) =>
            {
                // CreateHearts(hp);
                // ChangeHp(hp,_);
                thePlayer.PlayFeedbacks();
                Bar1.Value = 1 - hp / _maxHP;

                //Bar2.Value = hp / _maxHP;
            };
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