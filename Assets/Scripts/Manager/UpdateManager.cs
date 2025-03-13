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
    // 구독하고자 하는 함수를 UpdateMethod에 추가합니다
    public void SubscribeUpdate(Action method) => UpdateMethod += method;
    // 구독 해제하고자 하는 함수를 UpdateMethod에서 제거합니다
    public void UnSubscribeUpdate(Action method) => UpdateMethod -= method;
    public void SubscribeFixedUpdate(Action method) => FixedUpdateMethod += method;
    public void UnSubscribeFixedUpdate(Action method) => FixedUpdateMethod -= method;
    public void SubscribeLateUpdate(Action method) => LateUpdateMethod += method;
    public void UnSubscribeLateUpdate(Action method) => LateUpdateMethod -= method;
}