using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using Pditine.Scripts.Data.Ass;
using Pditine.Scripts.Player;
using UnityEngine;

namespace Pditine.Player.Ass
{
    public abstract class AssBase : ColliderBase
    {
        protected PlayerController thePlayer;
        public PlayerController ThePlayer => thePlayer;

        [SerializeField]protected AssDataBase data;
           
        public AssDataBase Data => data;

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
        }

        // public virtual float GetHP()
        // {
        //     return data.HP;
        // }
        //
        // public virtual float GetFriction()
        // {
        //     return data.Friction;
        // }
        //
        // public virtual float GetInitialVelocity()
        // {
        //     return data.InitialVelocity;
        // }
        
        // public void Init(AssDataBase theData)
        // {
        //     data = theData;
        // }

        // [HideInInspector]public float CurrentScale;
        // [SerializeField] private float assMinScale;

        // private void Start()
        // {
        //     CurrentScale = transform.localScale.x;
        // }
        //
        // private void FixedUpdate()
        // {
        //     DoChangeScale();
        // }
        // public void ChangeScale(float delta)
        // {
        //     CurrentScale += delta;
        //     if (CurrentScale < assMinScale)
        //         CurrentScale = assMinScale;
        // }
        //
        // private void DoChangeScale()
        // {
        //     if (transform.localScale.x.Equals(CurrentScale)) return;
        //     transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(CurrentScale,CurrentScale,CurrentScale),0.02f);
        // }
    }
}