using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {
        [HideInInspector] public Vector3 moveToPosition;
        [HideInInspector] public float debugWait = 1f;


        public enum AI { YOSHIKA, CHIMATA }
        public AI aiType;

        /* ----- YOSHIKA ---------- */

            // CONST
            public Vector3 Y_ogPos;
            public int Y_baseHP;
            public float Y_roamDelta;
            public float Y_roamTime;
            public float Y_roamPause;

            // VAR
            public int Y_HP;
            public Vector3 Y_newPos;
            public GameObject Y_player;
            public bool Y_playerDetected;
            public bool Y_playerAtkable;

        
        /* ----- CHIMATA ---------- */

            // CONST
            public Vector3 C_ogPos;
            public float C_roamDelta;
            public float C_roamTime;
            public float C_roamPause;

            // VAR
            public int C_HP;
            public bool C_isHostile;
            public Vector3 C_newPos;
            public GameObject C_player;
            public bool C_playerDetected;
            public bool C_playerBuyable;
            public bool C_playerBuying;
            
    }
}