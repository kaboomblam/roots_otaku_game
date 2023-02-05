using UnityEngine;

namespace OtakuGameJam
{
    public class StuckState : NPCStateManager
    {
        public override void CurrentNPCState(NPCBehaviour NPC)
        {
            ResetPosition(NPC);
        }

        public override void UpdateState(NPCBehaviour NPC)
        {
            CurrentNPCState(NPC);
        }

        public override void OnCollision(Collision2D other) { }

        private void ResetPosition(NPCBehaviour NPC)
        {
            Transform[] waypoints = GameObject.Find("Waypoint Sys").GetComponentsInChildren<Transform>();

            foreach (Transform waypoint in waypoints)
            {
                var _distance = Vector2.Distance(waypoint.transform.position, NPC.transform.position);

                if (_distance < 2)
                {
                    NPC.transform.position = waypoint.transform.position;
                    NPC.isStuck = false;
                    NPC.UpdateNPCState(NPC.racingState);
                }
            }

        }
    }
}
