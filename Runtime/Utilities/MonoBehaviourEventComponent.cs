using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using Sirenix.Serialization;
#endif

namespace BeardKit
{
#if ODIN_INSPECTOR
    public class MonoBehaviourEventComponent : SerializedMonoBehaviour
    {
        [SerializeField, OdinSerialize] IBehaviourAwakeListener[] m_awakeListeners;
        [SerializeField, OdinSerialize] IBehaviourUpdateListener[] m_updateListeners;
        [SerializeField, OdinSerialize] IBehaviourDestroyListener[] m_destroyListeners;

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
#else
    public class MonoBehaviourEventComponent : MonoBehaviour
    {
        [SerializeField] ScriptableObject[] m_awakeListeners;
        [SerializeField] ScriptableObject[] m_updateListeners;
        [SerializeField] ScriptableObject[] m_destroyListeners;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            foreach (var so in m_awakeListeners)
            {
                if (so is IBehaviourAwakeListener listener)
                {
                    listener.Awake();
                }
            }
        }

        private void Update()
        {
            foreach (var so in m_updateListeners)
            {
                if (so is IBehaviourUpdateListener listener)
                {
                    listener.Update();
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var so in m_destroyListeners)
            {
                if (so is IBehaviourDestroyListener listener)
                {
                    listener.OnDestroy();
                }
            }
        }
    }
#endif
}