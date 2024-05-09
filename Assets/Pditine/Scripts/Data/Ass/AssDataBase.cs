using UnityEngine;

namespace Pditine.Data.Ass
{
    [CreateAssetMenu(fileName = "AssData",menuName = "AssAssIn/Ass")]
    public class AssDataBase : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private int hp;
        [SerializeField] private float initialVelocity;
        [SerializeField] private float weight;
        [SerializeField] private GameObject prototype;
        [SerializeField] private Sprite portrait;
        [SerializeField] private string assName;
        [SerializeField] private string assIntroduction;
        
        public int ID => id;
        public int HP => hp;
        public float InitialVelocity => initialVelocity;
        public float Weight=>weight;
        public GameObject Prototype=>prototype;
        public Sprite Portrait => portrait;
        public string AssName => assName;
        public string AssIntroduction => assIntroduction;
    }
}