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

namespace Pditine.GamePlay.GameManager
{
    public abstract class GameManagerBase<T> : MonoBehaviour where T: GameManagerBase<T>
    {
        protected bool GameOver;
        
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
            DelayUtility.Delay(5.5f,()=>
            {
                PlayerCanMove(true);
            }); 
            PlayerManager.Instance.SwitchMap("GamePlay");
        }

        public virtual void SetPause()
        {
            if (GameOver) return;
            IsPause = !IsPause;
            Time.timeScale = IsPause ? 0 : 1;
            EventSystem.EventTrigger(IsPause ? "Pause" : "UnPause");
        }
        
        protected virtual void CreatePlayer(int assID,int thornID,PlayerController thePlayer)
        {
            AssBase theAss = Instantiate(DataManager.Instance.GetAssData(assID).Prototype).GetComponent<AssBase>();
            ThornBase theThorn = Instantiate(DataManager.Instance.GetThornData(thornID).Prototype).GetComponent<ThornBase>();
            theAss.Init(thePlayer);
            theThorn.Init(thePlayer);
            thePlayer.Init(theThorn,theAss);
        }
        
        protected virtual void CheckPlayerDead(int hp,int playerID)
        {
            if(GameOver)return;
            if (hp > 0) return;
            GameOver = true;
            var theLoser = playerID == 1 ? player1 : player2;
            var theWinner = playerID == 1 ? player2 : player1;
            PlayerCanMove(false);
            theLoser.BeDestroy();
            UIManager.Instance.GameOver(theWinner);
            EventSystem.EventTrigger("GameOver");
        }

        public void PlayerCanMove(bool canMove)
        {
            player1.canMove = canMove;
            player2.canMove = canMove;
        }
    }
}