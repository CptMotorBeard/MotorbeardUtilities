using UnityEngine;

namespace MotorbeardUtilities
{
    public class GameEventOneParam<T> : IGameEventOneParam<T>
    {
        [SerializeField] private bool m_logEventsToConsole = true;

        private BTL.SignalST<T> m_signal = null;

        public override BTL.SignalST<T> Signal => m_signal ??= new BTL.SignalST<T>();
        public override bool HasConnections => m_signal.HasConnections();

        public override void Emit(T arg)
        {
            if (m_logEventsToConsole)
            {
                DebugLogger.Log($"Event {name} dispatching with arg {arg}");
            }

            Signal.Emit(arg);
        }
    }
}