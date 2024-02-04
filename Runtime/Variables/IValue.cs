using UnityEngine;

namespace BeardKit
{
    public abstract class IValue<T> : NonPersistentScriptableObject
    {
        [field: SerializeField] public T Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}