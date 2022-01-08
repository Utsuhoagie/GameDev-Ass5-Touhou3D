using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Billboard : MonoBehaviour {
    Transform cam;

    void Start() {
        cam = GameObject.FindWithTag("MainCamera").transform;
    }

    void Update() {
        // Looks at DIRECTION of camera
        transform.LookAt(transform.position + cam.forward);
    }
}
