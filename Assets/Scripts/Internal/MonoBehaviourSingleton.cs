using UnityEngine;

namespace Internal
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) 
                {
                    var objs = FindObjectsOfType (typeof(T)) as T[];
                
                    if (objs.Length > 0)
                        _instance = objs[0];
                
                    if (objs.Length > 1) {
                        Debug.LogError ("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                
                    if (_instance == null) {
                        GameObject obj = new GameObject {
                            hideFlags = HideFlags.HideAndDontSave
                        };
                        _instance = obj.AddComponent<T> ();
                    }
                }
                return _instance;
            }
        }
        
        public static void RevokeAllManagers()
        {
            MonoBehaviourSingleton<T>[] managers = FindObjectsOfType<MonoBehaviourSingleton<T>>();

            foreach (MonoBehaviourSingleton<T> manager in managers)
            {
                manager.RevokeManager();
            }
        }

        private void RevokeManager() {
            Destroy(_instance.gameObject);
            _instance = null;
        } 
    }
}
