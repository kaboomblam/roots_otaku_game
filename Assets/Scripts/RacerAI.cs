using UnityEngine;

public class RacerAI : MonoBehaviour
{
    public Transform waypointParent;
    public float speed;
    public float turnSpeed;
    public float driftFactor;
    public float turnDistance;

    private Transform[] waypoints;
    Transform target;
    private int currentWaypoint = 1;
    private Rigidbody2D rb;

    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        AISteer();
        AIMotion();
        KillOrthogonalForce();
    }
    private void AISteer()
    {
        // Get the current waypoint
        target = waypoints[currentWaypoint];

        // Rotate the AI car towards the waypoint
        float angle = Mathf.Atan2(
            target.position.y - transform.position.y,
            target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);

        // rb.MoveRotation(angle);

        float distance = Vector2.Distance(transform.position, target.position);

        // Check if the AI car has reached the waypoint

        if (distance < turnDistance)
        {
            currentWaypoint++;

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
}
