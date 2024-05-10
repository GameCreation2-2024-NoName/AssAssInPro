using System.Collections.Generic;
using Pditine.Data.GameLevel;
using UnityEngine;

namespace Pditine.Data.GameModule
{
    [CreateAssetMenu(fileName = "GameModelData",menuName = "AssAssIn/GameModelData")]
    public class GameModelBase : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string moduleName;
        [SerializeField] private string introduction;
        [SerializeField] private int sceneID; //todo: 临时
        [SerializeField] private Sprite preview;
        [SerializeField] private List<GameLevelBase> gameLevels = new(); 
        [SerializeField] private bool done;
        public int ID => id;
        public string ModuleName=>moduleName;
        public string Introduction=>introduction;
        public int SceneID=>sceneID;
        public Sprite Preview=>preview;
        public List<GameLevelBase> GameLevels => gameLevels;
        public bool Done=>done;

    }
}
