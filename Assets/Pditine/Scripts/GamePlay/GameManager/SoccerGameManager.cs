using Pditine.Audio;
using Pditine.GamePlay.Buff;
using UnityEngine;

namespace Pditine.GamePlay.GameManager
{
    public class SoccerGameManager : GameManagerBase<SoccerGameManager>
    {
        [SerializeField] private string bgmName;
        [SerializeField] private BuffData theBuff;
        [SerializeField] private int neededScore;

        protected override void Start()
        {
            base.Start();
            AAIAudioManager.Instance.PlayBGM(bgmName);
            BuffManager.Instance.AttachBuff(new BuffInfo(theBuff,gameObject,player1));
            BuffManager.Instance.AttachBuff(new BuffInfo(theBuff,gameObject,player2));
        }

        public void CheckGameOver(int loserID,int score)
        {
            if (score < neededScore) return;
            GameOver(loserID);
        }
    }
}