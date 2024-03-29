﻿using UnityEngine;

namespace BeardKit
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "BeardKit/Events/GameEvent")]
    public class GameEvent : IGameEvent
    {
        [SerializeField] private bool m_logEventsToConsole = true;

        public override void Dispatch()
        {
            if (m_logEventsToConsole)
            {
                DebugLogger.Log($"Event {name} dispatching");
            }

            base.Dispatch();
        }
    }
}