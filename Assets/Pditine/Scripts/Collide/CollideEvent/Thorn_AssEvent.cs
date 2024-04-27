namespace Pditine.Collide.CollideEvent
{
    public class Thorn_AssEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Thorn" && tag2 == "Ass") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            //         //CameraMoveUtility.MoveAndZoom(collider2.transform.position,0.03f,4);
            //         var thePlayer = (collider2 as AssBase).ThePlayer;
            //         ClassicGameManager.Instance.PlayerDead(thePlayer.transform,thePlayer.ID);
            //         thePlayer.BeDestroy();
            //         thePlayer.LoseFeedback();
            //         EventSystem.EventTrigger("GameOver");
        }
    }
}