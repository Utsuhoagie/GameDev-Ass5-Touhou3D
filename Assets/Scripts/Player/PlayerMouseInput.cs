using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseInput : MonoBehaviour {
    float sensitivityX = 20f;
    float sensitivityY = 0.05f;
    float mouseX, mouseY;

    Transform tCamera;
    float clampLow = -10f;
    float clampHigh = 45f;
    float cameraHeight = 0f;


    void Awake() {
        //Cursor.lockState = CursorLockMode.Locked;
        
        tCamera = this.gameObject.transform.Find("Main Camera");
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        cameraHeight = Mathf.Clamp(cameraHeight - mouseY, clampLow, clampHigh);
        Vector3 newRotation = transform.eulerAngles;
        newRotation.x = cameraHeight;

        // tCamera.eulerAngles = newRotation;
    }




    public void GetMouseInput(Vector2 mouseInput) {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }
}
