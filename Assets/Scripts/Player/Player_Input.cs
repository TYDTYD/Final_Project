using UnityEngine;
using System.Collections.Generic;

namespace player
{
    public class Player_Input : MonoBehaviour
    {
        Interact PlayerInputManager = Interact.instance;

        Dictionary<KeyCode, InputState> keyValue = new Dictionary<KeyCode, InputState>();
        Dictionary<KeyCode, InputAction> keyDelegate = new Dictionary<KeyCode, InputAction>();

        [SerializeField] GameObject anchor;
        [SerializeField] GameObject bomb;

        Player GetPlayer;

        Idle Idle;
        Move RightMove;
        Move LeftMove;
        struct InputState
        {
            // 0 => 트리거 ||  1 => 연속적 트리거
            public int value;
            public bool isPressed;
            public InputState(int v, bool p)
            {
                value = v;
                isPressed = p;
            }
        }
        void EnableInput() => enabled = true;
        void DisableInput() => enabled = false;
        struct InputAction
        {
            public int value;
            public ICommand Command;
            public InputAction(int v, ICommand c)
            {
                value = v;
                Command = c;
            }
        }
        private void OnEnable()
        {
            // 업데이트 매니저의 Instance를 통해 함수를 추가합니다
            UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
            UpdateManager.Instance.SubscribeFixedUpdate(FixedUpdateMethod);
        }
        private void OnDisable()
        {
            // 업데이트 매니저의 Instance를 통해 함수를 제거합니다
            UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
            UpdateManager.Instance.UnSubscribeFixedUpdate(FixedUpdateMethod);
        }
        void Start()
        {
            GetPlayer = GetComponent<Player>();
            RightMove = new Move(GetPlayer.GetRigidbody, 7f, true);
            LeftMove = new Move(GetPlayer.GetRigidbody, 7f, false);
            Idle = new Idle(GetPlayer.GetRigidbody);

            GetPlayer.GetPlayer_Health.DeathEvent += DisableInput;
            GameManager.Instance.Restart += EnableInput;

            InputAction[] InputActions = new InputAction[]
            {
                new InputAction(1, RightMove),
                new InputAction(1, LeftMove),
                new InputAction(1, new Up(transform,GetPlayer.GetRigidbody)),
                new InputAction(1, new Down(transform,GetPlayer.GetRigidbody)),
                new InputAction(0, new Attack(GetPlayer.GetRigidbody)),
                new InputAction(0, new Item(GetPlayer.GetPlayer_Item.CurrentItem)),
                new InputAction(0, new Jump(GetPlayer.GetRigidbody,15f)),
                new InputAction(0, new Rope(anchor)),
                new InputAction(0, new Bomb(bomb))
            };

            for (int i = 0; i < InputActions.Length; i++)
            {
                var key = PlayerInputManager.keyCodes[i];
                keyValue[key] = new InputState(InputActions[i].value, false);
                keyDelegate[key] = InputActions[i];
            }
        }
        private void FixedUpdateMethod()
        {
            foreach (var press in keyValue)
            {
                if (!press.Value.isPressed)
                    continue;

                ICommand command = keyDelegate[press.Key].Command;
                if (press.Value.isPressed && press.Value.value != 0)
                {
                    if (GetPlayer.CurrentState.CanExecute(command))
                        command.Execute(GetPlayer);
                }
            }
        }
        void UpdateMethod()
        {
            bool anyKeyPressed = false;
            foreach (var key in keyDelegate.Keys)
            {
                var inputState = keyValue[key];
                inputState.isPressed = (inputState.value == 0) ? Input.GetKeyDown(key) : Input.GetKey(key);
                keyValue[key] = inputState;
                if (inputState.isPressed)
                    anyKeyPressed = true;
            }

            if (!anyKeyPressed)
            {
                if (GetPlayer.CurrentState.CanExecute(Idle))
                    Idle.Execute(GetPlayer);
                return;
            }

            foreach (var press in keyValue)
            {
                if (!press.Value.isPressed)
                    continue;

                ICommand command = keyDelegate[press.Key].Command;
                if (press.Value.isPressed && press.Value.value == 0)
                {
                    if(GetPlayer.CurrentState.CanExecute(command))
                        command.Execute(GetPlayer);
                }
            }
        }
        public Move GetRightMove => RightMove;
        public Move GetLeftMove => LeftMove;
        public bool GetKeyPress(KeyCode key) => keyValue[key].isPressed;
        public Interact PlayerKey => PlayerInputManager;
    }
}