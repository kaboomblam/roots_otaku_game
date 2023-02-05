using UnityEngine;

namespace OtakuGameJam
{
    public class NPCBehaviour : MonoBehaviour
    {
        [Range(7, 10)]
        public float speed;

        private float prevSpeed;

        [Range(6, 8)]
        public float turnSpeed;

        [Range(-1, 1.5f)]
        public float turnDistance;

        [Tooltip("Controls how much the NPC drifts on turns")]
        [Range(.5f, .85f)]
        public float driftFactor;

        [Space]

        public bool isStuck;
        public float stuckResetTimer = 3f;


        public SpecialsManager specialsManager;
        public int specialsCount;

        NPCStateManager currentState;

        public IdleState idleState = new IdleState();
        public RacingState racingState = new RacingState();
        public StuckState stuckState = new StuckState();
        public GameOverState gameOverState = new GameOverState();


        [Space]

        #region Difficulty setting

        [Header("NPC difficulty settings")]
        public int easy;
        public int normal;
        public int hard;


        #endregion


        // Start is called before the first frame update
        void Start()
        {
            specialsCount = 5;

            prevSpeed = speed;

            currentState = racingState;
        }

        void FixedUpdate()
        {
            currentState.CurrentNPCState(this);
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                UpdateNPCState(idleState);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                UpdateNPCState(racingState);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                UpdateNPCState(stuckState);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                UpdateNPCState(gameOverState);
            }

            if (isStuck)
            {
                UpdateNPCState(stuckState);
            }

            if (speed >= prevSpeed)
            {
                speed -= Time.deltaTime;
            }
        }

        public void UpdateNPCState(NPCStateManager state)
        {
            currentState = state;
            state.UpdateState(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            currentState.OnCollision(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("NPC") || !other.gameObject.CompareTag("Player"))
            {
                stuckResetTimer -= Time.deltaTime;
                if (stuckResetTimer < 0)
                {
                    isStuck = true;
                    stuckResetTimer = 3f;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            stuckResetTimer = 3f;
        }


    }
}
