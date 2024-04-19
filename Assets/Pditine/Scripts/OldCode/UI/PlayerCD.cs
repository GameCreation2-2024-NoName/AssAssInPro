using System;
using UnityEngine;
using UnityEngine.UI;

namespace LJH.Scripts.UI
{
    public class PlayerCD : MonoBehaviour
    {
        [SerializeField] private int id;
        public int ID => id;
        private Image _cdUI;

        private void Start()
        {
            _cdUI = GetComponent<Image>();
        }


        public void UpdateCD(float fillCD)
        {
            _cdUI.fillAmount = fillCD;
        }
    }
}