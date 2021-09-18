using System;
using UnityEngine;

namespace MotorbeardUtilities
{
    public abstract class IGameEvent : ScriptableObject
    {
        public abstract BTL.SignalST Signal { get; }
        public abstract bool HasConnections { get; }

        public abstract void Emit();

        public BTL.ConnectionST Connect(IGameEventListener listener) => Signal.Connect(listener);
        public BTL.ConnectionST Connect(Action action) => Signal.Connect(action);
    }
}