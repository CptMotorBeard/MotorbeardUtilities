namespace BeardKit
{
    public interface IGameEventListenerT<T>
    {
        void OnEvent(T param);
    }
}