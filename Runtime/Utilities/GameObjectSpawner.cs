using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeardKit
{
    [CreateAssetMenu(fileName = "GameObjectSpawner", menuName = "BeardKit/Utilities/GameObjectSpawner")]
    public class GameObjectSpawner : NonPersistentScriptableObject
    {
        [field: SerializeField] public bool SpawnToSceneRoot { get; } = false;

        private GameObjectSpawnerRoot m_target;

        public void RegisterGameObjectSpawnerRoot(GameObjectSpawnerRoot root)
        {
            Assert.IsNull(m_target, "A root object is already registered");
            m_target = root;
        }

        public void UnregisterGameObjectSpawnerRoot(GameObjectSpawnerRoot root)
        {
            if (ReferenceEquals(root, m_target))
            {
                m_target = null;
            }
        }

        public ObjectType InstantiateObject<ObjectType>(ObjectType original, Vector3 position = default, Quaternion rotation = default)
            where ObjectType : Object
        {
            if (m_target == null)
            {
                DebugLogger.LogWarning("No target has been setup for instantiation");
                return Instantiate(original, position, rotation);
            }

            if (SpawnToSceneRoot)
            {
                Scene currentActiveScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(m_target.gameObject.scene);

                ObjectType go = Instantiate(original, position, rotation);
                SceneManager.SetActiveScene(currentActiveScene);

                return go;
            }

            return Instantiate(original, position, rotation, m_target.transform);
        }

        public GameObject NewGameObject()
        {
            if (m_target == null)
            {
                DebugLogger.LogWarning("No target has been setup for instantiation");
                return new GameObject();
            }

            if (SpawnToSceneRoot)
            {
                Scene currentActiveScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(m_target.gameObject.scene);

                var go = new GameObject();
                SceneManager.SetActiveScene(currentActiveScene);

                return go;
            }
            else
            {
                var go = new GameObject();
                go.transform.parent = m_target.transform;
                return go;
            }
        }

        public GameObject NewGameObject(string name)
        {
            GameObject newGameObject = NewGameObject();
            newGameObject.name = name;

            return newGameObject;
        }
    }
}