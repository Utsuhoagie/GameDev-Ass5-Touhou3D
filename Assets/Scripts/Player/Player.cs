using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    // References
    GameObject player;
    [SerializeField] CharacterController ctrl;
    [SerializeField] PlayerMouseInput mouseInputCtrl;
    [SerializeField] PlayerShotManager shotCtrl;
    [SerializeField] PlayerAnimations animCtrl;

    // Player input class (C# generated class from Asset)
    PlayerInput baseInput;
    PlayerInput.PlayerActions input;


    // Player variables
    Vector3 moveDir = Vector3.zero;
    float speed = 6f;
    float ySpeed = 1.2f;
    Vector2 yLimits = new Vector2(-2f, 2f);



    // ------------ Lifetime methods --------------------
    void Awake() {
        player = this.gameObject;

        shotCtrl.BombEvent += DisableInput;

        SetupInput();
    }

    void SetupInput() {
        baseInput = new PlayerInput();
        baseInput.Player.Enable();
        input = baseInput.Player;
    }

    void Start() {}

    void Update() {

        MouseLook();

        MovePlayer();

    }


    // ----- Custom methods ----------

    void MouseLook() {
        Vector2 mouseInput = new Vector2(input.MouseX.ReadValue<float>(), input.MouseY.ReadValue<float>());
        mouseInputCtrl.GetMouseInput(mouseInput);
        //Debug.Log($"mouseInput = {mouseInput}");
    }

    void MovePlayer() {
        // Play animation for tilting forward/backward
        PlayAnimations(input.Fly.ReadValue<Vector3>());

        // if no input, return 
        if (input.Fly.ReadValue<Vector3>() == Vector3.zero)
            return;

        // read direction (including fly height)...
        moveDir = input.Fly.ReadValue<Vector3>();
        moveDir.y *= ySpeed;

        // ...then rotate it according to this Transform's eulerAngles
        //    so it aligns with object's XYZ axes
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles);
        moveDir = rotation * moveDir;

        // Move
        ctrl.Move(moveDir.normalized * speed * Time.deltaTime);
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, Mathf.Clamp(pos.y, yLimits[0], yLimits[1]), pos.z);
    }

    void PlayAnimations(Vector3 moveDir) {
        int tiltDir =   Mathf.Approximately(moveDir.z, 0f)? 
                            0 : 
                        moveDir.z > 0f ? 
                            1 : 
                            -1;

        switch (tiltDir) {
            case 0: 
                animCtrl.StopTilt();
                break;

            case 1:
                animCtrl.TiltForward();
                break;

            case -1:
                animCtrl.TiltBackward();
                break;
        }
    }

    void DisableInput(int bombChange, float growTime) {
        StartCoroutine(BombDelay(growTime));
    }

    IEnumerator BombDelay(float growTime) {
        input.Disable();
        yield return new WaitForSeconds(growTime);
        input.Enable();
    }
}
