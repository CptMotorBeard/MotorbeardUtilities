using UnityEngine;

namespace MotorbeardUtilities
{
    public class GameEventOneParam<T> : IGameEventOneParam<T>
    {
        [SerializeField] private bool m_logEventsToConsole = true;

        public override void Dispatch(T data)
        {
            if (m_logEventsToConsole)
            {
                DebugLogger.Log($"Event {name} dispatching");
            }

            base.Dispatch(data);
        }
    }
}