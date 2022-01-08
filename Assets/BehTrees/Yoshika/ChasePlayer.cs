using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ChasePlayer : ActionNode {
    Vector3 playerPos;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        playerPos = blackboard.Y_player.transform.position;
        context.gameObject.GetComponent<AI_Yoshika>().MoveTo(playerPos);

        return State.Success;
    }
}
