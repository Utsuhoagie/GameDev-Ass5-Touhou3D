using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IF_AtkablePlayer : ActionNode {
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.Y_playerAtkable) {
            return State.Success;
        }
        else {
            context.gameObject.GetComponent<AI_Yoshika>().StopAttack();
            return State.Failure;
        }
    }
}
