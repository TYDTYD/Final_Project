namespace player
{
    using UnityEngine;
    public class LandState : IState
    {
        Player player;
        public LandState(Player GetPlayer)
        {
            player = GetPlayer;
        }
        float LandTime = 2f, timeIdx = 0f;

        public PlayerStateType StateType => PlayerStateType.Land;

        public bool CanExecute(ICommand command)
        {
            if (player.GetDamaged)
            {
                player.CurrentState = player.GetDamage;
                return false;
            }
            if (command is Attack)
                return false;
            if (command is Down)
                return false;
            if (command is Idle)
            {
                timeIdx += Time.deltaTime;
                if (player.GetPlayer_Rigidbody.GetGrounded && timeIdx > LandTime)
                {
                    timeIdx = 0f;
                    return true;
                }
                return false;
            }
            if (command is Jump)
                return false;
            if (command is Move)
                return false;
            if (command is Up)
                return false;
            return false;
        }
    }
}