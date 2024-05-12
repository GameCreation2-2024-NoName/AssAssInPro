using Pditine.GamePlay.GameManager;
using UnityEngine;

namespace Hmxs.Scripts.SinglePlayer
{
    public class SinglePlayerGameManager : GameManagerBase<SinglePlayerGameManager>
    {
        public Transform Player => player1.transform;
    }
}