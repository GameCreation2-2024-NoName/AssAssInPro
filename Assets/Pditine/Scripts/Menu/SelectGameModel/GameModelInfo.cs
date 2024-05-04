using Pditine.Component;
using Pditine.Scripts.Data.GameModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Menu.SelectGameModel
{
    public class GameModelInfo : MonoBehaviour
    {
        public RectTransform target;
        [SerializeField]private Image theImage;
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        [SerializeField] private ButtonEffect_Audio theEffect;
        [SerializeField] private TextMeshProUGUI gameModelName;
        [SerializeField] private TextMeshProUGUI introduction;

        [SerializeField] private Sprite doneSprite;
        [SerializeField] private Sprite notDoneSprite;


        public bool Enable
        {
            set
            {
                theImage.enabled = value;
                gameModelName.enabled = value;
                introduction.enabled = value;
                buttonImage.enabled = value;
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
            if (info.Done)
            {
                button.enabled = true;
                buttonImage.sprite = doneSprite;
                theEffect.enabled = true;
            }
            else
            {
                button.enabled = false;
                buttonImage.sprite = notDoneSprite;
                theEffect.enabled = false;  
            }
        }

        private void Move()
        {
            if (transform.position.x.Equals(target.position.x)) return;
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
        }
    }
}