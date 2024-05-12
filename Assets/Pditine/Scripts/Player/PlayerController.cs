using System;
using Hmxs.Scripts;
using MoreMountains.Feedbacks;
using Pditine.Audio;
using Pditine.GamePlay.Buff;
using Pditine.GamePlay.UI;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pditine.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region 属性

        [SerializeField]private int id;
        public int ID => id;

        [HideInInspector] public float initialVelocityMulAdjustment = 1;
        [HideInInspector] public float initialVelocityAddAdjustment = 0;
        public float InitialVelocity =>
            _theAss.Data.InitialVelocity * initialVelocityMulAdjustment + initialVelocityAddAdjustment;

        [HideInInspector] public float frictionMulAdjustment = 1;
        [HideInInspector] public float frictionAddAdjustment = 0;

        public float Friction => (_theAss.Data.Friction + _theThorn.Data.Friction) * frictionMulAdjustment +
                                 frictionAddAdjustment;
        
        [HideInInspector] public float cdMulAdjustment = 1;
        [HideInInspector] public float cdAddAdjustment = 0;
        private float CD => _theThorn.Data.CD * cdMulAdjustment + cdAddAdjustment;

        [HideInInspector] public float hpMulAdjustment = 1;
        [HideInInspector] public int hpAddAdjustment = 0;
        public int HP => (int)(_theAss.Data.HP * hpMulAdjustment + hpAddAdjustment);

        [HideInInspector] public float atkMulAdjustment = 1;
        [HideInInspector] public int atkAddAdjustment = 0;
        public int ATK => (int)(_theThorn.Data.ATK * atkMulAdjustment + atkAddAdjustment);
        
        private int _currentHP;
        public int CurrentHP => _currentHP;
        
        private float _currentCD;
        public float CurrentCD=>_currentCD;
        
        [HideInInspector]public float CurrentSpeed;
        [SerializeField] private float rotateSpeed;
        private Vector2 _inputDirection;
        public Vector2 Direction;
        
        public bool canMove;
        public bool isInvincible;
        public float targetScale = 1;

        #endregion

        #region 引用
        
        private ThornBase _theThorn;
        public ThornBase TheThorn => _theThorn;
        private AssBase _theAss;
        public AssBase TheAss => _theAss;

        private InputHandler _inputHandler;
        public InputHandler InputHandler => _inputHandler;

        [SerializeField] private DirectionArrow arrow;

        #endregion

        #region 其他变量

        [Title("Effect")]
        [SerializeField] private MMF_Player hitFeedback;
        [SerializeField] private MMF_Player loseFeedback;
        [SerializeField]private MMScaleShaker _scaleShaker;

        private bool _isPause;
        public bool IsPause => _isPause;

        #endregion

        #region 事件
        public event Action<float> OnChangeCD;
        public event Action<int,int> OnChangeHP;
        public event Action OnDestroyed;

        #endregion
        // [Title("Audios")]
        // [SerializeField] private MMF_Player pushAudio;
        // [SerializeField] private MMF_Player slowdownAudio;

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart() { }

        private void OnEnable()
        {
            EventSystem.AddEventListener("Pause",Pause);
            EventSystem.AddEventListener("UnPause",UnPause);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("Pause",Pause);
            EventSystem.RemoveEventListener("UnPause",UnPause);
        }

        private void Pause() => _isPause = true;
        private void UnPause() => _isPause = false;

        private void FixedUpdate()
        {
            UpdateTransform();
            ReduceSpeed();
        }

        private void Update()
        {
            UpdateCD();
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            if(InputHandler.Dash)Dash();
            if(InputHandler.Direction.sqrMagnitude != 0)ChangeDirection(InputHandler.Direction);
        }

        public void Init(ThornBase theThorn,AssBase theAss)
        {
            Direction = transform.right;
            _inputHandler = id==1?PlayerManager.Instance.Handler1: PlayerManager.Instance.Handler2;
            _theAss = theAss;
            _theThorn = theThorn;
            _currentHP = HP;
        }
        
        public void ChangeDirection(Vector3 direction)
        {
            if (!canMove) return;
            if (_isPause) return;
            if (InputHandler.IsKeyboard)
            {
                _inputDirection = (Camera.main.ScreenToWorldPoint(direction) - transform.position).normalized;
                _inputDirection = _inputDirection.normalized; // LJH:奇怪的bug
            }
            else
                _inputDirection = direction;
            
            arrow.ChangeDirection(_inputDirection);
        }
        
        public void Dash()
        {
            if (!canMove) return;
            if (_isPause) return;
            if (_currentCD > 0) return;
            AAIAudioManager.Instance.PlayEffect("加速音效");
            Direction = _inputDirection;
            CurrentSpeed = InitialVelocity;
            _currentCD = CD;
        }

        private void ReduceSpeed()
        {
            CurrentSpeed -= Friction*Time.deltaTime;
            if (CurrentSpeed <= 0) CurrentSpeed = 0;
        }

        private void UpdateTransform()
        {
            transform.position += (Vector3)Direction*(CurrentSpeed*Time.deltaTime);
            transform.right = Vector3.Lerp(transform.right, Direction, rotateSpeed);
        }

        private void UpdateCD()
        {
            _currentCD -= Time.deltaTime;
            if (_currentCD <= 0) _currentCD = 0;
            OnChangeCD?.Invoke(_currentCD/CD);
        }

        public void ChangeHP(int delta)
        {
            if (delta < 0 && isInvincible) return;
            _currentHP += delta;
            if (_currentCD > HP) _currentCD = HP;
            OnChangeHP?.Invoke(_currentHP,ID);
        }
        
        public void BeDestroy()
        {
            canMove = false;
            
            DelayUtility.Delay(3f, () =>
            {
                loseFeedback.PlayFeedbacks();
            });
            DelayUtility.Delay(4, () =>
            {
                Destroy(gameObject);
            });
            OnDestroyed?.Invoke();
        }

        public void ResetProperty()
        {
            initialVelocityMulAdjustment = 1;
            atkMulAdjustment = 1;
            cdMulAdjustment = 1;
            frictionMulAdjustment = 1;
            hpMulAdjustment = 1;

            atkAddAdjustment = 0;
            cdAddAdjustment = 0;
            frictionAddAdjustment = 0;
            hpAddAdjustment = 0;
            initialVelocityAddAdjustment = 0;
        }

        public void HitFeedback() => hitFeedback.PlayFeedbacks();
        public void LoseFeedback() => loseFeedback.PlayFeedbacks();
    }
}