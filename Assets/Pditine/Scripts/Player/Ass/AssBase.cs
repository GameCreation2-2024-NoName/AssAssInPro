using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using Pditine.Data.Ass;
using Pditine.Scripts.Data.Ass;
using UnityEngine;
using UnityEngine.Events;

namespace Pditine.Player.Ass
{
    public abstract class AssBase : ColliderBase
    {
        protected PlayerController thePlayer;
        public PlayerController ThePlayer => thePlayer;

        [SerializeField]protected AssDataBase data;
           
        public AssDataBase Data => data;

        public UnityAction OnBeAttack;

        public UnityAction OnBeAttackByThorn;

        [SerializeField] private TrailRenderer theTrail;

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new(){new Thorn_AssEvent(),new BarrierThorn_AssEvent()};
        }

        public void Init(PlayerController parent)
        {
            thePlayer = parent;
            transform.position = parent.transform.position;
            transform.rotation = parent.transform.rotation;
            transform.parent = parent.transform;

            theTrail.startColor = parent.ID == 1 ? new Color(0, 1, 1) : new Color(1, 0.8f, 0.063f);
            theTrail.endColor = parent.ID == 1 ? new Color(0, 1, 1) : new Color(1, 0.8f, 0.063f);
        }
    }
}