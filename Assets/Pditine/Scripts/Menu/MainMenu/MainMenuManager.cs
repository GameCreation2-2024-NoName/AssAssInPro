using System.Collections;
using System.Collections.Generic;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.MainMenu
{
    public class MainMenuManager : SingletonMono<MainMenuManager>
    {
        [SerializeField] private List<CanvasGroup> menus = new();
        private int _lastMenuIndex;
        private int _currentMenuIndex;
        private Coroutine _doDisableMenu;
        [SerializeField] private Material theMaterial;

        public void ChangeMenu(int index)
        {
            if(_doDisableMenu is not null)StopCoroutine(_doDisableMenu);
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
                theMaterial.SetFloat("_Cutoff", (1 - lastMenu.alpha)/2.08f);
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
                theMaterial.SetFloat("_Cutoff",(1 - currentMenu.alpha)/2.08f);
                yield return new WaitForSeconds(0.01f);
            }
            currentMenu.alpha =1;
            theMaterial.SetFloat("_Cutoff",0);
        }
    }
}