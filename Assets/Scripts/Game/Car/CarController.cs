using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{

    public class CarController : MonoBehaviour
    {
        [Header("Car Settings")]
        public float accelerationFactor = 30.0f;
        public float turnFactor = 3.5f;

        public float driftFactor = 0.95f;
        float accelerationInput = 0f;
        float steeringInput = 0f;

        float rotationAngle = 0f;

        public float maxSpeed = 20;

        private Rigidbody2D _rb2d;

        float velocityVsUp = 0;

        // Start is called before the first frame update
        void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            ApplyEngineForce();
            KillOrthoganalVelocity();
            ApplySteering();
        }

        void ApplyEngineForce()
        {

            velocityVsUp = Vector2.Dot(transform.up, _rb2d.velocity);

            if (velocityVsUp > maxSpeed && accelerationFactor > 0)
                return;

            if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
                return;

            if (_rb2d.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
                return;

            if (accelerationInput == 0)
            {
                _rb2d.drag = Mathf.Lerp(_rb2d.drag, 3.0f, Time.fixedDeltaTime * 3);
            }
            else _rb2d.drag = 0;



            Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

            _rb2d.AddForce(engineForceVector, ForceMode2D.Force);
        }

        void ApplySteering()
        {
            float minSpeedBeforeAllowTurningFactor = (_rb2d.velocity.magnitude / 8);
            minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

            rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

            _rb2d.MoveRotation(rotationAngle);
        }

        public void SetInputVector(Vector2 inputVector)
        {
            steeringInput = inputVector.x;
            accelerationInput = inputVector.y;
        }

        void KillOrthoganalVelocity()
        {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(_rb2d.velocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(_rb2d.velocity, transform.right);

            _rb2d.velocity = forwardVelocity + rightVelocity * driftFactor;
        }



        private void Update()
        {
            Vector2 inputVector = Vector2.zero;

            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            SetInputVector(inputVector);
        }


    }
}
