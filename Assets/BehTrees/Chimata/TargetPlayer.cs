using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class TargetPlayer : ActionNode {
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // TODO: need anything to do here?
        // player already targeted from AI_Chimata

        return State.Success;
    }
}
