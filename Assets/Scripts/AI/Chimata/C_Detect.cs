using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Detect : MonoBehaviour {
    [SerializeField] AI_Chimata chimata;

    void OnTriggerEnter(Collider col) {
        chimata.SetFaceAndStopRoam(col.gameObject);
    }

    void OnTriggerExit(Collider col) {
        chimata.UnsetFaceAndStopRoam(col.gameObject);
    }
}
