using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MotorbeardUtilities
{
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        [SerializeField] private List<IGameEvent> m_events = new List<IGameEvent>();
        [SerializeField] private UnityEvent m_response = null;

        private List<BTL.ConnectionST> m_connections = new List<BTL.ConnectionST>();

        public bool IsRegistered => m_connections.Count > 0;

        private void OnEnable()
        {
            ConnectToEvent();
        }

        private void OnDisable()
        {
            DisconnectFromEvent();
        }

        public void ConnectToEvent()
        {
            DisconnectFromEvent();

            if (m_events.Count > 0)
            {
                m_connections.Capacity = m_events.Count;

                for (int i = 0; i < m_events.Count; ++i)
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
            for (int i = 0; i < m_connections.Count; ++i)
            {
                m_connections[i].Disconnect();
            }

            m_connections.Clear();
        }

        public void OnEvent()
        {
            m_response?.Invoke();
        }
    }
}