using Pditine.Audio;
using Pditine.GamePlay.GameManager;
using TMPro;
using UnityEngine;

namespace Pditine.Map
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private TextMeshPro scoreUI;
        [SerializeField] private int id;
        [SerializeField] private Transform createPoint;
        [SerializeField] private GameObject soccerPrototype;
        private int _score;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("DynamicBarrier"))
            {

                var theBall = other.GetComponent<Soccer>();
                if(theBall.HasTriggered)return;
                UpdateScore();
                theBall.Destroy();
                CreateSoccer();
            }
        }

        private void UpdateScore()
        {
            //AAIAudioManager.Instance.PlayEffect(cheerAudioName);
            _score++;
            scoreUI.text = _score.ToString();

            SoccerGameManager.Instance.CheckGameOver(id,_score);
        }
        private void CreateSoccer()
        {
            //todo:对象池
            //var theBall = PoolSystem.GetGameObject(lightBalls[Random.Range(0, lightBalls.Count)]).GetComponent<LightBall>();
        
            // var overlaps = Physics2D.OverlapPointAll(createPoint.position);
            // foreach (var overlay in overlaps)
            // {
            //     if (overlay.CompareTag("Ass")) return;
            // }
            
            var theBall = Instantiate(soccerPrototype).GetComponent<Soccer>();
            theBall.transform.position = createPoint.position;
            theBall.Init();
        }
    }
}