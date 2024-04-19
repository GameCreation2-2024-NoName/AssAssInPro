using System.Collections.Generic;
using Pditine.Scripts.Data.Ass;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Scripts.Data
{
    public class DataManager : DdolSingletonMono<DataManager>
    {
        [SerializeField] private List<AssDataBase> asses = new();
        [SerializeField] private List<ThornDataBase> thorns = new();

        public AssDataBase GetAssData(int assID)
        {
            //return asses.Find(a=>a.ID == assID);
            return asses[assID];
        }
        public ThornDataBase GetThornData(int thornID)
        {
            return thorns[thornID];
        }
    }
}