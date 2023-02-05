using UnityEngine;

namespace OtakuGameJam
{

    public abstract class NPCStateManager
    {
        public abstract void CurrentNPCState(NPCBehaviour NPC);
        public abstract void UpdateState(NPCBehaviour NPC);
        public abstract void OnCollision(Collision2D other);
    }
}
