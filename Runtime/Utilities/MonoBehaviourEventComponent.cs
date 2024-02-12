using UnityEngine;

namespace BeardKit
{
    public class MonoBehaviourEventComponent : MonoBehaviour
    {
        [SerializeField] ScriptableObject[] m_awakeListeners;
        [SerializeField] ScriptableObject[] m_updateListeners;
        [SerializeField] ScriptableObject[] m_destroyListeners;

        private void OnValidate()
        {
            if (m_awakeListeners != null)
            {
                foreach (var so in m_awakeListeners)
                {
                    Assert.IsTrue(so is IBehaviourAwakeListener);
                }
            }

            if (m_updateListeners != null)
            {
                foreach (var so in m_updateListeners)
                {
                    Assert.IsTrue(so is IBehaviourUpdateListener);
                }
            }

            if (m_destroyListeners != null)
            {
                foreach (var so in m_destroyListeners)
                {
                    Assert.IsTrue(so is IBehaviourDestroyListener);
                }
            }
        }

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
}