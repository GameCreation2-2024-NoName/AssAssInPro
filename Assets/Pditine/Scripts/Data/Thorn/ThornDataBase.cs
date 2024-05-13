using UnityEngine;
using UnityEngine.Serialization;

namespace Pditine.Scripts.Data.Ass
{
    [CreateAssetMenu(fileName = "ThornData",menuName = "AssAssIn/Thorn")]
    public class ThornDataBase : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private int atk;
        [SerializeField] private float cd;
        [SerializeField] private float weight;
        [SerializeField] private GameObject prototype;
        [SerializeField] private Sprite portrait;
        [SerializeField] private string thornName;
        [SerializeField] private string thornIntroduction;

        public int ID => id;
        public int ATK => atk;
        public float CD => cd;
        public float Weight=>weight;
        public GameObject Prototype=>prototype;
        public Sprite Portrait => portrait;
        public string ThornName => thornName;
        public string ThornIntroduction => thornIntroduction;
    }
}