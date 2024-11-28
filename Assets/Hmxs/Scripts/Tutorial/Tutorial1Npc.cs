using Pditine.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts.Tutorial
{
    public class Tutorial1Npc : PlayerController
    {
        [Title("IdleState")]
        [MinMaxSlider(0, 5)]
        [SerializeField] private Vector2 idleTimeRange;

        [Title("ChargeState")]
        [MinMaxSlider(0, 5)]
        [SerializeField] private Vector2 chargeTimeRange;
        [SerializeField] private float maxDistance;

        [Title("AttackState")]
        [SerializeField] private AimMode aimMode;
        [SerializeField] private float directAttackDistance = 2f;
        [SerializeField] private LayerMask wallLayer;

        private enum AimMode
        {
            Direct,
            Prediction
        }
        private enum NpcState
        {
            Idle,
            Charging,
            Attack
        }

        private NpcState _currentState;

        private float _idleTime;
        private float _chargeTime;
        private float _counter;

        public override void ChangeDirection(Vector3 direction)
        {
            if (!canMove) return;
            if (IsPause) return;
            InputDirection = direction;
            OnChangeCurrentDirection?.Invoke(InputDirection);
        }

        protected override void OnUpdate()
        {
            if (!canMove) return;
            if (IsPause) return;

            switch (_currentState)
            {
                case NpcState.Idle:
                    OnIdle();
                    break;
                case NpcState.Charging:
                    OnCharging();
                    break;
                case NpcState.Attack:
                    OnAttack();
                    break;
            }

            AimPlayer();
        }

        private void OnIdle()
        {
            _counter += Time.deltaTime;
            if (_counter >= _idleTime) ChangeState(NpcState.Charging);
        }

        private void OnCharging()
        {
            _counter += Time.deltaTime;
            Charge();
            if (_counter >= _chargeTime || ChargeDone) ChangeState(NpcState.Attack);
        }

        private void OnAttack()
        {
            Dash();
            ChangeState(NpcState.Idle);
        }

        private void ChangeState(NpcState state)
        {
            _currentState = state;

            switch (state)
            {
                case NpcState.Idle:
                    _counter = 0f;
                    _idleTime = Random.Range(idleTimeRange.x, idleTimeRange.y);
                    break;
                case NpcState.Charging:
                    _counter = 0f;
                    var distance = Vector2.Distance(transform.position, Tutorial1Manager.Instance.PlayerTransform.position);
                    _chargeTime = Mathf.Lerp(chargeTimeRange.x, chargeTimeRange.y, distance / maxDistance);
                    break;
                case NpcState.Attack:
                    break;
            }
        }

        private void AimPlayer()
        {
            Vector2 dashDirection = Vector2.zero;
            switch (aimMode)
            {
                case AimMode.Direct:
                    dashDirection = AimDirectly();
                    break;
                case AimMode.Prediction:
                    dashDirection = AimPrediction();
                    break;
            }
            ChangeDirection(dashDirection);
        }

        private Vector2 AimDirectly()
        {
            var player = Tutorial1Manager.Instance.PlayerTransform.position;
            return player - transform.position;
        }

        private Vector2 AimPrediction()
        {
            var position = (Vector2)transform.position;
            var playerPosition = (Vector2)Tutorial1Manager.Instance.PlayerTransform.position;
            var playerVelocity = Tutorial1Manager.Instance.Player.CurrentSpeed;
            var playerDirection = Tutorial1Manager.Instance.Player.CurrentDirection;
            playerPosition += playerDirection * playerVelocity;

            Vector2 dashDirection;
            if (Vector2.Distance(playerPosition, position) < directAttackDistance)
            {
                // Direct attack
                dashDirection = playerPosition - position;
                return dashDirection;
            }

            // Indirect attack
            var direction = playerPosition - position;
            var leftPoint = Physics2D.Raycast(position, Vector2.left, 20, wallLayer).point;
            var rightPoint = Physics2D.Raycast(position, Vector2.right, 20, wallLayer).point;
            var topPoint = Physics2D.Raycast(position, Vector2.up, 20, wallLayer).point;
            var bottomPoint = Physics2D.Raycast(position, Vector2.down, 20, wallLayer).point;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.y > 0)
                {
                    // top
                    var player2Top = topPoint - position;
                    var top2Mid = (direction.x > 0 ? Vector2.right : Vector2.left) * (Mathf.Abs(direction.x) / 2);
                    dashDirection = player2Top + top2Mid;
                }
                else
                {
                    // bottom
                    var player2Bottom = bottomPoint - position;
                    var bottom2Mid = (direction.x > 0 ? Vector2.right : Vector2.left) * (Mathf.Abs(direction.x) / 2);
                    dashDirection = player2Bottom + bottom2Mid;
                }
            }
            else
            {
                if (direction.x > 0)
                {
                    // right
                    var player2Right = rightPoint - position;
                    var right2Mid = (direction.y > 0 ? Vector2.up : Vector2.down) * (Mathf.Abs(direction.y) / 2);
                    dashDirection = player2Right + right2Mid;
                }
                else
                {
                    // left
                    var player2Left = leftPoint - position;
                    var left2Mid = (direction.y > 0 ? Vector2.up : Vector2.down) * (Mathf.Abs(direction.y) / 2);
                    dashDirection = player2Left + left2Mid;
                }
            }
            return dashDirection;
        }
    }
}