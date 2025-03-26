namespace player
{
    using UnityEngine;
    public class Player_Rigidbody : MonoBehaviour
    {
        [SerializeField] Transform GetTransform;
        Rigidbody2D GetRigidbody2D;
        Player GetPlayer;

        [HideInInspector] public bool ledgeDetected;

        [Header("Ledge Info")]
        [SerializeField] Vector2 offset1;
        [SerializeField] Vector2 offset2;

        Vector2 climbBegunPosition;
        Vector2 climbOverPosition;

        public bool canGrabLedge = true;
        public bool canClimb;

        bool Grounded = false;
        bool Ladder = false;
        bool Climbing = false;
        float gravity = 6f;
        void Start()
        {
            GetPlayer = GetComponent<Player>();
            GetRigidbody2D = GetPlayer.GetRigidbody;
        }
        private void Update()
        {
            CheckCollision();
            CheckForLedge();
        }

        void CheckForLedge()
        {
            if(ledgeDetected && canGrabLedge)
            {
                canGrabLedge = false;

                //Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;

                //climbBegunPosition = ledgePosition + offset1;
                //climbOverPosition = ledgePosition + offset2;

                canClimb = true;
            }
            /*
            if (canClimb)
                transform.position = climbBegunPosition;
            */
        }

        void CheckCollision()
        {
            Debug.Log(canGrabLedge);
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
        void CheckGrounded(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
            {
                Grounded = true;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                Ladder = true;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckGrounded(collision);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            CheckGrounded(collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Grounded = false;
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                Ladder = true;
                if (GetClimbing)
                {
                    transform.position = new Vector3(collision.transform.position.x
                        - GetTransform.localPosition.x, transform.position.y);
                }
                return;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                Ladder = false;
                GetClimbing = false;
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
    }
}