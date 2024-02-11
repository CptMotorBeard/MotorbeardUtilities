namespace BeardKit
{
    public interface IBehaviourAwakeListener
    {
        public void Awake();
    }

    public interface IBehaviourUpdateListener
    {
        public void Update();
    }

    public interface IBehaviourDestroyListener
    {
        public void OnDestroy();
    }
}