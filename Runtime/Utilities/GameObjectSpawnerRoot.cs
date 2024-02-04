using UnityEngine;

namespace BeardKit
{
    public class GameObjectSpawnerRoot : MonoBehaviour
    {
        [SerializeField] private GameObjectSpawner m_spawner;

        private void OnEnable()
        {
            if (m_spawner)
            {
                m_spawner.RegisterGameObjectSpawnerRoot(this);
            }
        }

        private void OnDisable()
        {
            if (m_spawner)
            {
                m_spawner.UnregisterGameObjectSpawnerRoot(this);
            }
        }
    }
}