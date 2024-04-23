using LJH.Scripts.Collide;
using Pditine.Scripts.Data.Ass;
using UnityEngine;

namespace Pditine.Scripts.Player.Ass
{
    public abstract class AssBase : ColliderBase
    {
        protected PlayerController thePlayer;
        public PlayerController ThePlayer => thePlayer;

        [SerializeField]protected AssDataBase data;
        public AssDataBase Data => data;
        
        public void Init(PlayerController parent)
        {
            thePlayer = parent;
            transform.position = parent.transform.position;
            transform.rotation = parent.transform.rotation;
            transform.parent = parent.transform;
        }

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