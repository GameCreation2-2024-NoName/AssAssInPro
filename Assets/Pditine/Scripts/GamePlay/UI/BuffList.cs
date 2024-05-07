using System.Collections.Generic;
using Pditine.GamePlay.Buff;
using UnityEngine;

namespace Pditine.GamePlay.UI
{
    public class BuffList : MonoBehaviour
    {
        [SerializeField] private BuffIcon buffPrototype;
        private readonly List<BuffIcon> _buffIcons = new();
        public void AddBuff(BuffInfo buffInfo)
        {
            //todo:对象池
            var theIcon = Instantiate(buffPrototype.gameObject, transform).GetComponent<BuffIcon>();
            theIcon.transform.localScale = transform.localScale;
            theIcon.Init(buffInfo);
            _buffIcons.Add(theIcon);
        }

        public void RemoveBuff(BuffInfo buffInfo)
        {
            for (int i = 0; i < _buffIcons.Count; i++)
            {
                if (_buffIcons[i].BuffInfo == buffInfo)
                {
                    var theIcon = _buffIcons[i];
                    _buffIcons.RemoveAt(i);
                    Destroy(theIcon.gameObject);//对象池
                }
            }
        }
    }
}