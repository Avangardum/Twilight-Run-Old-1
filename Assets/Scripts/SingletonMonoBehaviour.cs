using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = (T)this;
        else
            throw new System.Exception($"Created more than one instance of {typeof(T)}");
    }
}
