using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class CollisionDetection : MonoBehaviour
    {
        private SpecialsManager special;

        private RacerAI racerAI;

        private BoxCollider2D boxCollider;

        private GamePlayManager gamePlay;


        private void Awake()
        {

            special = GetComponent<SpecialsManager>();

            racerAI = GetComponent<RacerAI>();

            boxCollider = GetComponent<BoxCollider2D>();

            gamePlay = FindObjectOfType<GamePlayManager>();

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // if (other.gameObject.CompareTag("Powerup"))
            // {
            // }
            if (other.gameObject.CompareTag("Sticky Sap"))
            {
                racerAI.rb.velocity *= 0.4f;
            }

            if (other.gameObject.CompareTag("Starting Line"))
            {

            }

            // if (other.gameObject.CompareTag("Finish Line"))
            // {

            // }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.CompareTag("NPC"))
            {
                SpeedControl(other);
            }

            if (other.gameObject.CompareTag("Track Boundary"))
            {
                SpeedControl(other);
            }

        }

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
                racerAI.rb.velocity *= 0.4f;
            }

            if (left || right)
            {
                racerAI.rb.velocity *= 0.8f;
            }
        }
    }
}
