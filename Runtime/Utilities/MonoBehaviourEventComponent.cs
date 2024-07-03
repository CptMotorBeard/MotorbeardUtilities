using UnityEngine;

namespace BeardKit
{
    public class MonoBehaviourEventComponent : MonoBehaviour
    {
        [SerializeField] private ScriptableObject[] m_awakeListeners;
        [SerializeField] private ScriptableObject[] m_updateListeners;
        [SerializeField] private ScriptableObject[] m_destroyListeners;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            foreach (ScriptableObject so in m_awakeListeners)
            {
                if (so is IBehaviourAwakeListener listener)
                {
                    listener.Awake();
                }
            }
        }

        private void Update()
        {
            foreach (ScriptableObject so in m_updateListeners)
            {
                if (so is IBehaviourUpdateListener listener)
                {
                    listener.Update();
                }
            }
        }

        private void OnDestroy()
        {
            foreach (ScriptableObject so in m_destroyListeners)
            {
                if (so is IBehaviourDestroyListener listener)
                {
                    listener.OnDestroy();
                }
            }
        }

        private void OnValidate()
        {
            if (m_awakeListeners != null)
            {
                foreach (ScriptableObject so in m_awakeListeners)
                {
                    Assert.IsTrue(so is IBehaviourAwakeListener);
                }
            }

            if (m_updateListeners != null)
            {
                foreach (ScriptableObject so in m_updateListeners)
                {
                    Assert.IsTrue(so is IBehaviourUpdateListener);
                }
            }

            if (m_destroyListeners != null)
            {
                foreach (ScriptableObject so in m_destroyListeners)
                {
                    Assert.IsTrue(so is IBehaviourDestroyListener);
                }
            }
        }
    }
}