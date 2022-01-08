using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RoamPause : ActionNode {
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.aiType == Blackboard.AI.YOSHIKA)
            context.gameObject.GetComponent<AI_Yoshika>().PauseRoam();
        else if (blackboard.aiType == Blackboard.AI.CHIMATA)
            context.gameObject.GetComponent<AI_Chimata>().PauseRoam();

        return State.Success;
    }
}