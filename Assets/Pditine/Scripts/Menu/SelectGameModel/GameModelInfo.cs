using Pditine.Scripts.Data.GameModule;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pditine.Scripts.SelectGameModuleScene
{
    public class GameModelInfo : MonoBehaviour
    {
        public RectTransform target;
        [SerializeField]private Image theImage;
        [SerializeField] private TextMeshProUGUI gameModelName;
        [SerializeField] private TextMeshProUGUI introduction;
        [SerializeField] private Image startBtn;

        public bool Enable
        {
            set
            {
                theImage.enabled = value;
                gameModelName.enabled = value;
                introduction.enabled = value;
                startBtn.enabled = value;
            }
        }
        
        [Range(0,1)][SerializeField]private float speed;

        private void FixedUpdate()
        {
            Move();
        }

        public void SetInfo(GameModelBase info)
        {
            theImage.sprite = info.Preview;
            gameModelName.text = info.ModuleName;
            introduction.text = info.Introduction;
        }

        private void Move()
        {
            if (transform.position.x.Equals(target.position.x)) return;
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
        }
    }
}