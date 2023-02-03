using System.Collections;
using UnityEngine;
using OtakuGameJam;
using TMPro;

namespace OtakuGameJam
{
    public class RacerAI : MonoBehaviour
    {
        [HideInInspector]
        public float speed;
        [HideInInspector]
        public float turnSpeed;
        [HideInInspector]
        public float driftFactor;

        [Tooltip("Distance from waypoint for AI to turn. (optimum value = 2)")]
        [SerializeField]
        private float maxTurnDistance;

        [HideInInspector]
        public float turnDistance;
        // public float turnErrorMin;
        // public float turnErrorMax;

        float timer = 3f;

        [Range(-1, 1.5f)]
        [SerializeField]
        private float turnError;

        [HideInInspector]
        public Rigidbody2D rb;

        [SerializeField]
        private Transform waypointParent;
        private Transform[] waypoints;
        private Transform target;
        private int currentWaypoint = 1;

        private SpecialsManager specials;

        [SerializeField]
        private TextMeshProUGUI _speedText;

        void Start()
        {
            waypoints = waypointParent.GetComponentsInChildren<Transform>();

            rb = GetComponent<Rigidbody2D>();

            turnDistance = maxTurnDistance;

            specials = GetComponent<SpecialsManager>();

            specials._specials = 5;
            // StartCoroutine("UserSpecial");
            InvokeRepeating("UserSpecial", 1, .5f);
        }

        void FixedUpdate()
        {
            IsStuck();

            AISteer();

            AIMotion();

            KillOrthogonalForce();

            _speedText.SetText($"{(rb.velocity.magnitude * 12.5).ToString("00")} KM/H");

        }

        private void AISteer()
        {
            // Get the current waypoint
            target = waypoints[currentWaypoint];

            // Rotate the AI car towards the waypoint
            float angle = Mathf.Atan2(
                (target.position.y - transform.position.y),
                target.position.x - transform.position.x) * Mathf.Rad2Deg;

            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);


            float distance = Vector2.Distance(transform.position, target.position);

            // Check if the AI car has reached the waypoint
            if (distance < turnDistance)
            {
                currentWaypoint++;

                Debug.Log(currentWaypoint);

                AIError();

                // Go back to the first waypoint if all waypoints have been reached

                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 1;
                }
            }
        }
        private void AIMotion()
        {
            Vector2 direction = (target.position - transform.position).normalized;

            rb.AddForce(direction * speed, ForceMode2D.Force);

            // Limit the velocity to the speed variable
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);

        }
        private void KillOrthogonalForce()
        {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);

            Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

            rb.velocity = forwardVelocity * driftFactor + rightVelocity;
        }
        private void AIError()
        {
            turnError = Random.Range(-1, 1.6f);

            turnDistance = maxTurnDistance + turnError;
        }

        private void IsStuck()
        {
            if (rb.velocity.magnitude * 10.5 < 5)
            {
                // timer -= Time.deltaTime;

                // if (timer < 0)
                // {
                //     if (currentWaypoint <= 1)
                //     {
                //         currentWaypoint = waypoints.Length;
                //         timer = 3f;
                //     }
                //     else
                //     {
                //         currentWaypoint--;
                //         Debug.Log($"IsStuck {currentWaypoint}");
                //         timer = 3f;
                //     }
                // }
            }
            else
                return;
        }
        private void UserSpecial()
        {
            Debug.Log("Called");
            int rando = Random.Range(0, 100);

            if (rando < 5 && specials._specials > 0)
            {

                specials.TriggerSpecial();
            }
            if (rando > 95)
            {

                specials.AddToSpecial();
            }
        }
    }
}