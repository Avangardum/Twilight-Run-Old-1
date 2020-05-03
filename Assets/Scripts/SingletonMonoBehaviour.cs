using UnityEngine;

namespace TwilightRun
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = (T)this;
            else
                throw new System.Exception($"Created more than one instance of {typeof(T)}");
        }
    }

}