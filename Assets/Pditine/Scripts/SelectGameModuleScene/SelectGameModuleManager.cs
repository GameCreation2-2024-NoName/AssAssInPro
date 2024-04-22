using System.Collections.Generic;
using Pditine.Scripts.Data;
using Pditine.Scripts.Data.GameModule;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pditine.Scripts.SelectGameModuleScene
{
    public class SelectGameModuleManager : MonoBehaviour
    {
        [SerializeField] private GameModuleImage leftImage;
        [SerializeField] private GameModuleImage centreImage;
        [SerializeField] private GameModuleImage rightImage;
        [SerializeField] private RectTransform leftPoint;
        [SerializeField] private RectTransform centrePoint;
        [SerializeField] private RectTransform rightPoint;
        private int gameModuleIndex;
        private GameModuleBase currentGameModule;
        private List<GameModuleBase> _gameModules;

        [SerializeField]private TextMeshProUGUI gameModuleName;
        [SerializeField]private TextMeshProUGUI gameModuleIntroduction;
        
        private void Start()
        {
            _gameModules = DataManager.Instance.GameModules;
            currentGameModule = _gameModules[0];
            Init();
        }

        private void Init()
        {
            leftImage.target = leftPoint;
            centreImage.target = centrePoint;
            rightImage.target = rightPoint;
            currentGameModule = _gameModules[gameModuleIndex];
            centreImage.Sprite = currentGameModule.Preview;
            gameModuleName.text = currentGameModule.ModuleName;
            gameModuleIntroduction.text = currentGameModule.Introduction;
        }
        
        public void TurnLeft()
        {
            gameModuleIndex--;
            if (gameModuleIndex < 0) gameModuleIndex = _gameModules.Count - 1;
            rightImage.Enable = false;
            leftImage.Enable = true;
            (leftImage, centreImage, rightImage) = (rightImage, leftImage, centreImage);
            Init();
        }
        
        public void TurnRight()
        {
            gameModuleIndex++;
            if (gameModuleIndex > _gameModules.Count - 1) gameModuleIndex = 0;
            rightImage.Enable = true;
            leftImage.Enable = false;
            (leftImage, centreImage, rightImage) = (centreImage, rightImage, leftImage);
            Init();
        }

        public void StartGame()
        {
            SceneManager.LoadScene(currentGameModule.SceneID);
        }
    }
}