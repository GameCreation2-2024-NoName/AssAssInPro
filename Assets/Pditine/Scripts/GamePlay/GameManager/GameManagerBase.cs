using Hmxs.Scripts;
using MoreMountains.Feedbacks;
using Pditine.Data;
using Pditine.GamePlay.Buff;
using Pditine.GamePlay.UI;
using Pditine.Player;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Scripts.Data.DatePassing;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Time = UnityEngine.Time;

namespace Pditine.GamePlay.GameManager
{
    public abstract class GameManagerBase<T> : MonoBehaviour where T: GameManagerBase<T>
    {
        protected bool IsGameOver;
        
        [SerializeField] protected MMF_Player startEffect;
        [Header("玩家")]
        [SerializeField] protected PlayerController player1;
        [SerializeField] protected PlayerController player2;
        
        protected PassingData PassingData;
        protected bool IsPause;

        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this as T;
            else
            {
                Debug.LogWarning("单例重复");
                Destroy(gameObject);
            }
        }

        protected virtual void Start()
        {
            PassingData = DataManager.Instance.PassingData;
            player1.OnChangeHP += CheckPlayerDead;
            player2.OnChangeHP += CheckPlayerDead;
            Init();
            DebugSystem.AddCommand("Player/AddHP", AddHP);
        }
        
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) SetPause();
        }
        
        protected virtual void Init()
        {
            //组装玩家
            CreatePlayer(PassingData.player1AssID,PassingData.player1ThornID,player1);
            CreatePlayer(PassingData.player2AssID,PassingData.player2ThornID,player2);
            
            BuffManager.Instance.Init(player1,player2);
            UIManager.Instance.Init(player1,player2);
            
            startEffect.PlayFeedbacks();
            DelayUtility.Delay(4.7f,()=>
            {
                PlayerCanMove(true);
                PlayerManager.Instance.SwitchMap("GamePlay");
            });
        }

        public virtual void SetPause()
        {
            if (IsGameOver) return;
            IsPause = !IsPause;
            Time.timeScale = IsPause ? 0 : 1;
            EventSystem.EventTrigger(IsPause ? "Pause" : "UnPause");
        }

        public virtual void SetPause(bool isPause)
        {
            if (IsGameOver) return;
            IsPause = isPause;
            Time.timeScale = IsPause ? 0 : 1;
            EventSystem.EventTrigger(IsPause ? "Pause" : "UnPause");
        }
        
        protected virtual void CreatePlayer(int assID,int thornID,PlayerController thePlayer)
        {
            AssBase theAss = Instantiate(DataManager.Instance.GetAssData(assID).Prototype).GetComponent<AssBase>();
            ThornBase theThorn = Instantiate(DataManager.Instance.GetThornData(thornID).Prototype).GetComponent<ThornBase>();
            thePlayer.Init(theThorn,theAss);// 保证先初始化PlayerController
            theAss.Init(thePlayer);
            theThorn.Init(thePlayer);
        }
        
        protected virtual void CheckPlayerDead(float hp,int playerID)
        {
            if(IsGameOver)return;
            if (hp > 0) return;
            if (playerID == 1 && player1.isInvincible) return;
            if (playerID == 2 && player2.isInvincible) return;

            PlayerDead(playerID);
            GameOver(playerID);
        }

        public virtual void PlayerDead(int playerID)
        {
            var theLoser = playerID == 1 ? player1 : player2;
            theLoser.BeDestroy();
        }
        
        public virtual void GameOver(int loserID)
        {
            IsGameOver = true;
            var theLoser = loserID == 1 ? player1 : player2;
            var theWinner = loserID == 1 ? player2 : player1;
            PlayerCanMove(false);
            //theLoser.BeDestroy();
            UIManager.Instance.GameOver(theWinner);
            EventSystem.EventTrigger("GameOver");
        }

        public virtual void PlayerCanMove(bool canMove)
        {
            player1.canMove = canMove;
            player2.canMove = canMove;
        }

        #region 指令

        private void AddHP()
        {
            player1.ChangeHP(100);
            player2.ChangeHP(100);
        }

        #endregion
    }
}