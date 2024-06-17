using Pditine.Audio;
using UnityEngine;

namespace Pditine.GamePlay.GameManager
{
    public class ClassicGameManager : GameManagerBase<ClassicGameManager>
    {
        [SerializeField] private string bgmName;

        protected override void Start()
        {
            base.Start();
            AAIAudioManager.Instance.PlayBGM(bgmName);
        }
    }
}
