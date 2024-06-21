using System;
using System.Collections;
using System.Collections.Generic;
using Hmxs.Scripts;
using Pditine.Data;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pditine.MainMenu
{
    public class MainMenuManager : SingletonMono<MainMenuManager>
    {
        [SerializeField] private List<CanvasGroup> menus = new();
        private int _lastMenuIndex;
        private int _currentMenuIndex;
        private Coroutine _doDisableMenu;
        [SerializeField] private Material theMaterial;

        private void Start()
        {
            OpenMenu(DataManager.Instance.PassingData.mainMenuOpenedMenuIndex);
            //PlayerManager.Instance.Init();
            Time.timeScale = 1;
            PlayerManager.Instance.SwitchMap("Selection");
            PlayerManager.Instance.Reset();
            Debug.Log("reset");
        }

        public void OpenMenu(int menuIndex)
        {
            foreach (var menu in menus)
            {
                menu.alpha = 0;
                menu.gameObject.SetActive(false);
            }

            var theMenu = menus[menuIndex];
            theMenu.alpha = 1;
            _currentMenuIndex = menuIndex;
            theMenu.gameObject.SetActive(true);
        }

        public void ChangeMenu(int index)
        {
            if (index == _currentMenuIndex) return;
            if(_doDisableMenu is not null)
            {
                StopCoroutine(_doDisableMenu);
                menus[_lastMenuIndex].alpha = 0;
                menus[_lastMenuIndex].gameObject.SetActive(false);
            }
            _lastMenuIndex = _currentMenuIndex;
            _currentMenuIndex = index;
            _doDisableMenu = StartCoroutine(DoDisableMenu(menus[_lastMenuIndex], menus[_currentMenuIndex]));
        }

        public void ChangeMenu(string sceneName)
        {
            int index = -1;
            for(var i = 0;i<menus.Count;i++)
            {
                if (menus[i].gameObject.name == sceneName)
                {
                    index = i;
                    break;
                }
            }
            if(index>=0)
                ChangeMenu(index);
            else
                PFCLog.Error("场景名错误");
        }
        
        //临时
        private IEnumerator DoDisableMenu(CanvasGroup lastMenu,CanvasGroup currentMenu)
        {
            while (lastMenu.alpha > 0.02f)
            {
                lastMenu.alpha = Mathf.Lerp(lastMenu.alpha, 0, 0.05f);
                theMaterial.SetFloat("_Cutoff", (1 - lastMenu.alpha)/1.9f);
                yield return new WaitForSeconds(0.01f);
            }
            theMaterial.SetFloat("_Cutoff",1);
            lastMenu.alpha = 0;
            lastMenu.gameObject.SetActive(false);
            currentMenu.gameObject.SetActive(true);
            currentMenu.alpha = 0;
            while (currentMenu.alpha<0.95f)
            {
                currentMenu.alpha = Mathf.Lerp(currentMenu.alpha, 1, 0.05f);
                theMaterial.SetFloat("_Cutoff",(1 - currentMenu.alpha)/1.9f);
                yield return new WaitForSeconds(0.01f);
            }
            currentMenu.alpha =1;
            theMaterial.SetFloat("_Cutoff",0);
        }
    }
}