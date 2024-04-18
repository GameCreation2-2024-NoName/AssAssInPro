using System;
using System.Collections.Generic;
using UnityEngine;

//using Unity.VisualScripting;

namespace PurpleFlowerCore.Pool
{
    public class ObjectPoolData
    {
        //private readonly Type _type;
        private readonly Queue<object> _objects = new();
        private int _maxCount;
        //private bool _infinitePop;
        // public ObjectPoolData(Type type)
        // {
        //     _type = type;
        // }

        //public void Init(int maxCount = -1,bool infinitePop = true,bool fillWhenInit = true)
        public void Init(int maxCount = -1)
        {
            _maxCount = maxCount;
            Debug.Log(_maxCount);
            //_infinitePop = infinitePop;
            // if (fillWhenInit)
            // {
            //     if (maxCount <= 0) return;
            //     for (int i = 0; i < maxCount; i++)
            //     {
            //         _objects.Enqueue(_type.Instantiate());
            //     }
            // }
        }

        public object Pop()
        {
            object theObject;
            if (_objects.Count<=0)
            {
                // if (!_infinitePop) return null;
                // theObject = _type.Instantiate();
                // return theObject;
                return null;
            }
            theObject = _objects.Dequeue();
            return theObject;
        }
        
        public void Push(object theObject)
        {
            if (_objects.Count >= _maxCount && _maxCount>0) return;
            _objects.Enqueue(theObject);
        }
    }
}