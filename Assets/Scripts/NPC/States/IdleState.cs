using UnityEngine;

namespace OtakuGameJam
{
    public class IdleState : NPCStateManager
    {

        public override void CurrentNPCState(NPCBehaviour NPC)
        {
            Debug.Log("NPC is idle");


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
