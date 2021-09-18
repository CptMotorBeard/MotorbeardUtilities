using UnityEngine;
using UnityEngine.SceneManagement;

namespace MotorbeardUtilities
{
    [CreateAssetMenu(fileName = "GameObjectSpawner", menuName = "MotorbeardUtilities/Utilities/GameObjectSpawner")]
    public class GameObjectSpawner : ScriptableObject
    {
        [field: SerializeField] public bool SpawnToSceneRoot { get; set; } = false;

        private GameObjectSpawnerRoot m_target = null;

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

        public ObjectType InstantiateObject<ObjectType>(ObjectType original, Vector3 position = default, Quaternion rotation = default) where ObjectType : Object
        {
            if (m_target == null)
            {
                DebugLogger.LogWarning("No target has been setup for instantiation");
                return UnityEngine.Object.Instantiate(original, position, rotation);
            }

            if (SpawnToSceneRoot)
            {
                Scene currentActiveScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(m_target.gameObject.scene);

                ObjectType go = UnityEngine.Object.Instantiate(original, position, rotation);
                SceneManager.SetActiveScene(currentActiveScene);

                return go;
            }
            else
            {
                return UnityEngine.Object.Instantiate(original, position, rotation, m_target.transform);
            }
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

                GameObject go = new GameObject();
                SceneManager.SetActiveScene(currentActiveScene);

                return go;
            }
            else
            {
                GameObject go = new GameObject();
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