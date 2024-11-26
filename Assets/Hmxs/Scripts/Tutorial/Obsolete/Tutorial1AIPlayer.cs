using System;
using Pditine.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hmxs.Scripts.Tutorial
{
    [Obsolete]
    public class Tutorial1AIPlayer : PlayerController
    {
        [Title("IdleState")]
        [SerializeField] private float idleTime = 2f;

        [Title("AttackState")]
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private float directAttackDistance = 2f;
        [SerializeField] [Range(0f, 1f)] private float directAttackProbability = 0.5f;
        [SerializeField] private bool playerMovementPrediction;

        private enum Tutorial1AIPlayerState
        {
            Idle,
            Attack
        }

        private Tutorial1AIPlayerState _currentState;
        private Vector2 _leftPoint;
        private Vector2 _rightPoint;
        private Vector2 _topPoint;
        private Vector2 _bottomPoint;
        private float _counter;

        private void Start()
        {
            _currentState = Tutorial1AIPlayerState.Idle;
            _counter = 0f;
        }

        public override void ChangeDirection(Vector3 direction)
        {
            if (!canMove) return;
            if (IsPause) return;
            InputDirection = direction;
        }

        protected override void OnUpdate()
        {
            if (!canMove) return;
            if (IsPause) return;

            switch (_currentState)
            {
                case Tutorial1AIPlayerState.Idle:
                    OnIdle();
                    break;
                case Tutorial1AIPlayerState.Attack:
                    OnAttack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnIdle()
        {
            _counter += Time.deltaTime;
            if (!(_counter >= idleTime)) return;
            _counter = 0f;
            _currentState = Tutorial1AIPlayerState.Attack;
        }

        private void OnAttack()
        {
            Vector2 dashDirection;
            var player1 = (Vector2)Tutorial1GameManager.Instance.Player1.position;
            var position = (Vector2)transform.position;

            if (playerMovementPrediction)
            {
                var player1Velocity = Tutorial1GameManager.Instance.Player1.GetComponent<PlayerController>().CurrentSpeed;
                var player1Direction = Tutorial1GameManager.Instance.Player1.GetComponent<PlayerController>().CurrentDirection;
                player1 += player1Direction * player1Velocity;
            }

            if (Vector2.Distance(player1, position) < directAttackDistance || Random.value < directAttackProbability)
            {
                // Direct attack
                dashDirection = player1 - position;
                ApplyAttack(dashDirection);
                return;
            }

            // Indirect attack
            var x = player1.x - position.x;
            var y = player1.y - position.y;
            CheckWallPoint();
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (y > 0)
                {
                    // top
                    var player2Top = _topPoint - position;
                    var top2Mid = (x > 0 ? Vector2.right : Vector2.left) * (Mathf.Abs(x) / 2);
                    dashDirection = player2Top + top2Mid;
                }
                else
                {
                    // bottom
                    var player2Bottom = _bottomPoint - position;
                    var bottom2Mid = (x > 0 ? Vector2.right : Vector2.left) * (Mathf.Abs(x) / 2);
                    dashDirection = player2Bottom + bottom2Mid;
                }
            }
            else
            {
                if (x > 0)
                {
                    // right
                    var player2Right = _rightPoint - position;
                    var right2Mid = (y > 0 ? Vector2.up : Vector2.down) * (Mathf.Abs(y) / 2);
                    dashDirection = player2Right + right2Mid;
                }
                else
                {
                    // left
                    var player2Left = _leftPoint - position;
                    var left2Mid = (y > 0 ? Vector2.up : Vector2.down) * (Mathf.Abs(y) / 2);
                    dashDirection = player2Left + left2Mid;
                }
            }

            ApplyAttack(dashDirection);
        }

        private void ApplyAttack(Vector3 direction)
        {
            ChangeDirection(direction.normalized);
            Dash();
            _currentState = Tutorial1AIPlayerState.Idle;
        }

        private void CheckWallPoint()
        {
            var position = transform.position;
            _leftPoint = Physics2D.Raycast(position, Vector2.left, 20, wallLayer).point;
            _rightPoint = Physics2D.Raycast(position, Vector2.right, 20, wallLayer).point;
            _topPoint = Physics2D.Raycast(position, Vector2.up, 20, wallLayer).point;
            _bottomPoint = Physics2D.Raycast(position, Vector2.down, 20, wallLayer).point;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, directAttackDistance);
        }
    }
}