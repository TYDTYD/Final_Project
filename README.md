# Final_Project
마지막 프로젝트


## Github CI/CD 구축
![Image](https://github.com/user-attachments/assets/f97c08f0-135c-45c0-a6ce-a9ca4ef220e6)

Github Actions를 활용하여 자동화 빌드를 구축하였습니다.

## 키 변경 기능 구현
![Final_Project-Setting-WindowsMacLinux-Unity66000 0 26f1_DX11_2025-01-2423-01-05-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/f8652c26-1047-42bc-988f-3b41ad6610c1)

명령 패턴을 사용하여 키를 변경할 수 있도록 구현하였습니다.

## 업데이트 매니저 구현
<details>
  <summary>
    업데이트 매니저
  </summary>
  <pre>
    
```cs
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
```
  </pre>
</details>

## 마스크 기능 구현
![Final_Project-MaskTest-WindowsMacLinux-Unity66000 0 26f1_DX11_2025-02-0221-42-32-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/956c9b84-aed6-4cfd-a6f5-bfd7c9160f8d)

마스크 기능을 통해 연출 효과를 넣었습니다.

## 로프 기능 구현
![Final_Project-Demo-WindowsMacLinux-Unity66000 0 26f1_DX11_2025-02-1518-10-13-ezgif com-video-to-gif-converter-min](https://github.com/user-attachments/assets/2bda6b15-8168-43f5-9f70-f30bb94ccdb9)

로프 기능을 통해 밧줄을 타고 다닐 수 있도록 하였습니다.

## 통계창 구현
![Final_Project-Stage1-WindowsMacLinux-Unity66000 0 26f1_DX11_2025-03-0619-46-20-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/499a337b-f5cf-4cb0-9524-38b3ef09fa6b)

스테이지 중간마다 플레이 기록에 따른 통계창을 볼 수 있도록 구현하였습니다.

## 비동기 씬 로드 구현

비동기적으로 씬을 로드하여 딜레이를 없앴습니다.

## UniRx를 활용한 MVVM 패턴 구현

UniRx를 활용하여 UI를 쉽게 관리할 수 있게끔 Model - View - View Model 패턴을 구성하였습니다.
