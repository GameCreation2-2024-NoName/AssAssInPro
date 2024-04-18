using UnityEngine;

namespace PurpleFlowerCore.Base
{
    public class PFCManager : MonoBehaviour
    {
        // private readonly Dictionary<Type,> _modules = new();
        // public static Dictionary<Type, MonoBehaviour> Modules => Instance._modules;

        private static PFCManager _instance;
        public static PFCManager Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                GameObject pfcGameObject = new GameObject
                {
                    name = "PurpleFlowerCore"
                };
                _instance = pfcGameObject.AddComponent<PFCManager>();
                DontDestroyOnLoad(pfcGameObject);
                return _instance;
            }
        }
        
        //todo:add system
        // public void AddSystem()
        // {
        //     
        // }
        
        private void Awake()
        {
            if(_instance is not null)
                Destroy(_instance.gameObject);
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDisable()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}