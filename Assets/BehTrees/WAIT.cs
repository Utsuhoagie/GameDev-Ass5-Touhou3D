using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class WAIT : ActionNode {
    [HideInInspector] public float duration;
    float startTime;

    protected override void OnStart() {
        duration = blackboard.debugWait;
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