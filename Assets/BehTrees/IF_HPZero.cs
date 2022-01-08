using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IF_HPZero : ActionNode {
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.aiType == Blackboard.AI.YOSHIKA) {
            return (blackboard.Y_HP <= 0) ? 
                State.Success : 
                State.Failure ;
        }

        else if (blackboard.aiType == Blackboard.AI.CHIMATA) {
            return (blackboard.C_HP <= 0) ? 
                State.Success : 
                State.Failure ;
        }



        // NOTE: DOESN'T ACTUALLY RUN
        // only to bypass compile errors
        else
            return State.Failure;
    }
}
