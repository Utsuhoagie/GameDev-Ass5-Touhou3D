using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Atkable : MonoBehaviour {
    [SerializeField] AI_Yoshika yoshika;

    void OnTriggerEnter(Collider col) {
        yoshika.SetPlayerAtkable(col.gameObject);
    }

    void OnTriggerExit(Collider col) {
        yoshika.UnsetPlayerAtkable(col.gameObject);
    }
}
