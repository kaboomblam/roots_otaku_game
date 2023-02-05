using System.ComponentModel;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

namespace OtakuGameJam
{
    public class RacingState : NPCStateManager
    {
        private Transform _waypointParent;
        private Transform[] _waypoints;
        private Transform _target;
        private int _currentWaypoint = 1;
        private float _speed;
        private float _turnSpeed;
        private float _turnDistance;
        private float _driftFactor;

        private float _specialTimer;

        private Rigidbody2D _rb;

        private SpecialsManager _specialsManager;



        #region variable setters
        public float Speed
        {
            get { return _speed; }

            set
            {
                if (value > 10)
                {
                    _speed = 10;
                }
                else if (value < 7)
                {
                    _speed = 7;
                }
                else
                    _speed = value;
            }

        }

        public float TurnSpeed
        {
            get { return _turnSpeed; }

            set
            {
                if (value > 8)
                {
                    _turnSpeed = 8;
                }
                else if (value < 6)
                {
                    _turnSpeed = 6;
                }
                else
                    _turnSpeed = value;
            }

        }

        public float TurnDistance
        {
            get { return _turnDistance; }

            set
            {
                if (value > 2f || value == 0)
                {
                    _turnDistance = 2f;
                }
                else if (value < -1)
                {
                    _turnDistance = -1;
                }
                else
                    _turnDistance = value;
            }

        }

        public float DriftFactor
        {
            get { return _driftFactor; }

            set
            {
                if (value > .95f)
                {
                    _driftFactor = .95f;
                }
                else if (value < .5f)
                {
                    _driftFactor = .5f;
                }
                else
                    _driftFactor = value;
            }

        }

        #endregion


        // public RacingState() { }

        // public RacingState(float speed)
        // {
        //     Speed = speed;
        // }

        // public RacingState(float speed, float turnSpeed)
        // {
        //     Speed = speed;

        //     TurnSpeed = turnSpeed;
        // }

        // public RacingState(float speed, float turnSpeed, float turnDistance)
        // {
        //     Speed = speed;

        //     TurnSpeed = turnSpeed;

        //     TurnDistance = turnDistance;

        // }

        // public RacingState(float speed, float turnSpeed, float turnDistance, float driftFactor)
        // {
        //     Speed = speed;

        //     TurnSpeed = turnSpeed;

        //     TurnDistance = turnDistance;

        //     DriftFactor = driftFactor;

        // }

        public override void CurrentNPCState(NPCBehaviour NPC)
        {
            Debug.Log("NPC state change to Racing.");

            Setter(NPC);

            GetWaypoints();

            TurnToNextWaypoint(NPC);

            AddForceToNPC(NPC);

            KillOrthogonalForce(NPC);

            UseSpecial(NPC);

        }

        public override void UpdateState(NPCBehaviour NPC)
        {
            CurrentNPCState(NPC);
        }

        public override void OnCollision(Collision2D other)
        {
            SpeedControl(other);
        }

        #region Data handlers

        private void Setter(NPCBehaviour NPC)
        {
            Speed = NPC.speed;
            TurnSpeed = NPC.turnSpeed;
            TurnDistance = NPC.turnDistance;
            DriftFactor = NPC.driftFactor;
            _specialsManager = NPC.GetComponent<SpecialsManager>();
        }

        private void GetWaypoints()
        {
            _waypointParent = GameObject.Find("Waypoint Sys").GetComponent<Transform>();

            _waypoints = _waypointParent.GetComponentsInChildren<Transform>();
        }

        #endregion

        #region NPC Turn Direction
        private void TurnToNextWaypoint(NPCBehaviour NPC)
        {
            _target = _waypoints[_currentWaypoint]; // Get the current waypoint

            // Rotate the AI car towards the waypoint
            float angle = Mathf.Atan2(
                (_target.position.y - NPC.transform.position.y),
                _target.position.x - NPC.transform.position.x) * Mathf.Rad2Deg;

            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, q, Time.deltaTime * _turnSpeed);


            float distance = Vector2.Distance(NPC.transform.position, _target.position);

            // Check if the AI car has reached the waypoint
            if (distance < _turnDistance)
            {
                _currentWaypoint++;

                // AIError();

                // Go back to the first waypoint if all _waypoints have been reached

                if (_currentWaypoint >= _waypoints.Length)
                {
                    _currentWaypoint = 1;
                }
            }
        }

        #endregion

        #region Force handlers 

        private void AddForceToNPC(NPCBehaviour NPC)
        {
            _rb = NPC.GetComponent<Rigidbody2D>();

            Vector2 direction = (_target.position - NPC.transform.position).normalized;

            _rb.AddForce(direction * _speed, ForceMode2D.Force);

            // Limit the velocity to the speed variable
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _speed);

        }

        private void KillOrthogonalForce(NPCBehaviour NPC)
        {
            Vector2 forwardVelocity = NPC.transform.up * Vector2.Dot(_rb.velocity, NPC.transform.up);

            Vector2 rightVelocity = NPC.transform.right * Vector2.Dot(_rb.velocity, NPC.transform.right);

            _rb.velocity = forwardVelocity * _driftFactor + rightVelocity;
        }

        #endregion

        #region Control speed
        private void SpeedControl(Collision2D other)
        {
            Collider2D collider = other.collider;

            Vector3 contactPoint = other.contacts[0].point;

            Vector3 center = collider.bounds.center;

            bool top = contactPoint.x > center.x;

            bool bottom = contactPoint.x > center.x;

            bool left = contactPoint.y > center.y;

            bool right = contactPoint.y > center.y;

            if (top)
            {
                _rb.velocity *= 0.8f;
            }

            if (left || right)
            {
                _rb.velocity *= 0.9f;
            }
        }

        #endregion

        #region Using special
        private void UseSpecial(NPCBehaviour NPC)
        {

            int rando = Random.Range(0, 1000);

            if (rando < 5 && NPC.specialsCount > 0)
            {
                Debug.Log("Using Special");

                int _specialSelector = Random.Range(0, 2);

                switch (_specialSelector)
                {
                    case 0:
                        {
                            _specialsManager.TriggerSpecial("Nitrous");
                            break;
                        }
                    case 1:
                        {
                            _specialsManager.TriggerSpecial("Stick Sap");
                            break;
                        }
                }

            }


        }

        #endregion
    }
}
