using System.Collections.Generic;
using UnityEngine;

namespace PurpleFlowerCore.Pool
{
    public class GameObjectPoolData
    {
        private readonly Transform _poolRoot;
        private readonly GameObject _originalGameObject;
        private readonly Queue<GameObject> _gameObjects = new();
        private int _maxCount;
        private bool _infinitePop;

        public GameObjectPoolData(Transform poolRoot,GameObject originalGameObject)
        {
            _poolRoot = poolRoot;
            _originalGameObject = originalGameObject;
            Init();
        }

        public void Init(int maxCount = -1,bool infinitePop = true,bool fillWhenInit = false)
        {
            _maxCount = maxCount;
            _infinitePop = infinitePop;
            _gameObjects.Clear();
            if (fillWhenInit)
            {
                if (maxCount <= 0) return;
                for (int i = 0; i < maxCount; i++)
                {
                    GameObject theGameObject = Object.Instantiate(_originalGameObject, _poolRoot, true);
                    theGameObject.name = _originalGameObject.name;
                    theGameObject.SetActive(false);
                    _gameObjects.Enqueue(theGameObject);
                }
            }
        }

        public GameObject Pop()
        {
            GameObject theGameObject;
            if (_gameObjects.Count<=0)
            {
                if (!_infinitePop) return null;
                theGameObject = Object.Instantiate(_originalGameObject);
                theGameObject.name = _originalGameObject.name;
                return theGameObject;
            };
            theGameObject = _gameObjects.Dequeue();
            theGameObject.transform.parent = null;
            theGameObject.SetActive(true);
            return theGameObject;
        }
        
        public void Push(GameObject theGameObject)
        {
            if (_gameObjects.Count >= _maxCount && _maxCount>0) return;
            theGameObject.SetActive(false);
            theGameObject.transform.parent = _poolRoot;
            _gameObjects.Enqueue(theGameObject);
        }
    }
}