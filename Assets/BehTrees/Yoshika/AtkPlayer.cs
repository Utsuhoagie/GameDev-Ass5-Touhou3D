using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AtkPlayer : ActionNode {
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.gameObject.GetComponent<AI_Yoshika>().Attack();

        return State.Success;
    }
}
