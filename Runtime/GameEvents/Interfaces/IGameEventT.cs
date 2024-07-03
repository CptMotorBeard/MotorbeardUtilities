using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardKit
{
    public abstract class IGameEventT<TData> : ScriptableObject, IGameEventSignal
    {
        public bool IsDispatching => m_dispatchCount > 0;

        private readonly LinkedList<object> m_receivers = new LinkedList<object>();
        private uint m_dispatchCount;

        protected virtual void OnEnable()
        {
            this.DontDestroyOnLoad();
        }

        void IGameEventSignal.AttemptDisconnect(LinkedListNode<object> node)
        {
            if (!IsDispatching &&
                node != null)
            {
                m_receivers.Remove(node);
            }
        }

        public GameEventConnection Connect(IGameEventListenerT<TData> listener)
        {
            LinkedListNode<object> node = m_receivers.AddLast((object)null);
            var connection = new GameEventConnection(this, node, listener);
            node.Value = new WeakReference<GameEventConnection>(connection);

            return connection;
        }

        public GameEventConnection Connect(Action<TData> action)
        {
            LinkedListNode<object> node = m_receivers.AddLast((object)null);
            var connection = new GameEventConnection(this, node, action);
            node.Value = new WeakReference<GameEventConnection>(connection);

            return connection;
        }

        public virtual void Dispatch(TData data)
        {
            ++m_dispatchCount;

            LinkedListNode<object> node = m_receivers.First;
            int nodeCount = m_receivers.Count;

            while (node != null &&
                   nodeCount-- > 0)
            {
                GameEventConnection connection = GetConnectionFromNode(node);
                if (connection == null &&
                    m_dispatchCount == 1)
                {
                    LinkedListNode<object> nodeToRemove = node;
                    node = node.Next;
                    m_receivers.Remove(nodeToRemove);
                    continue;
                }

                AttemptDispatch(connection, data);
                LinkedListNode<object> oldNode = node;
                node = node.Next;

                if (connection.IsFlaggedForRemoval &&
                    m_dispatchCount == 1)
                {
                    m_receivers.Remove(oldNode);
                }
            }

            --m_dispatchCount;
        }

        public void DisconnectAll()
        {
            LinkedListNode<object> node = m_receivers.First;
            while (node != null)
            {
                LinkedListNode<object> nextNode = node.Next;
                GameEventConnection connection = GetConnectionFromNode(node);
                connection?.Disconnect();
                node = nextNode;
            }
        }

        public bool HasConnections()
        {
            LinkedListNode<object> node = m_receivers.First;
            while (node != null)
            {
                GameEventConnection connection = GetConnectionFromNode(node);
                if (!connection?.IsFlaggedForRemoval ?? false)
                {
                    return true;
                }

                node = node.Next;
            }

            return false;
        }

        private static void AttemptDispatch(GameEventConnection connection, TData data)
        {
            object callable = connection.GetCallable();

            if (callable == null)
            {
                return;
            }

            switch (callable)
            {
                case Action<TData> action:
                    action.Invoke(data);
                    break;

                case IGameEventListenerT<TData> listener:
                    listener.OnEvent(data);
                    break;

                default:
                    Assert.IsTrue(false, $"Unknown type to invoke in signal: {callable.GetType()}");
                    break;
            }
        }

        private static GameEventConnection GetConnectionFromNode(LinkedListNode<object> node)
        {
            switch (node.Value)
            {
                case WeakReference<GameEventConnection> weakRef:
                    if (weakRef.TryGetTarget(out GameEventConnection target))
                    {
                        return target;
                    }

                    return null;

                case GameEventConnection connection:
                    return connection;
                default:
                    Assert.IsTrue(false, $"Unknown type in signal receiver list: {node.Value.GetType()}");
                    return null;
            }
        }
    }
}