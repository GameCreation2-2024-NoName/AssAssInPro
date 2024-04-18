using LJH.Scripts.Collide;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class Boundary : ColliderBase
    {
        //[SerializeField] private float offset;
        [SerializeField] private Vector2 normalDirection;

        public Vector2 NormalDirection => normalDirection;
        //[SerializeField] private float force;
        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     switch (other.gameObject.tag)
        //     {
        //         case "Player":
        //             var thePlayer = other.gameObject.GetComponent<PlayerController>();
        //             //if (!thePlayer) thePlayer = other.gameObject.GetComponentInParent<PlayerController>();
        //             var originDirection = thePlayer.Direction;
        //             Vector2 Out_Direction = Vector2.Reflect(originDirection,normalDirection);
        //
        //             thePlayer.Direction = Out_Direction;
        //         
        //             //other.transform.position += (Vector3)normalDirection*offset;
        //             break;
        //        
        //     }
        //     
        // }
    }
}