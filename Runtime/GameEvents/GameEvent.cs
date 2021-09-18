using UnityEngine;

namespace MotorbeardUtilities
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "MotorbeardUtilities/Events/GameEvent")]
    public class GameEvent : IGameEvent
    {
        [SerializeField] private bool m_logEventsToConsole = true;

        private BTL.SignalST m_signal = null;

        public override BTL.SignalST Signal => m_signal ??= new BTL.SignalST();
        public override bool HasConnections => m_signal.HasConnections();

        public override void Emit()
        {
            if (m_logEventsToConsole)
            {
                DebugLogger.Log($"Event {name} dispatching");
            }

            Signal.Emit();
        }
    }
}