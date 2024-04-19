using System.Collections.Generic;
using Hmxs.Scripts;
using LJH.Scripts.Map;
using UnityEngine;
using LJH.Scripts.Player;
using Pditine.Scripts;
using PurpleFlowerCore;
using PurpleFlowerCore.Event;

namespace LJH.Scripts.Collide
{
    public static class CollideHandler
    {
        public static void ColliderHandle(string tag1, string tag2, ColliderBase collider1, ColliderBase collider2,bool canExchange=true)
        //public static void ColliderHandle(string tag1, string tag2, ColliderBase collider1, ColliderBase collider2)
        {
            //HapticPatterns.PlayEmphasis();
            PFCLog.Info($"碰撞处理:{tag1},{tag2}");
            
            collider1.CollisionEvent?.Invoke();
            collider2.CollisionEvent?.Invoke();
            

            if (tag1 == "Boundary" && tag2 == "Thorn")
            {
                var thePlayer = (collider2 as Thorn).ThePlayer;
                var originDirection = thePlayer.Direction;
                Vector2 Out_Direction = Vector2.Reflect(originDirection,((Boundary)collider1).NormalDirection);
                thePlayer.Direction = Out_Direction;
                thePlayer.HitFeedback();
                collider1.transform.GetComponent<VisualBox>()?.Act();
                //collider1.transform.position += (Vector3)thePlayer.Direction;
            }
            
            if (tag1 == "Boundary" && tag2 == "Ass")
            {
                var thePlayer = (collider2 as Ass).ThePlayer;
                var originDirection = thePlayer.Direction;
                Vector2 Out_Direction = Vector2.Reflect(originDirection,((Boundary)collider1).NormalDirection);

                thePlayer.Direction = Out_Direction;
                collider1.transform.GetComponent<VisualBox>()?.Act();
            }
            
            if (tag1 == "Thorn" && tag2 == "Thorn")
            {
                var thePlayer1 = (collider1 as Thorn).ThePlayer;
                var thePlayer2 = (collider2 as Thorn).ThePlayer;
                (thePlayer1.Direction, thePlayer2.Direction) = (thePlayer2.Direction, thePlayer1.Direction);
                (thePlayer1.CurrentSpeed, thePlayer2.CurrentSpeed) = (thePlayer2.CurrentSpeed, thePlayer1.CurrentSpeed);
                thePlayer1.HitFeedback();
                thePlayer2.HitFeedback();
                return;
            }
            
            if (tag1 == "BarrierThorn" && tag2 == "BarrierThorn")
            {
                var theBarrier1 = (collider1 as BarrierThorn).TheBarrier;
                var theBarrier2 = (collider2 as BarrierThorn).TheBarrier;
                
                (collider1 as BarrierThorn).TheBarrier.ThePedestal.AddCollider(collider2);
                collider2.AddCollider((collider1 as BarrierThorn).TheBarrier.ThePedestal);
                (collider2 as BarrierThorn).TheBarrier.ThePedestal.AddCollider(collider1);
                collider1.AddCollider((collider2 as BarrierThorn).TheBarrier.ThePedestal);
                
                (theBarrier1.Direction, theBarrier2.Direction) = (theBarrier2.Direction, theBarrier1.Direction);
                (theBarrier1.CurrentSpeed, theBarrier2.CurrentSpeed) = (theBarrier2.CurrentSpeed, theBarrier1.CurrentSpeed);
                theBarrier1.HitFeedback.PlayFeedbacks();
                theBarrier2.HitFeedback.PlayFeedbacks();
                return;
            }
            
            if (tag1 == "BarrierThorn" && tag2 == "BarrierPedestal")
            {
                var theBarrier1 = (collider1 as BarrierThorn).TheBarrier;
                var theBarrier2 = (collider2 as BarrierPedestal).TheBarrier;
                
                // (collider1 as BarrierThorn).TheBarrier.ThePedestal.AddCollider(collider2);
                // collider2.AddCollider((collider1 as BarrierThorn).TheBarrier.ThePedestal);
                //
                // PFCLog.Info((collider2 as BarrierPedestal).TheBarrier.TheThorn);
                // PFCLog.Info(collider1);
                // (collider2 as BarrierPedestal).TheBarrier.TheThorn.AddCollider((collider1));
                //
                // collider1.AddCollider((collider2 as BarrierThorn).TheBarrier.TheThorn);
                
                (theBarrier1.Direction, theBarrier2.Direction) = (theBarrier2.Direction, theBarrier1.Direction);
                (theBarrier1.CurrentSpeed, theBarrier2.CurrentSpeed) = (theBarrier2.CurrentSpeed, theBarrier1.CurrentSpeed);
                theBarrier1.HitFeedback.PlayFeedbacks();
                theBarrier2.HitFeedback.PlayFeedbacks();
                return;
            }

            if (tag1 == "Thorn" && tag2 == "Ass")
            {
                //CameraMoveUtility.MoveAndZoom(collider2.transform.position,0.03f,4);
                var thePlayer = (collider2 as Ass).ThePlayer;
                ClassicGameManager.Instance.PlayerDead(thePlayer.transform,thePlayer.ID);
                thePlayer.BeDestroy();
                thePlayer.LoseFeedback();
                EventSystem.EventTrigger("GameOver");
            }
            
            if (tag1 == "BarrierThorn" && tag2 == "Ass")
            {
                //CameraMoveUtility.MoveAndZoom(collider2.transform.position,0.03f,4);
                var thePlayer = (collider2 as Ass).ThePlayer;
                ClassicGameManager.Instance.PlayerDead(thePlayer.transform,thePlayer.ID);
                thePlayer.BeDestroy();
                thePlayer.LoseFeedback();
                EventSystem.EventTrigger("GameOver");
            }

            if (tag1 == "BarrierThorn" && tag2 == "Thorn")
            {
                var thePlayer = (collider2 as Thorn).ThePlayer;
                var theBarrier = (collider1 as BarrierThorn).TheBarrier;
                
                (collider1 as BarrierThorn).TheBarrier.ThePedestal.AddCollider(collider2);
                collider2.AddCollider((collider1 as BarrierThorn).TheBarrier.ThePedestal);
                
                theBarrier.Direction = thePlayer.Direction;
                thePlayer.Direction = -thePlayer.Direction;
                theBarrier.CurrentSpeed = thePlayer.CurrentSpeed/1.5f;
                thePlayer.CurrentSpeed /= 1.2f;
                thePlayer.HitFeedback();
            }
            
            if (tag1 == "BarrierPedestal" && tag2 == "Thorn")
            {
                var thePlayer = (collider2 as Thorn).ThePlayer;
                var theBarrier = (collider1 as BarrierPedestal).TheBarrier; 
                
                (collider1 as BarrierPedestal).TheBarrier.TheThorn.AddCollider(collider2);
                collider2.AddCollider((collider1 as BarrierPedestal).TheBarrier.TheThorn);
                
                theBarrier.Direction = thePlayer.Direction;
                thePlayer.Direction = -thePlayer.Direction;
                theBarrier.CurrentSpeed = thePlayer.CurrentSpeed/1.5f;
                thePlayer.CurrentSpeed /= 1.2f;
                thePlayer.HitFeedback();
            }
            
            if (tag1 == "Boundary" && tag2 == "BarrierThorn")
            {
                var theBarrier= (collider2 as BarrierThorn).TheBarrier;
                var originDirection = theBarrier.Direction;
                Vector2 Out_Direction = Vector2.Reflect(originDirection,((Boundary)collider1).NormalDirection);
                theBarrier.Direction = Out_Direction;
                collider1.transform.GetComponent<VisualBox>()?.Act();
                theBarrier.collideWithBoundary.PlayFeedbacks();
            }
            
            if (tag1 == "Boundary" && tag2 == "BarrierPedestal")
            {
                var theBarrier= (collider2 as BarrierPedestal).TheBarrier;
                var originDirection = theBarrier.Direction;
                Vector2 Out_Direction = Vector2.Reflect(originDirection,((Boundary)collider1).NormalDirection);
                theBarrier.Direction = Out_Direction;
                collider1.transform.GetComponent<VisualBox>()?.Act();
                theBarrier.collideWithBoundary.PlayFeedbacks();
            }
            if(canExchange)
                ColliderHandle(tag2,tag1,collider2,collider1,false);
        }


    }
}