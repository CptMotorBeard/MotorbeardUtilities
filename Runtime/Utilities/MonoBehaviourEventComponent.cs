using UnityEngine;

namespace BeardKit
{
    public class MonoBehaviourEventComponent : MonoBehaviour
    {
        [SerializeField] IBehaviourAwakeListener[] m_awakeListeners;
        [SerializeField] IBehaviourUpdateListener[] m_updateListeners;
        [SerializeField] IBehaviourDestroyListener[] m_destroyListeners;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            foreach (var listener in m_awakeListeners)
            {
                listener.Awake();
            }
        }

        private void Update()
        {
            foreach (var listener in m_updateListeners)
            {
                listener.Update();
            }
        }

        private void OnDestroy()
        {
            foreach (var listener in m_destroyListeners)
            {
                listener.OnDestroy();
            }
        }
    }
}