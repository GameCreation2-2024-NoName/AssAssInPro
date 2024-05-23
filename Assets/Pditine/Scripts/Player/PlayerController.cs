using System;
using Hmxs.Scripts;
using MoreMountains.Feedbacks;
using Pditine.Audio;
using Pditine.Component;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pditine.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region 属性

        [SerializeField] private int id;
        public int ID => id;

        [HideInInspector] public float initialVelocityMulAdjustment = 1;
        [HideInInspector] public float initialVelocityAddAdjustment = 0;

        public float InitialVelocity =>
            _theAss.Data.InitialVelocity * initialVelocityMulAdjustment + initialVelocityAddAdjustment;

        [HideInInspector] public float frictionMulAdjustment = 1;
        [HideInInspector] public float frictionAddAdjustment = 0;


        private float _friction;

        public float Friction => _friction * frictionMulAdjustment +
                                 frictionAddAdjustment;

        [HideInInspector] public float weightMulAdjustment = 1;
        [HideInInspector] public float weightAddAdjustment = 0;

        public float Weight => (_theAss.Data.Weight + _theThorn.Data.Weight) * weightMulAdjustment +
                               weightAddAdjustment;

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
        public float CurrentCD => _currentCD;

        [HideInInspector] public float CurrentSpeed;
        [SerializeField] private float rotateSpeed;
        protected Vector2 InputDirection;
        [ReadOnly] private Vector2 _currentDirection;

        public Vector2 CurrentDirection
        {
            get => _currentDirection;
            set
            {
                OnChangeCurrentDirection?.Invoke(value.normalized);
                _currentDirection = value.normalized;
            }
        }

        [ReadOnly] public bool canMove;
        [ReadOnly] public bool isInvincible;


        #endregion

        #region 引用

        private ThornBase _theThorn;
        public ThornBase TheThorn => _theThorn;
        private AssBase _theAss;
        public AssBase TheAss => _theAss;

        public InputHandler InputHandler =>
            id == 1 ? PlayerManager.Instance.Handler1 : PlayerManager.Instance.Handler2;

        [SerializeField] private DirectionArrow arrow;

        #endregion

        #region 其他变量

        private bool _isPause;
        public bool IsPause => _isPause;

        [HideInInspector] public float targetScale;

        #endregion

        #region 事件

        public event Action<float> OnChangeCD;
        public event Action<int> OnTryChangeHP; // 血量变化量
        public event Action<int, int> OnChangeHP; // 当前血量 玩家id
        public event Action OnDestroyed;
        public event Action<Vector3> OnChangeCurrentDirection;

        #endregion

        #region 特效

        [Title("Effect")] [SerializeField] private MMF_Player hitFeedback;
        [SerializeField] private MMF_Player loseFeedbackBlue;

        [SerializeField] private MMF_Player loseFeedbackYellow;

        //[SerializeField] private MMScaleShaker scaleShaker;
        [SerializeField] private MMF_Player beHitAssFeedbackBlue;
        [SerializeField] private MMF_Player beHitAssFeedbackYellow;
        [SerializeField] private SpriteEffect_Flash spriteEffectFlash;

        #endregion

        private void OnEnable()
        {
            EventSystem.AddEventListener("Pause", Pause);
            EventSystem.AddEventListener("UnPause", UnPause);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("Pause", Pause);
            EventSystem.RemoveEventListener("UnPause", UnPause);
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
            if (InputHandler is null) return;
            if (InputHandler.Dash) Dash();
            if (InputHandler.Direction.sqrMagnitude != 0) ChangeDirection(InputHandler.Direction);
        }

        public void Init(ThornBase theThorn, AssBase theAss)
        {
            _currentDirection = transform.right;
            targetScale = transform.localScale.x;
            _theAss = theAss;
            _theThorn = theThorn;
            _currentHP = HP;
            _friction = Weight;
            arrow.Init(id);

            OnInit();
        }

        protected virtual void OnInit()
        {
        }

        public virtual void ChangeDirection(Vector3 direction)
        {
            if (!canMove) return;
            if (_isPause) return;
            if (InputHandler.IsKeyboard)
            {
                InputDirection = (Camera.main.ScreenToWorldPoint(direction) - transform.position).normalized;
                InputDirection = InputDirection.normalized; // LJH:奇怪的bug
            }
            else
                InputDirection = direction;

            arrow.ChangeDirection(InputDirection);
        }

        public void Dash()
        {
            if (!canMove) return;
            if (_isPause) return;
            if (_currentCD > 0) return;
            AAIAudioManager.Instance.PlayEffect("加速音效");
            _currentDirection = InputDirection;
            CurrentSpeed = InitialVelocity;
            _currentCD = CD;
        }

        private void ReduceSpeed()
        {
            CurrentSpeed -= Friction * Time.deltaTime;
            if (CurrentSpeed <= 0) CurrentSpeed = 0;
        }

        private void UpdateTransform()
        {
            transform.position += (Vector3)_currentDirection * (CurrentSpeed * Time.deltaTime);
            transform.right = Vector3.Lerp(transform.right, _currentDirection, rotateSpeed);
        }

        private void UpdateCD()
        {
            _currentCD -= Time.deltaTime;
            if (_currentCD <= 0) _currentCD = 0;
            OnChangeCD?.Invoke(_currentCD / CD);
        }

        public void ChangeHP(int delta)
        {
            //Debug.Log("change hp :"+delta);

            if (delta < 0 && isInvincible)
            {
                OnTryChangeHP?.Invoke(delta);
                return;
            }

            OnTryChangeHP?.Invoke(delta);
            _currentHP += delta;
            if (_currentCD > HP) _currentCD = HP;
            OnChangeHP?.Invoke(_currentHP, ID);
        }

        public void BeDestroy()
        {
            canMove = false;

            DelayUtility.Delay(0.5f, () =>
            {
                LoseFeedback();
                _theThorn.gameObject.SetActive(false);
                _theAss.gameObject.SetActive(false);
                arrow.gameObject.SetActive(false);
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

        public void BeHitAssFeedback()
        {
            var mmfPlayer = id == 1 ? beHitAssFeedbackBlue : beHitAssFeedbackYellow;
            mmfPlayer.PlayFeedbacks();
        }

        public void LoseFeedback()
        {
            var mmfPlayer = id == 1 ? loseFeedbackBlue : loseFeedbackYellow;
            mmfPlayer.PlayFeedbacks();
        }

        public void SetFlash(bool open)
        {
            if(open)
                spriteEffectFlash.FlashOn();
            else 
                spriteEffectFlash.FlashOff();
        }
    
    }
}