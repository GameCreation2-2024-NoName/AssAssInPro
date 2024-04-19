using System.Collections.Generic;
using MoreMountains.Feedbacks;
using PurpleFlowerCore;
using PurpleFlowerCore.Event;
using UnityEngine;

namespace LJH.Scripts.Player
{
    public class PlayerActionManager : SingletonMono<PlayerActionManager>
    {
        private readonly List<PlayerAction> _players = new();
        [SerializeField] private GameObject prepareUILeft;
        [SerializeField] private GameObject prepareUIRight;
        [SerializeField] private GameObject readyUILeft;
        [SerializeField] private GameObject readyUIRight;
        [SerializeField] private MMF_Player mmfPlayer;

        private void OnEnable()
        {
            var gameStartUI = ProcessSystem.CreateProcess("GameStartUI");
            gameStartUI.Add(new ActionNode(() =>
            {
                foreach (var player in _players)
                {
                    player.StartFight();
                }
                mmfPlayer.PlayFeedbacks();
            })).Add(new WaitNode(4.1f)).Add(new ActionNode(() =>
            {
                foreach (var player in _players)
                {
                    player.ThePlayer.CanMove = true;
                    
                }
                Debug.Log(1111);
            }));
        }

        public void AddPlayer(PlayerAction thePlayer)
        {
            _players.Add(thePlayer);
        }

        public void CheckReady()
        {
            if (_players.Count <= 1) return;
            foreach (var player in _players)
            {
                if (!player.Ready) return;
            }

            
            CloseUI();
            //
            // mmfPlayer.PlayFeedbacks();
            // Timer.Register(mmfPlayer.GetTime(), onComplete: (() =>
            // {
            //     
            // }))
            ProcessSystem.GetProcess("GameStartUI").Start_();
            EventSystem.EventTrigger("GameStart");
        }
        
        public void SetPrepareUI(int playerId,bool ready)
        {
            if(playerId==0)
            {
                prepareUILeft.SetActive(!ready);
                readyUILeft.SetActive(ready);
            }
            else
            {
                prepareUIRight.SetActive(!ready);
                readyUIRight.SetActive(ready);
            }
        }

        public void CloseUI()
        {
            prepareUILeft.SetActive(false);
            readyUILeft.SetActive(false);
            prepareUIRight.SetActive(false);
            readyUIRight.SetActive(false);
        }
        
    }
}