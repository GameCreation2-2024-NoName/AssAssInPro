// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_14
// -------------------------------------------------

using System.Collections.Generic;
using Pditine.Data.Ass;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Scripts.Data.Ass;
using UnityEngine;

namespace Pditine.Tool
{
    public class DataController : MonoBehaviour
    {
        // [SerializeField] private List<ThornDataBase> 刺 = new();
        // [SerializeField] private List<AssDataBase> Ass = new();
        [SerializeField] private List<ScriptableObject> configData = new();
        public List<ScriptableObject> ConfigData => configData;
    }
}