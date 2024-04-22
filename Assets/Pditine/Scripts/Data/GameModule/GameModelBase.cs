using UnityEngine;

namespace Pditine.Scripts.Data.GameModule
{
    [CreateAssetMenu(fileName = "GameModelData",menuName = "AssAssIn/GameModelData")]
    public class GameModelBase : ScriptableObject
    {
        [SerializeField] private int id;
        public int ID => id;
        
        [SerializeField] private string moduleName;
        public string ModuleName=>moduleName;
        
        [SerializeField] private string introduction;
        public string Introduction=>introduction;
        
        [SerializeField] private int sceneID;
        public int SceneID=>sceneID;
        
        [SerializeField] private Sprite preview;
        public Sprite Preview=>preview;

    }
}
