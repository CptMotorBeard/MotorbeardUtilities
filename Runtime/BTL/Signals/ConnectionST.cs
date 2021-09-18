using MotorbeardUtilities;
using System;
using System.Collections.Generic;

namespace BTL
{
    /// <summary>Single threaded Connection</summary>
    public class ConnectionST
    {
        private ISignal_ST m_signal;
        private LinkedListNode<object> m_node;
        private object m_callable;

        private int m_lockCount = 0;

        public bool IsLocked => m_lockCount > 0;
        public bool IsConnected => m_node == null;
        public bool IsFlaggedForRemoval => m_callable == null;

        public object GetCallable() => m_callable;

        public void Disconnect()
        {
            m_signal?.CheckThread();
            m_signal?.AttemptDisconnect(m_node);

            m_callable = null;
            m_node = null;
            m_signal = null;
        }

        public void Detach()
        {
            if (m_node != null)
            {
                m_node.Value = this;
                m_node = null;
                m_signal = null;
            }
        }

        public void Lock()
        {
            m_signal?.CheckThread();
            ++m_lockCount;
        }

        public void Unlock()
        {
            m_signal?.CheckThread();

            if (m_lockCount <= 0)
                throw new InvalidOperationException($"Unbalanced Lock/Unlock detected. LockCount is {m_lockCount} when trying to Unlock");

            --m_lockCount;
        }

        public ConnectionST(ISignal_ST signal, LinkedListNode<object> node, object callable)
        {
            m_signal = signal ?? throw new ArgumentNullException($"{nameof(signal)} cannot be null");
            m_node = node ?? throw new ArgumentNullException($"{nameof(node)} cannot be null");
            m_callable = callable ?? throw new ArgumentNullException($"{nameof(callable)} cannot be null");
        }

        ~ConnectionST()
        {
            Assert.IsFalse(IsConnected, "GameEventConnection garbage collected but still connected!");
        }
    }
}