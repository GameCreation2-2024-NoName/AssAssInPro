using System;
using UnityEngine.Events;

namespace Hmxs.Toolkit.Base.Bindable
{
    [Serializable]
    public struct BindableProperty<T> where T : IEquatable<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if(_value.Equals(value)) return;
                _value = value;
                onValueChanged?.Invoke(_value);
            }
        }

        public UnityEvent<T> onValueChanged;

        public BindableProperty(T initValue = default, UnityEvent<T> valueChangedCallBack = default)
        {
            _value = initValue;
            onValueChanged = valueChangedCallBack;
        }
    }
}