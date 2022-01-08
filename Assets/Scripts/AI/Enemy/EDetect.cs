using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDetect : MonoBehaviour {

    public delegate void DetectEventDelegate(GameObject player);
    public event DetectEventDelegate DetectEvent;
    
    public delegate void ExitEventDelegate();
    public event ExitEventDelegate ExitEvent;

    [SerializeField] SphereCollider col;
    float detectRange; // { get; set; }


    void Awake() { 
        detectRange = col.radius;

        transform.parent.GetComponent<AI_1>().SetAtkRange(detectRange);
    }

    void OnTriggerEnter(Collider pCol) {
        DetectEvent?.Invoke(pCol.gameObject);
    }

    void OnTriggerExit() {
        ExitEvent?.Invoke();
    }
}
