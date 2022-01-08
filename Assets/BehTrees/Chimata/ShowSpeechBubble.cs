using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ShowSpeechBubble : ActionNode {
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.gameObject.GetComponent<AI_Chimata>().SetSpeechBubble(true);
        context.gameObject.GetComponent<AI_Chimata>().SetItems(false);

        return State.Success;
    }
}
