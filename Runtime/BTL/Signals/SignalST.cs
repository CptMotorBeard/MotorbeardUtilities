using MotorbeardUtilities;
using System;
using System.Collections.Generic;

namespace BTL
{
    /// <summary>Single threaded signal interface</summary>
    public interface ISignal_ST
    {
        bool IsDispatching { get; }
        void CheckThread();
        void AttemptDisconnect(LinkedListNode<object> node);
    }

    /// <summary>Single threaded signal</summary>
    public class SignalST : ISignal_ST
    {
        private LinkedList<object> m_receivers = new LinkedList<object>();
        private int m_dispatchCounter = 0;
        private readonly int m_owningThreadID = -1;

        public bool IsDispatching => m_dispatchCounter > 0;

        public SignalST()
        {
            m_owningThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        public void Emit()
        {
            CheckThread();
            m_dispatchCounter += 1;

            var node = m_receivers.First;
            int nodeCount = m_receivers.Count;

            while (node != null && nodeCount-- > 0)
            {
                ConnectionST connection = GetConnectionFromNode(node);
                if (connection == null && m_dispatchCounter == 1)
                {
                    var nodeToRemove = node;
                    node = node.Next;
                    m_receivers.Remove(nodeToRemove);
                    continue;
                }

                AttemptDispatch(connection);

                var oldNode = node;
                node = node.Next;

                if (connection.IsFlaggedForRemoval && m_dispatchCounter == 1)
                    m_receivers.Remove(oldNode);
            }

            m_dispatchCounter -= 1;
        }

        public ConnectionST Connect(IGameEventListener listener)
        {
            if (listener == null)
                throw new ArgumentNullException($"{nameof(listener)} cannot be null");

            CheckThread();

            var node = m_receivers.AddLast((object)null);
            ConnectionST connection = new ConnectionST(this, node, listener);
            node.Value = new WeakReference<ConnectionST>(connection);

            return connection;
        }

        public ConnectionST Connect(Action listener)
        {
            if (listener == null)
                throw new ArgumentNullException($"{nameof(listener)} cannot be null");

            CheckThread();

            var node = m_receivers.AddLast((object)null);
            ConnectionST connection = new ConnectionST(this, node, listener);
            node.Value = new WeakReference<ConnectionST>(connection);

            return connection;
        }

        public void DisconnectAll()
        {
            var node = m_receivers.First;
            while (node != null)
            {
                var nextNode = node.Next;
                ConnectionST connection = GetConnectionFromNode(node);
                connection?.Disconnect();
                node = nextNode;
            }
        }

        public bool HasConnections()
        {
            var node = m_receivers.First;
            while (node != null)
            {
                ConnectionST connection = GetConnectionFromNode(node);
                if (!connection?.IsFlaggedForRemoval ?? false)
                    return true;

                node = node.Next;
            }

            return false;
        }

        private ConnectionST GetConnectionFromNode(LinkedListNode<object> node)
        {
            switch (node.Value)
            {
                case WeakReference<ConnectionST> weakRef:
                    if (weakRef.TryGetTarget(out ConnectionST target))
                        return target;
                    return null;

                case ConnectionST connection:
                    return connection;
                default:
                    Assert.IsTrue(false, $"Unknown type in signal receiver list: {node.Value.GetType()}");
                    return null;
            }
        }

        private static void AttemptDispatch(ConnectionST connection)
        {
            object callable = connection.GetCallable();

            if (callable == null || connection.IsLocked)
                return;

            switch (callable)
            {
                case Action action:
                    action.Invoke();
                    break;

                case IGameEventListener listener:
                    listener.OnEvent();
                    break;

                default:
                    Assert.IsTrue(false, $"Unknown type to invoke in signal: {callable.GetType()}");
                    break;
            }
        }

        private void CheckThread()
        {
            ((ISignal_ST)this).CheckThread();
        }

        void ISignal_ST.CheckThread()
        {
            if (m_owningThreadID != System.Threading.Thread.CurrentThread.ManagedThreadId)
                throw new InvalidOperationException("Using Signal API on different thread than it was constructed on!");
        }

        void ISignal_ST.AttemptDisconnect(LinkedListNode<object> node)
        {
            if (!IsDispatching && node != null)
            {
                m_receivers.Remove(node);
            }
        }
    }

