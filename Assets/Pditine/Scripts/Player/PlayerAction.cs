using HighlightPlus2D;
using Pditine.Scripts.Player;
using PurpleFlowerCore;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LJH.Scripts.Player
{
    public class PlayerAction : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private bool canSelect = true;
        private bool _ready;
        public bool Ready => _ready;
        private bool _selectAssOrThorn;
        [SerializeField]private PlayerController _thePlayer;
        public PlayerController ThePlayer => _thePlayer;
        private PlayerInput PlayerInput => GetComponent<PlayerInput>();

        private void Start()
        {
            //PlayerActionManager.Instance.AddPlayer(this);
            _thePlayer = FindObjectOfType<PlayerController>();
            _thePlayer.TheInput = PlayerInput;
        }

        public void BindPlayer(PlayerController targetPlayer)
        {
            _thePlayer = targetPlayer;
            _thePlayer.TheInput = GetComponent<PlayerInput>();
        }

        #region PlayerInput

        public void ChangeDirection(InputAction.CallbackContext ctx)
        {
            if (!_thePlayer) return;
            _thePlayer.ChangeDirection(ctx);
        }

        public void Launch(InputAction.CallbackContext ctx)
        {
            if (!_thePlayer) return;
            _thePlayer.Launch(ctx);
        }

        #endregion

        #region Prepare

        public void Select(InputAction.CallbackContext ctx)
        {
            if (_ready) return;    
            var input = ctx.ReadValue<Vector2>();
            if (!canSelect)
            {
                if (input == Vector2.zero) canSelect = true;
                return;
            }

            _thePlayer.SelectAudio();

            SetPlayerHighLight(false);
            
            var angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            switch (angle)
            {

                case >= -45 and <= 45:
                    // Right
                    //Debug.Log("Right");
                    //_thePlayer.NextAss();
                    _selectAssOrThorn = !_selectAssOrThorn;
                    break;
                case >= 45 and <= 135:
                    // Up
                    // if(_selectAssOrThorn)
                    //     _thePlayer.LastAss();
                    // else
                    //     _thePlayer.LastThorn();
                    break;
                case >= -135 and <= -45:
                    // Down
                    Debug.Log("down");
                    // if(_selectAssOrThorn)
                    //     _thePlayer.NextAss();
                    // else
                    //     _thePlayer.NextThorn();
                    break;
                default:
                    // Left
                    //_thePlayer.LastAss();
                    _selectAssOrThorn = !_selectAssOrThorn;
                    break;
            }

            SetPlayerHighLight(true);
            canSelect = false;
        }

        public void Confirm(InputAction.CallbackContext ctx)
        {
            _thePlayer.ConfirmAudio();

            _ready = !_ready;
            SetPlayerHighLight(!_ready);
            Debug.Log(!_ready);
            PlayerActionManager.Instance.SetPrepareUI(PlayerInput.playerIndex,_ready);
            PlayerActionManager.Instance.CheckReady();
        }

        public void StartFight()
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("PlayerInput");
            //ThePlayer.CanMove = true;
        }

        #endregion

        public void OnDeviceLost(PlayerInput playerInput) => Destroy(gameObject);

        private void SetPlayerHighLight(bool isHighLight)
        {
            if (_selectAssOrThorn)
                _thePlayer.TheAss.GetComponent<HighlightEffect2D>().highlighted = isHighLight;
            else
                _thePlayer.TheThorn.GetComponent<HighlightEffect2D>().highlighted = isHighLight;
        }
    }
}