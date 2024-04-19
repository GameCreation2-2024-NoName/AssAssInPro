using UnityEngine;

namespace Pditine.Scripts.Data.DatePassing
{
    [CreateAssetMenu(fileName = "PassingData",menuName = "AssAssIn/PassingData")]
    //用于跨场景传递数据
    public class PassingData : ScriptableObject
    {
        public int assID;
        public int thornID;
    }
}