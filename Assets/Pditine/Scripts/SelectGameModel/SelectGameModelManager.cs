using System.Collections;
using System.Collections.Generic;
using Pditine.Scripts.Data;
using Pditine.Scripts.Data.GameModule;
using Pditine.Scripts.SelectGameModuleScene;
using PurpleFlowerCore;
using TMPro;
using UnityEngine;

namespace Pditine.SelectGameModel
{
    public class SelectGameModelManager : MonoBehaviour
    {
        [SerializeField] private GameModelImage leftImage;
        [SerializeField] private GameModelImage centreImage;
        [SerializeField] private GameModelImage rightImage;
        [SerializeField] private RectTransform leftPoint;
        [SerializeField] private RectTransform centrePoint;
        [SerializeField] private RectTransform rightPoint;
        [SerializeField]private TextMeshProUGUI gameModuleName;
        [SerializeField]private TextMeshProUGUI gameModuleIntroduction;
        [SerializeField] [Range(0, 1)] private float textFadeSpeed;
        
        private int _gameModuleIndex;
        private GameModelBase _currentGameModel;
        private List<GameModelBase> _gameModels;

        
        private void Start()
        {
            _gameModels = DataManager.Instance.GameModules;
            _currentGameModel = _gameModels[0];
            Init();
        }

        private void Init()
        {
            leftImage.target = leftPoint;
            centreImage.target = centrePoint;
            rightImage.target = rightPoint;
            _currentGameModel = _gameModels[_gameModuleIndex];
            centreImage.Sprite = _currentGameModel.Preview;
            ChangeText();
        }
        
        public void TurnLeft()
        {
            _gameModuleIndex--;
            if (_gameModuleIndex < 0) _gameModuleIndex = _gameModels.Count - 1;
            rightImage.Enable = false;
            leftImage.Enable = true;
            (leftImage, centreImage, rightImage) = (rightImage, leftImage, centreImage);
            Init();
        }
        
        public void TurnRight()
        {
            _gameModuleIndex++;
            if (_gameModuleIndex > _gameModels.Count - 1) _gameModuleIndex = 0;
            rightImage.Enable = true;
            leftImage.Enable = false;
            (leftImage, centreImage, rightImage) = (centreImage, rightImage, leftImage);
            Init();
        }

        public void StartGame()
        {
            DataManager.Instance.PassingData.currentGameModel = _currentGameModel;
            SceneSystem.LoadScene(2);
        }

        private Coroutine _changeText;
        private void ChangeText()
        {
            if(_changeText is not null)
                StopCoroutine(_changeText);
            _changeText = StartCoroutine(DoChangeText());
        }
        
        private IEnumerator DoChangeText()
        {
            while (gameModuleName.color.a>0.05f)
            {
                gameModuleName.color = Color.Lerp(gameModuleName.color,new Color(1,1,1,0),textFadeSpeed);
                gameModuleIntroduction.color = gameModuleName.color;
                yield return new WaitForSeconds(0.01f);
            }
            gameModuleName.text = _currentGameModel.ModuleName;
            gameModuleIntroduction.text = _currentGameModel.Introduction;
            while (gameModuleName.color.a<0.95f)
            {
                gameModuleName.color = Color.Lerp(gameModuleName.color,new Color(1,1,1,1),textFadeSpeed);
                gameModuleIntroduction.color = gameModuleName.color;
                yield return new WaitForSeconds(0.01f);
            }
        }
        
    }
}