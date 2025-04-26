namespace player
{
    using UnityEngine;
    public class Player_Rigidbody : MonoBehaviour
    {
        [SerializeField] Transform GetTransform;
        Rigidbody2D GetRigidbody2D;
        Player GetPlayer;

        bool Grounded = false;
        bool Ladder = false;
        bool Climbing = false;
        bool Wall = false;
        bool Ledge = false;
        float gravity = 6f;
        void Start()
        {
            GetPlayer = GetComponent<Player>();
            GetRigidbody2D = GetPlayer.GetRigidbody;
        }
        private void Update()
        {
            Debug.Log($"Ledge : {Ledge}");
            Debug.Log($"Wall : {Wall}");
        }
        void UpdateClimbingState()
        {
            if (GetClimbing)
            {
                GetRigidbody2D.gravityScale = 0f;
                GetRigidbody2D.linearVelocityY = 0f;
            }
            else
            {
                GetRigidbody2D.gravityScale = gravity;
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (Ladder)
            {
                if (GetClimbing)
                {
                    transform.position = new Vector3(collision.transform.position.x
                        - GetTransform.localPosition.x, transform.position.y);
                }
                return;
            }
        }
        public bool GetClimbing
        {
            get => Climbing;
            set
            {
                if (Climbing != value)
                {
                    Climbing = value;
                    UpdateClimbingState();
                }
            }
        }
        public bool GetGrounded
        {
            get => Grounded;
            set
            {
                if (Grounded != value)
                {
                    Grounded = value;
                }
            }
        }
        public bool GetLadder
        {
            get => Ladder;
            set
            {
                if (Ladder != value)
                {
                    Ladder = value;
                }
            }
        }
        public bool GetWall
        {
            get => Ladder;
            set
            {
                if (Wall != value)
                    Wall = value;
            }
        }
        public bool GetLedge
        {
            get => Ledge;
            set
            {
                if (Ledge != value)
                    Ledge = value;
            }
        }
    }
}