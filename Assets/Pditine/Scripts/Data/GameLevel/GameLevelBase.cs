using UnityEngine;

namespace Pditine.Data.GameLevel
{
    [CreateAssetMenu(fileName = "GameLevel",menuName = "AssAssIn/GameLevel")]
    public class GameLevelBase :  ScriptableObject
    {
        [SerializeField] private int sceneIndex;
        [SerializeField] private string levelName;
        [SerializeField] private Sprite preview;

        public int SceneIndex => sceneIndex;
        public string LevelName => levelName;
        public Sprite Preview => preview;
    }
}