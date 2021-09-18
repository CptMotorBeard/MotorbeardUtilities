using System;
using UnityEngine;

namespace MotorbeardUtilities
{
    public abstract class IGameEventOneParam<TData> : ScriptableObject
    {
        public abstract BTL.SignalST<TData> Signal { get; }
        public abstract bool HasConnections { get; }

        public abstract void Emit(TData arg);

        public BTL.ConnectionST Connect(IGameEventListenerOneParam<TData> listener) => Signal.Connect(listener);
        public BTL.ConnectionST Connect(Action<TData> action) => Signal.Connect(action);
    }
}