    /// <summary>Single threaded signal with data</summary>
    public class SignalST<TData> : ISignal_ST
    {
        private LinkedList<object> m_receivers = new LinkedList<object>();
        private int m_dispatchCounter = 0;
        private readonly int m_owningThreadID = -1;

        public bool IsDispatching => m_dispatchCounter > 0;

        public SignalST()
        {
            m_owningThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        public void Emit(TData arg)
        {
            CheckThread();
            m_dispatchCounter += 1;

            var node = m_receivers.First;
            int nodeCount = m_receivers.Count;

            while (node != null && nodeCount-- > 0)
            {
                ConnectionST connection = GetConnectionFromNode(node);
                if (connection == null && m_dispatchCounter == 1)
                {
                    var nodeToRemove = node;
                    node = node.Next;
                    m_receivers.Remove(nodeToRemove);
                    continue;
                }

                AttemptDispatch(connection, arg);

                var oldNode = node;
                node = node.Next;

                if (connection.IsFlaggedForRemoval && m_dispatchCounter == 1)
                    m_receivers.Remove(oldNode);
            }

            m_dispatchCounter -= 1;
        }

        public ConnectionST Connect(IGameEventListenerOneParam<TData> listener)
        {
            if (listener == null)
                throw new ArgumentNullException($"{nameof(listener)} cannot be null");

            CheckThread();

            var node = m_receivers.AddLast((object)null);
            ConnectionST connection = new ConnectionST(this, node, listener);
            node.Value = new WeakReference<ConnectionST>(connection);

            return connection;
        }

        public ConnectionST Connect(Action<TData> listener)
        {
            if (listener == null)
                throw new ArgumentNullException($"{nameof(listener)} cannot be null");

            CheckThread();

            var node = m_receivers.AddLast((object)null);
            ConnectionST connection = new ConnectionST(this, node, listener);
            node.Value = new WeakReference<ConnectionST>(connection);

            return connection;
        }

        public void DisconnectAll()
        {
            var node = m_receivers.First;
            while (node != null)
            {
                var nextNode = node.Next;
                ConnectionST connection = GetConnectionFromNode(node);
                connection?.Disconnect();
                node = nextNode;
            }
        }

        public bool HasConnections()
        {
            var node = m_receivers.First;
            while (node != null)
            {
                ConnectionST connection = GetConnectionFromNode(node);
                if (!connection?.IsFlaggedForRemoval ?? false)
                    return true;

                node = node.Next;
            }

            return false;
        }

        private ConnectionST GetConnectionFromNode(LinkedListNode<object> node)
        {
            switch (node.Value)
            {
                case WeakReference<ConnectionST> weakRef:
                    if (weakRef.TryGetTarget(out ConnectionST target))
                        return target;
                    return null;

                case ConnectionST connection:
                    return connection;
                default:
                    Assert.IsTrue(false, $"Unknown type in signal receiver list: {node.Value.GetType()}");
                    return null;
            }
        }

        private static void AttemptDispatch(ConnectionST connection, TData arg)
        {
            object callable = connection.GetCallable();

            if (callable == null || connection.IsLocked)
                return;

            switch (callable)
            {
                case Action<TData> action:
                    action.Invoke(arg);
                    break;

                case IGameEventListenerOneParam<TData> listener:
                    listener.OnEvent(arg);
                    break;

                default:
                    Assert.IsTrue(false, $"Unknown type to invoke in signal: {callable.GetType()}");
                    break;
            }
        }

        private void CheckThread()
        {
            ((ISignal_ST)this).CheckThread();
        }

        void ISignal_ST.CheckThread()
        {
            if (m_owningThreadID != System.Threading.Thread.CurrentThread.ManagedThreadId)
                throw new InvalidOperationException("Using Signal API on different thread than it was constructed on!");
        }

        void ISignal_ST.AttemptDisconnect(LinkedListNode<object> node)
        {
            if (!IsDispatching && node != null)
            {
                m_receivers.Remove(node);
            }
        }
    }
}