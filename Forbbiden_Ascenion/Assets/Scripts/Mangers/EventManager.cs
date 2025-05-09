using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Event Manager");
                go.AddComponent<EventManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
    }

    public event Action<int> OnCollectCollectable;

    public void InvokeCollectCollectable(int objectID) => OnCollectCollectable?.Invoke(objectID);

    public event Action OnPlayerDead;

    public void InvokePlayerDead() => OnPlayerDead?.Invoke();
}
