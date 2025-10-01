using UnityEngine;

namespace Assets._04.Core
{
    public class Singleton<T> where T : new()
    {
        private T instance;

        public T Instance
        {
            get
            {
                if (instance == null)
                    instance = new();
                return instance;
            }
        }
    }

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private T instance;

        public T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if(instance == null)
                        return null;
                }
                return instance;
            }
        }
    }
}
