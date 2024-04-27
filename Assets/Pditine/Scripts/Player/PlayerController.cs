using System.Linq;
using Hmxs.Scripts;
using LJH.Scripts.UI;
using MoreMountains.Feedbacks;
using Pditine.Player.Thorn;
using Pditine.Scripts.Player.Ass;
using Pditine.Scripts.Player.Thorn;
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
        
        private float InitialVelocity=>_theAss.Data.InitialVelocity;
        private float Friction=>_theAss.Data.Friction+_theThorn.Data.Friction;
        private float CD => _theThorn.Data.CD;
        private float HP => _theAss.Data.HP;
        private float ATK => _theThorn.Data.ATK;
        
        [HideInInspector]public float CurrentSpeed;
        [SerializeField] private float rotateSpeed;
        private Vector2 _inputDirection;
        [SerializeField] private GameObject directionArrow;
        [HideInInspector]public Vector2 Direction;
        private PlayerCD _cdUI;

        private float _currentCD;

        [HideInInspector]public PlayerInput TheInput;
        
        private ThornBase _theThorn;
        public ThornBase TheThorn => _theThorn;
        private AssBase _theAss;
        public AssBase TheAss => _theAss;

        public bool CanMove;

        [SerializeField]private InputHandler _inputHandler;
        public InputHandler InputHandler => _inputHandler;
        // {
        //     get
        //     {
        //         if (_inputHandler is null)
        //             _inputHandler = id == 1 ? PlayerManager.Instance.Handler1 : PlayerManager.Instance.Handler2;
        //         return InputHandler;
        //     }
        // }

        [Title("Effect")]
        [SerializeField] private MMF_Player hitFeedback;
        [SerializeField] private MMF_Player loseFeedback;
        private MMScaleShaker _scaleShaker;

        [Title("Audios")]
        [SerializeField] private MMF_Player pushAudio;
        [SerializeField] private MMF_Player slowdownAudio;

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
            if(InputHandler.Dash)Dash();
            if(InputHandler.Direction.sqrMagnitude != 0)ChangeDirection(InputHandler.Direction);
        }

        public void Init(ThornBase theThorn,AssBase theAss)
        {
            //_inputHandler = id==1?PlayerManager.Instance.Handler1: PlayerManager.Instance.Handler2;
            _theAss = theAss;
            _theThorn = theThorn;
        }
        
        public void ChangeDirection(Vector3 direction)
        {
            if (!CanMove) return;
            //_inputDirection = direction;
            _inputDirection = (Camera.main.ScreenToWorldPoint(direction) - transform.position).normalized;
            directionArrow.transform.right = _inputDirection;
        }
        
        public void Dash()
        {
            if (!CanMove) return;
            if (_currentCD > 0) return;
            Direction = _inputDirection;
            CurrentSpeed = InitialVelocity;
            _currentCD = CD;
        }

        private void ReduceSpeed()
        {
            CurrentSpeed -= Friction*Time.deltaTime;
            if (CurrentSpeed <= 0) CurrentSpeed = 0;
        }

        private void UpdateCD()
        {
            _currentCD -= Time.deltaTime;
            if (_currentCD <= 0) _currentCD = 0;
            _cdUI.UpdateCD(_currentCD/CD);
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
    }
}