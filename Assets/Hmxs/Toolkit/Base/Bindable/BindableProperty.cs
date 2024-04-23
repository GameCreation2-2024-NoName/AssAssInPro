using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hmxs.Toolkit.Base.Bindable
{
    [Serializable]
    public struct BindableProperty<T> where T : IEquatable<T>
    {
        [SerializeField] [ReadOnly] private T value;

        public T Value
        {
            get => value;
            set
            {
                if (this.value.Equals(value)) return;
                this.value = value;
                onValueChanged?.Invoke(this.value);
            }
        }

        public Action<T> onValueChanged;

        public BindableProperty(T initValue = default, Action<T> valueChangedCallBack = default)
        {
            value = initValue;
            onValueChanged = valueChangedCallBack;
        }
    }
}