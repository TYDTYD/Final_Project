using UnityEngine;
using System;
public class UpdateManager : MonoBehaviour
{
    public static UpdateManager Instance { get; private set; }
    Action UpdateMethod;
    Action FixedUpdateMethod;
    Action LateUpdateMethod;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        UpdateMethod?.Invoke();
    }

    void FixedUpdate()
    {
        FixedUpdateMethod?.Invoke();
    }

    private void LateUpdate()
    {
        LateUpdateMethod?.Invoke();
    }

    void SubscribeUpdate(Action method) => UpdateMethod += method;
    void UnSubscribeUpdate(Action method) => UpdateMethod -= method;
    void SubscribeFixedUpdate(Action method) => FixedUpdateMethod += method;
    void UnSubscribeFixedUpdate(Action method) => FixedUpdateMethod -= method;
    void SubscribeLateUpdate(Action method) => LateUpdateMethod += method;
    void UnSubscribeLateUpdate(Action method) => LateUpdateMethod -= method;
}
