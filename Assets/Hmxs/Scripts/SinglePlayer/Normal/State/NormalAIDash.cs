using System.Collections.Generic;
using Hmxs.Scripts.FSM;
using UnityEngine;

namespace Hmxs.Scripts.SinglePlayer.Normal.State
{
    public class NormalAIDash : StateBase<NormalAIState>
    {
        private struct DirectionWeight
        {
            public float PlayerWeight { get; private set; }

            public float LightBallWeight { get; private set; }

            public float BarrierWeight { get; private set; }

            public DirectionWeight(float playerWeight, float lightBallWeight, float barrierWeight)
            {
                PlayerWeight = playerWeight;
                LightBallWeight = lightBallWeight;
                BarrierWeight = barrierWeight;
            }

            public DirectionWeight(float i)
            {
                PlayerWeight = Random.Range(0f, i);
                LightBallWeight = Random.Range(0f, i);
                BarrierWeight = Random.Range(0f, i);
            }

            public void Normalize()
            {
                var sum = PlayerWeight + LightBallWeight + BarrierWeight;
                PlayerWeight /= sum;
                LightBallWeight /= sum;
                BarrierWeight /= sum;
            }
        }

        private readonly NormalAIPlayer _aiPlayer;
        private readonly float _detectRadius;
        private readonly List<Vector3> _lightBallList = new();
        private readonly List<Vector3> _barrierList = new();

        public NormalAIDash(NormalAIState type, ITransition<NormalAIState> transition, GameObject owner,
            float detectRadius)
            : base(type, transition, owner)
        {
            _aiPlayer = Owner.GetComponent<NormalAIPlayer>();
            _detectRadius = detectRadius;
            _lightBallList.Clear();
            _barrierList.Clear();
        }

        public override void OnEnter()
        {
            var hitColliders = Physics.OverlapSphere(_aiPlayer.transform.position, _detectRadius);
            foreach (var hitCollider in hitColliders)
            {
                switch (hitCollider.gameObject.tag)
                {
                    case "LightBall":
                        _lightBallList.Add((hitCollider.transform.position - Owner.transform.position).normalized);
                        break;
                    case "BarrierPedestal":
                        _barrierList.Add((hitCollider.transform.position - Owner.transform.position).normalized);
                        break;
                }
            }
            Debug.Log("lightBall: " + _lightBallList.Count + " barrier: " + _barrierList.Count);

            var weight = Random.Range(0, 5) switch
            {
                0 => new DirectionWeight(1),
                1 => new DirectionWeight(1, 1, 1),
                2 => new DirectionWeight(1, 0, 0),
                3 => _lightBallList.Count > 0 ?
                    new DirectionWeight(0, 1, 0) :
                    new DirectionWeight(1),
                4 => _barrierList.Count > 0 ?
                    new DirectionWeight(0, 0, 1) :
                    new DirectionWeight(1),
                _ => new DirectionWeight(1)
            };
            weight.Normalize();

            var direction = Vector3.zero;

            foreach (var lightBall in _lightBallList)
            {
                direction += lightBall.normalized * weight.LightBallWeight;
                Debug.Log("lightBall: " + lightBall.normalized);
            }

            foreach (var barrier in _barrierList)
            {
                direction += barrier.normalized * weight.BarrierWeight;
                Debug.Log("barrier: " + barrier.normalized);
            }
            var target = SinglePlayerGameManager.Instance.Player.position;
            direction += (target - Owner.transform.position).normalized * weight.PlayerWeight;
            _aiPlayer.ChangeDirection(direction.normalized);
            _aiPlayer.Dash();
        }

        public override void OnExit()
        {
            _lightBallList.Clear();
            _barrierList.Clear();
        }

        public override void OnUpdate()
        {
        }
    }
}