using UnityEngine;

namespace Pditine.Scripts.Data.Ass
{
    [CreateAssetMenu(fileName = "AssData",menuName = "AssAssIn/Ass")]
    public class AssDataBase : ScriptableObject
    {
        [SerializeField] private int id;
        public int ID => id;
        
        [SerializeField] private float hp;
        public float HP => hp;
        
        [SerializeField] private float initialVelocity;
        public float InitialVelocity => initialVelocity;
        
        [SerializeField] private float friction;
        public float Friction=>friction;

        [SerializeField] private GameObject prototype;
        public GameObject Prototype=>prototype;

        [SerializeField] private Sprite portrait;
        public Sprite Portrait => portrait;
    }
}