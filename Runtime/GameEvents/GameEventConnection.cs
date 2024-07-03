using System.Collections.Generic;

namespace BeardKit
{
    public class GameEventConnection
    {
        public bool IsConnected => m_node != null;

        public bool IsFlaggedForRemoval => m_callable == null;
        private readonly object m_callable;
        private readonly LinkedListNode<object> m_node;
        private readonly IGameEventSignal m_signal;

        public GameEventConnection(IGameEventSignal signal, LinkedListNode<object> node, object callable)
        {
            m_signal = signal;
            m_node = node;
            m_callable = callable;
        }

        public object GetCallable()
        {
            return m_callable;
        }

        public void Disconnect()
        {
            m_signal?.AttemptDisconnect(m_node);
        }
    }
}