using UnityEngine;

namespace BeardKit
{
    public class GameEventT<T> : IGameEventT<T>
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