using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBullet : MonoBehaviour {
    [SerializeField] Rigidbody rgBody;
    [SerializeField] MeshRenderer mesh;

    GameObject targetPlayer;
    bool moved = false;

    Vector3 moveDir;
    float speed = 8f;

    public int damage { get; set; } = 1;
    float lifeTime = 2f;


    void Awake() {
        Destroy(this.gameObject, lifeTime);
    }

    void Update() {

    }

    public void Init(Vector3 dir) {
        moveDir = dir;
        moveDir *= speed;

        Debug.DrawRay(transform.position, dir * 4f, Color.red, 0.5f);
        rgBody.AddForce(moveDir, ForceMode.VelocityChange);


        transform.forward = moveDir;


        mesh.material.color = Utils.RandomColor();
    }

}
