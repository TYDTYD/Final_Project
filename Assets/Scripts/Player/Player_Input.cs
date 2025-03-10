using UnityEngine;
using System.Collections.Generic;

namespace player
{
    public class Player_Input : MonoBehaviour
    {
        Dictionary<KeyCode, InputState> keyValue = new Dictionary<KeyCode, InputState>();
        Dictionary<KeyCode, InputAction> keyDelegate = new Dictionary<KeyCode, InputAction>();
        [SerializeField] GameObject anchor;
        [SerializeField] GameObject bomb;
        Player GetPlayer;
        Move RightMove;
        Move LeftMove;
        class InputState
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
        void Start()
        {
            GetPlayer = GetComponent<Player>();
            RightMove = new Move(GetPlayer.GetRigidbody, 7f, true);
            LeftMove = new Move(GetPlayer.GetRigidbody, 7f, false);

            GetPlayer.GetPlayer_Health.DeathEvent += DisableInput;

            InputAction[] InputActions = {
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
                var key = InputHandler.keyCodes[i];
                keyValue[key] = new InputState(InputActions[i].value, false);
                keyDelegate[key] = InputActions[i];
            }
        }
        private void FixedUpdate()
        {
            foreach (var press in keyValue)
            {
                if (press.Value.isPressed && press.Value.value != 0)
                    keyDelegate[press.Key].Command.Execute(GetPlayer);
            }
        }
        void Update()
        {
            foreach (var key in keyDelegate.Keys)
            {
                keyValue[key].isPressed = (keyValue[key].value == 0)
                ? Input.GetKeyDown(key)  // 단발 입력
                : Input.GetKey(key);     // 지속 입력
            }

            foreach (var press in keyValue)
            {
                if (press.Value.isPressed && press.Value.value == 0)
                    keyDelegate[press.Key].Command.Execute(GetPlayer);
            }
        }
        public Move GetRightMove => RightMove;
        public Move GetLeftMove => LeftMove;
    }
}