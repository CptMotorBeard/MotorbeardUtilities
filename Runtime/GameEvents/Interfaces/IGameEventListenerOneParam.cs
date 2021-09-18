namespace MotorbeardUtilities
{
    public interface IGameEventListenerOneParam<T>
    {
        void OnEvent(T param);
    }
}