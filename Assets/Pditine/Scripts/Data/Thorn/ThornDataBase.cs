using UnityEngine;

namespace Pditine.Scripts.Data.Ass
{
    [CreateAssetMenu(fileName = "ThornData",menuName = "AssAssIn/Thorn")]
    public class ThornDataBase : ScriptableObject
    {
        [SerializeField] private int id;
        public int ID => id;
        
        [SerializeField] private float atk;
        public float ATK => atk;
        
        [SerializeField] private float cd;
        public float CD => cd;
        
        [SerializeField] private float friction;
        public float Friction=>friction;

        [SerializeField] private GameObject prototype;
        public GameObject Prototype=>prototype;
    }
}