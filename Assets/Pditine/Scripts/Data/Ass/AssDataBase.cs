using UnityEngine;

namespace Pditine.Scripts.Data.Ass
{
    [CreateAssetMenu(fileName = "AssData",menuName = "AssAssIn/Ass")]
    public class AssDataBase : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private float hp;
        [SerializeField] private float initialVelocity;
        [SerializeField] private float friction;
        [SerializeField] private GameObject prototype;
        [SerializeField] private Sprite portrait;
        [SerializeField] private string assName;
        [SerializeField] private string assIntroduction;

        public int ID => id;
        public float HP => hp;
        public float InitialVelocity => initialVelocity;
        public float Friction=>friction;
        public GameObject Prototype=>prototype;
        public Sprite Portrait => portrait;
        public string AssName => assName;
        public string AssIntroduction => assIntroduction;
    }
}