using System.Collections.Generic;
using System.Linq;
using Pditine.GamePlay.Buff;
using Pditine.Scripts.Data.Ass;
using Pditine.Scripts.Data.DatePassing;
using Pditine.Scripts.Data.GameModule;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Data
{
    public class DataManager : DdolSingletonMono<DataManager>
    {
        [SerializeField] private List<AssDataBase> asses = new();
        [SerializeField] private List<ThornDataBase> thorns = new();
        [SerializeField] private List<GameModelBase> gameModules = new();
        [SerializeField] private List<BuffData> buffData = new();
        public List<AssDataBase> Asses => asses;
        public List<ThornDataBase> Thorns => thorns;
        public List<GameModelBase> GameModules => gameModules;
        public List<BuffData> BuffData => buffData;

        [SerializeField] private PassingData passingData;
        /// <summary>
        /// 用于跨场景数据传输
        /// </summary>
        public PassingData PassingData => passingData; 

        public AssDataBase GetAssData(int assID)
        {
            return asses[assID];
        }
        public ThornDataBase GetThornData(int thornID)
        {
            return thorns[thornID];
        }

        public GameModelBase GetGameModule(int gameModuleID)
        {
            if (gameModuleID >= gameModules.Count) return null;
            return gameModules[gameModuleID];
        }

        public BuffData GetBuffData(int buffDataIndex)
        {
            return buffData[buffDataIndex];
        }

        public BuffData GetBuffData(string buffDataName)
        {
            return buffData.Find(theBuff => theBuff.buffName == buffDataName);
        }
    }
}