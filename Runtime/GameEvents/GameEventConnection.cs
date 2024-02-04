using System;
using System.Collections.Generic;

namespace BeardKit
{
    public class GameEventConnection
    {
        private IGameEventSignal m_signal;
        private LinkedListNode<object> m_node;
        private object m_callable;

        public bool IsConnected => m_node != null;
        public bool IsFlaggedForRemoval => m_callable == null;

        public object GetCallable() => m_callable;

        public GameEventConnection(IGameEventSignal signal, LinkedListNode<object> node, object callable)
        {
            m_signal = signal;
            m_node = node;
            m_callable = callable;
        }

        public void Disconnect()
        {
            m_signal?.AttemptDisconnect(m_node);
        }
    }
}