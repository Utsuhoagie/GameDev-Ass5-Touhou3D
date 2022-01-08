using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RandPos : ActionNode {
    bool isYoshika;
    Vector3 ogPos;
    Vector3 rand;
    float delta;

    protected override void OnStart() {
        isYoshika   = blackboard.aiType == Blackboard.AI.YOSHIKA ? true : false;
        ogPos       = isYoshika? blackboard.Y_ogPos : blackboard.C_ogPos;
        delta       = isYoshika? blackboard.Y_roamDelta : blackboard.C_roamDelta;
        rand        = ogPos;
    }

    protected override void OnStop() {}

    protected override State OnUpdate() {

        rand.x = Random.Range(ogPos.x - delta, ogPos.x + delta);
        rand.z = Random.Range(ogPos.z - delta, ogPos.z + delta);
        
        if (isYoshika)
            blackboard.Y_newPos = rand;
        else
            blackboard.C_newPos = rand;

        return State.Success;
    }
}
