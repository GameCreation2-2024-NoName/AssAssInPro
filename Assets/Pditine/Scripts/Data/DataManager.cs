using System.Collections.Generic;
using Pditine.Scripts.Data.Ass;
using Pditine.Scripts.Data.GameModule;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Scripts.Data
{
    public class DataManager : DdolSingletonMono<DataManager>
    {
        [SerializeField] private List<AssDataBase> asses = new();
        [SerializeField] private List<ThornDataBase> thorns = new();
        [SerializeField] private List<GameModuleBase> gameModules = new();
        public List<AssDataBase> Asses => asses;
        public List<ThornDataBase> Thorns => thorns;
        public List<GameModuleBase> GameModules => gameModules;

        public AssDataBase GetAssData(int assID)
        {
            return asses[assID];
        }
        public ThornDataBase GetThornData(int thornID)
        {
            return thorns[thornID];
        }

        public GameModuleBase GetGameModule(int gameModuleID)
        {
            if (gameModuleID >= gameModules.Count) return null;
            return gameModules[gameModuleID];
        }
    }
}