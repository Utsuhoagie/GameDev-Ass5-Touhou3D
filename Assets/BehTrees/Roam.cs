using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Roam : ActionNode {
    Vector3 movePos;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        if (blackboard.aiType == Blackboard.AI.YOSHIKA) {
            movePos = blackboard.Y_newPos;
            context.gameObject.GetComponent<AI_Yoshika>().MoveTo(movePos);
        }
        else if (blackboard.aiType == Blackboard.AI.CHIMATA) {
            movePos = blackboard.C_newPos;
            context.gameObject.GetComponent<AI_Chimata>().MoveTo(movePos);
        }

        return State.Success;
    }
}
