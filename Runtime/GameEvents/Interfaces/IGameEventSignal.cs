using System.Collections.Generic;

public interface IGameEventSignal
{
    void AttemptDisconnect(LinkedListNode<object> node);
}