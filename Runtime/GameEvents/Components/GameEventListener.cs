using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BeardKit
{
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        [SerializeField] private List<IGameEvent> m_events = new List<IGameEvent>();
        [SerializeField] private UnityEvent m_response;
        public bool IsRegistered => m_connections.Count > 0;

        private readonly List<GameEventConnection> m_connections = new List<GameEventConnection>();

        private void OnEnable()
        {
            ConnectToEvent();
        }

        private void OnDisable()
        {
            DisconnectFromEvent();
        }

        public void OnEvent()
        {
            m_response?.Invoke();
        }

        public void ConnectToEvent()
        {
            DisconnectFromEvent();

            if (m_events.Count > 0)
            {
                m_connections.Capacity = m_events.Count;

                for (var i = 0; i < m_events.Count; ++i)
                {
                    IGameEvent evt = m_events[i];
                    if (evt != null)
                    {
                        m_connections.Add(m_events[i].Connect(this));
                    }
                    else
                    {
                        DebugLogger.LogError($"GameEventListener on {gameObject.name} has a null event. Please remove it");
                    }
                }
            }
            else
            {
                DebugLogger.LogWarning($"GameEventListener on {gameObject.name} has no events to register");
            }
        }

        public void DisconnectFromEvent()
        {
            for (var i = 0; i < m_connections.Count; ++i)
            {
                m_connections[i].Disconnect();
            }

            m_connections.Clear();
        }
    }
}