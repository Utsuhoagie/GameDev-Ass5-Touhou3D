using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class WAIT_ROAM : ActionNode {
    public float duration;
    float startTime;

    protected override void OnStart() {
        if (blackboard.aiType == Blackboard.AI.YOSHIKA)
            duration = blackboard.Y_roamTime;
        else if (blackboard.aiType == Blackboard.AI.CHIMATA)
            duration = blackboard.C_roamTime;

        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > duration)
            return State.Success;
        else
            return State.Running;
    }
}