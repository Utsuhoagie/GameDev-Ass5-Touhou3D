using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ShowItems : ActionNode {
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.gameObject.GetComponent<AI_Chimata>().SetSpeechBubble(false);
        context.gameObject.GetComponent<AI_Chimata>().SetItems(true);

        return State.Success;
    }
}
