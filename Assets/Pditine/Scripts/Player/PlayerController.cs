using System.Linq;
using LJH.Scripts.Player;
using LJH.Scripts.UI;
using MoreMountains.Feedbacks;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pditine.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]private int id;
        public int ID => id;
        
        private bool _isCharging;
        [HideInInspector]public float CurrentSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float friction;
        [SerializeField] private float rotateSpeed;
        private Vector2 _inputDirection;
        [SerializeField] private GameObject directionArrow;
        [HideInInspector]public Vector2 Direction;
        private PlayerCD _cdUI;
        
        [SerializeField] private float cd;
        private float _currentCD;

        [HideInInspector]public PlayerInput TheInput;
        private Thorn _theThorn;
        public Thorn TheThorn => _theThorn;
        private Ass _theAss;
        public Ass TheAss => _theAss;

        public bool CanMove;

        [Title("Effect")]
        [SerializeField] private MMF_Player hitFeedback;
        [SerializeField] private MMF_Player loseFeedback;
        private MMScaleShaker _scaleShaker;

        [Title("Audios")]
        [SerializeField] private MMF_Player pushAudio;
        [SerializeField] private MMF_Player slowdownAudio;
        [SerializeField] private MMF_Player selectAudio;
        [SerializeField] private MMF_Player confirmAudio;

        private void Start()
        {
            _scaleShaker = GetComponent<MMScaleShaker>();
            Direction = transform.right;
            _cdUI = FindObjectsOfType<PlayerCD>().FirstOrDefault(p=>p.ID == id);
            if(!_cdUI)
                PFCLog.Error("未找到UI");
        }

        private void FixedUpdate()
        {
            transform.position += (Vector3)Direction*(CurrentSpeed*Time.deltaTime);
            transform.right = Vector3.Lerp(transform.right, Direction, rotateSpeed);
        }

        private void Update()
        {
            ReduceSpeed();
            UpdateCD();
        }

        public void Init(Thorn theThorn,Ass theAss)
        {
            _theAss = theAss;
            _theThorn = theThorn;
        }
        
        public void ChangeDirection(InputAction.CallbackContext ctx)
        {
            if (!CanMove) return;
            if (!_isCharging) return;
            var tempInputDirection = _inputDirection;
            //_inputDirection = (Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>())-transform.position).normalized;
            if (HaveGamepad())
                _inputDirection = ctx.ReadValue<Vector2>().normalized;
            else
                _inputDirection = (ctx.ReadValue<Vector2>() - new Vector2(Screen.width / 2, Screen.height / 2))
                    .normalized;
            if (_inputDirection.normalized == Vector2.zero)
                _inputDirection = tempInputDirection;
            directionArrow.transform.right = _inputDirection;
        }

        private bool HaveGamepad()
        {
            return TheInput.devices.OfType<Gamepad>().Any();
        }
        
        public void Launch(InputAction.CallbackContext ctx)
        {
            if (!CanMove) return;
            if (_currentCD > 0) return;
            
            if (ctx.started)
            {
                if (_isCharging) return;
                _isCharging = true;
                directionArrow.SetActive(true);
                Debug.Log("开始蓄力");
            }
        
            if (ctx.canceled)
            {
                if (!_isCharging) return;
                _isCharging = false;
                Direction = _inputDirection;
                CurrentSpeed = maxSpeed;
                _currentCD = cd;
                directionArrow.SetActive(false);
                Debug.Log("结束蓄力");
            }
        }

        private void ReduceSpeed()
        {
            CurrentSpeed -= friction*Time.deltaTime;
            if (CurrentSpeed <= 0) CurrentSpeed = 0;
        }

        private void UpdateCD()
        {
            if(!_isCharging)
                _currentCD -= Time.deltaTime;
            if (_currentCD <= 0) _currentCD = 0;
            _cdUI.UpdateCD(_currentCD/cd);
        }

        public void ChangeSpeed(float percentageDelta)
        {
            maxSpeed *= 1 + percentageDelta*0.01f;
        }

        public void BeDestroy()
        {
            CanMove = false;
            DelayUtility.Delay(3.7f, () =>
            {
                loseFeedback.PlayFeedbacks();
            });
            DelayUtility.Delay(4, () =>
            {
                Destroy(gameObject);
            });
        }
        
        public void HitFeedback() => hitFeedback.PlayFeedbacks();
        public void LoseFeedback() => loseFeedback.PlayFeedbacks();
        public void SelectAudio() => selectAudio.PlayFeedbacks();
        public void ConfirmAudio() => confirmAudio.PlayFeedbacks();
    }
}