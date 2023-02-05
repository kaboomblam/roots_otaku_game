using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class GameOverState : NPCStateManager
    {

        Rigidbody2D rb;

        public override void CurrentNPCState(NPCBehaviour NPC)
        {
            rb = NPC.GetComponent<Rigidbody2D>();

            rb.velocity -= rb.velocity * Time.deltaTime;
        }

        public override void UpdateState(NPCBehaviour NPC)
        {
            CurrentNPCState(NPC);
        }
        public override void OnCollision(Collision2D other)
        {

        }


    }
}
