﻿using Pditine.Scripts.Data.GameModule;
using UnityEngine;

namespace Pditine.Scripts.Data.DatePassing
{
    [CreateAssetMenu(fileName = "PassingData",menuName = "AssAssIn/PassingData")]
    //用于跨场景传递数据
    public class PassingData : ScriptableObject
    {
        public int player1AssID;
        public int player2AssID;
        public int player1ThornID;
        public int player2ThornID;
        public GameModelBase currentGameModel;
        public int mainMenuOpenedMenuIndex;
    }
}