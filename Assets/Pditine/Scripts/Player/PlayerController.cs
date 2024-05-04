using System;
using Hmxs.Scripts;
using MoreMountains.Feedbacks;
using Pditine.Audio;
using Pditine.GamePlay.UI;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using PurpleFlowerCore.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pditine.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region 属性

        [SerializeField]private int id;
        public int ID => id;
        public float InitialVelocity=>_theAss.Data.InitialVelocity;
        public float Friction=>_theAss.Data.Friction+_theThorn.Data.Friction;
        private float CD => _theThorn.Data.CD;
        
        public int HP => _theAss.Data.HP;
        public int ATK => _theThorn.Data.ATK;

        private int _currentHP;
        public int CurrentHP => _currentHP;
        
        private float _currentCD;
        public float CurrentCD=>_currentCD;
        
        [HideInInspector]public float CurrentSpeed;
        [SerializeField] private float rotateSpeed;
        private Vector2 _inputDirection;
        [HideInInspector]public Vector2 Direction;
        
        public bool CanMove;

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

        #region 事件
        public event Action<float> OnChangeCD;
        public event Action<int> OnChangeHP;
        public event Action OnDestroyed;

        #endregion

        [Title("Effect")]
        [SerializeField] private MMF_Player hitFeedback;
        [SerializeField] private MMF_Player loseFeedback;
        [SerializeField]private MMScaleShaker _scaleShaker;

        [Title("Audios")]
        [SerializeField] private MMF_Player pushAudio;
        [SerializeField] private MMF_Player slowdownAudio;

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
            Direction = transform.right;
            _inputHandler = id==1?PlayerManager.Instance.Handler1: PlayerManager.Instance.Handler2;
            _theAss = theAss;
            _theThorn = theThorn;
            _currentHP = HP;
            UIManager.Instance.Init(this);
        }
        
        public void ChangeDirection(Vector3 direction)
        {
            if (!CanMove) return;
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
            if (!CanMove) return;
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

        private void UpdateCD()
        {
            _currentCD -= Time.deltaTime;
            if (_currentCD <= 0) _currentCD = 0;
            OnChangeCD?.Invoke(_currentCD/CD);
        }

        public void ChangeHP(int delta)
        {
            _currentHP += delta;
            OnChangeHP?.Invoke(_currentHP);
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
            OnDestroyed?.Invoke();
        }
        
        public void HitFeedback() => hitFeedback.PlayFeedbacks();
        public void LoseFeedback() => loseFeedback.PlayFeedbacks();
    }
}