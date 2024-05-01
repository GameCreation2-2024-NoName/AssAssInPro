﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pditine.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private List<CanvasGroup> menus = new();
        private int _lastMenuIndex;
        private int _currentMenuIndex;

        public void ChangeMenu(int index)
        {
            _lastMenuIndex = _currentMenuIndex;
            _currentMenuIndex = index;
            StartCoroutine(DoDisableMenu(menus[_lastMenuIndex], menus[_currentMenuIndex]));
        }
        
        //临时
        private IEnumerator DoDisableMenu(CanvasGroup lastMenu,CanvasGroup currentMenu)
        {
            while (lastMenu.alpha > 0.02f)
            {
                lastMenu.alpha = Mathf.Lerp(lastMenu.alpha, 0, 0.05f);
                yield return new WaitForSeconds(0.01f);
            }
            lastMenu.alpha = 0;
            lastMenu.gameObject.SetActive(false);

            currentMenu.gameObject.SetActive(true);
            currentMenu.alpha = 0;
            while (currentMenu.alpha<0.95f)
            {
                currentMenu.alpha = Mathf.Lerp(currentMenu.alpha, 1, 0.05f);
                yield return new WaitForSeconds(0.01f);
            }
            currentMenu.alpha =1;
        }
    }
}