using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour {
    Rigidbody rgBody;

    Vector3 moveDir;
    float speed = 16f;

    public int damage { get; set; } = 1;
    float lifeTime = 1.5f;


    void Awake() {
        rgBody = gameObject.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
    }

    public void Init(Vector3 dir) {
        moveDir = dir * speed;
        rgBody.AddForce(moveDir, ForceMode.VelocityChange);
    }
}
