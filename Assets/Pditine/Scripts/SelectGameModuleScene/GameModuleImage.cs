using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Scripts.SelectGameModuleScene
{
    public class GameModuleImage : MonoBehaviour
    {
        public RectTransform target;
        [SerializeField]private Image theImage;

        public Sprite Sprite
        {
            set => theImage.sprite = value;
        }

        public bool Enable
        {
            get => theImage.enabled;
            set => theImage.enabled = value;
        }
        
        [Range(0,1)][SerializeField]private float speed;

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (transform.position.x.Equals(target.position.x)) return;
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
        }
    }
}