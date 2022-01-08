using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Detect : MonoBehaviour {
    [SerializeField] AI_Yoshika yoshika;

    void OnTriggerEnter(Collider col) {
        yoshika.SetPlayerTargeted(col.gameObject);
    }

    void OnTriggerExit(Collider col) {
        yoshika.UnsetPlayerTargeted(col.gameObject);
    }
}
