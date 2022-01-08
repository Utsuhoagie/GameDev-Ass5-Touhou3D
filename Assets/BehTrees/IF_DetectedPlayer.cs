using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IF_DetectedPlayer : ActionNode {
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.aiType == Blackboard.AI.YOSHIKA)
            return blackboard.Y_playerDetected? State.Success : State.Failure;
        else if (blackboard.aiType == Blackboard.AI.CHIMATA)
            return blackboard.C_playerDetected? State.Success : State.Failure;



        // NOTE: doesn't actually get called
        // just adding to remove "not all paths return..." error
        else
            return State.Failure;
    }
}
