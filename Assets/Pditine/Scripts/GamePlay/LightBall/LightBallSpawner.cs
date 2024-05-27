using System;
using System.Collections.Generic;
using Pditine.Data;
using PurpleFlowerCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pditine.GamePlay.LightBall
{
    public class LightBallSpawner : MonoBehaviour
    {
        [SerializeField] private Transform leftUpPoint;
        [SerializeField] private Transform rightDownPoint;
        
        [SerializeField] private List<GameObject> lightBalls = new();
        //private List<GameObject> _lightBalls;

        private float _cd;
        [SerializeField] private float minCD;
        [SerializeField] private float maxCD;

        private bool _isCreating = true;

        private void Awake()
        {
            //_lightBalls = DataManager.Instance.LightBalls;
            _cd = Random.Range(minCD, maxCD);
        }

        private void OnEnable()
        {
            EventSystem.AddEventListener("Pause",StopCreateLightBall);
            EventSystem.AddEventListener("UnPause",StartCreateLightBall);
            EventSystem.AddEventListener("GameStart",StartCreateLightBall);
            EventSystem.AddEventListener("GameOver",StopCreateLightBall);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("Pause",StopCreateLightBall);
            EventSystem.RemoveEventListener("UnPause",StartCreateLightBall);
            EventSystem.RemoveEventListener("GameStart",StartCreateLightBall);
            EventSystem.RemoveEventListener("GameOver",StopCreateLightBall);
        }

        private void StartCreateLightBall() => _isCreating = true;
        private void StopCreateLightBall() => _isCreating = false;

        private void Update()
        {
            UpdateCD();
        }

        private void UpdateCD()
        {
            if (!_isCreating) return;
            _cd -= Time.deltaTime;
            if (!(_cd < 0)) return;
            _cd = Random.Range(minCD, maxCD);
            CreateLightBall();
        }

        private void CreateLightBall()
        {
            //todo:对象池
            //var theBall = PoolSystem.GetGameObject(lightBalls[Random.Range(0, lightBalls.Count)]).GetComponent<LightBall>();
            float posX = Random.Range(leftUpPoint.transform.position.x, rightDownPoint.transform.position.x);
            float posY = Random.Range(rightDownPoint.transform.position.y, leftUpPoint.transform.position.y);

            var overlaps = Physics2D.OverlapPointAll(new( posX, posY));
            foreach (var overlay in overlaps)
            {
                if (overlay.CompareTag("LightBall")) return;
            }
            
            var theBall = Instantiate(lightBalls[Random.Range(0, lightBalls.Count)].transform).GetComponent<LightBall>();
            theBall.transform.position = new Vector3(posX, posY, 0);
            theBall.Init();
        }
    }
}