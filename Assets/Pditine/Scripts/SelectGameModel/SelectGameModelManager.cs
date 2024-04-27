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
        private int _gameModuleIndex;
        private GameModelBase _currentGameModel;
        private List<GameModelBase> _gameModels;

        [SerializeField]private TextMeshProUGUI gameModuleName;
        [SerializeField]private TextMeshProUGUI gameModuleIntroduction;
        
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
            gameModuleName.text = _currentGameModel.ModuleName;
            gameModuleIntroduction.text = _currentGameModel.Introduction;
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
    }
}