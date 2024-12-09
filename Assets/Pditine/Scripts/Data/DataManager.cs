using System.Collections.Generic;
using System.Linq;
using Pditine.Data.Ass;
using Pditine.Data.GameModule;
using Pditine.Data.Other;
using Pditine.Data.Player;
using Pditine.Data.Thorn;
using Pditine.GamePlay.Buff;
using Pditine.Scripts.Data.DatePassing;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Pditine.Data
{
    public class DataManager : DdolSingletonMono<DataManager>
    {
        [SerializeField] private List<AssDataBase> asses = new();
        [SerializeField] private List<ThornDataBase> thorns = new();
        [SerializeField] private List<GameModelBase> gameModules = new();
        [SerializeField] private List<GameModelBase> multiplayerGameModules = new();
        //[SerializeField] private List<BuffData> buffData = new();
        [SerializeField] private List<GameObject> lightBalls = new();
        [SerializeField] private List<BuffData> buffData;
        [SerializeField] private PassingData passingData;
        [SerializeField] private PlayerData defaultPlayerData;
        [SerializeField] private OtherGameData otherGameData;
        
        public OtherGameData OtherGameData => otherGameData;
        public PlayerData DefaultPlayerData => defaultPlayerData;

        public float MaxHP => asses.Select(ass => ass.HP).Prepend(0).Max();
        public float MaxEnergy => asses.Select(ass => ass.Energy).Prepend(0).Max();
        public float MaxAssWeight => asses.Select(ass => ass.Weight).Prepend(0).Max();
        
        public float MaxATK => thorns.Select(thorn => thorn.ATK).Prepend(0).Max();
        public float MaxSpeedCoefficient => thorns.Select(thorn => thorn.SpeedCoefficient).Prepend(0).Max();
        public float MaxThornWeight => thorns.Select(thorn => thorn.Weight).Prepend(0).Max();

        public List<AssDataBase> Asses => asses;
        public List<ThornDataBase> Thorns => thorns;
        public List<GameModelBase> GameModules => gameModules;
        public List<GameModelBase> MultiplayerGameModules => multiplayerGameModules;
        //public List<BuffData> BuffData => buffData;
        public List<GameObject> LightBalls => lightBalls;
        
        /// <summary>
        /// 用于跨场景数据传输
        /// </summary>
        public PassingData PassingData => passingData;

        public List<BuffData> BuffData => buffData;

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

        //todo:枚举或string查找
        public BuffData GetBuffData(int buffId)
        {
            return buffData.Find((b) => b.id == buffId);
        }

        // public BuffData GetBuffData(int buffDataIndex)
        // {
        //     return buffData[buffDataIndex];
        // }
        //
        // public BuffData GetBuffData(string buffDataName)
        // {
        //     return buffData.Find(theBuff => theBuff.buffName == buffDataName);
        // }
    }
}