using UnityEngine;

namespace MotorbeardUtilities
{
    public abstract class IValue<T> : ScriptableObject
    {
        [field: SerializeField] public T Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}