using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour {
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Rigidbody rgBody;

    Vector3 moveDir;
    float speed = 75f;

    public int damage { get; set; } = 1;
    float lifeTime = 0.75f;
    bool isSnipe = false;


    void Awake() {
    }

    public void Init(Vector3 dir, bool _snipe = false) {
        isSnipe = _snipe;

        moveDir = dir.normalized * speed;

        rgBody.AddForce(moveDir, ForceMode.VelocityChange);

        //StartCoroutine(ShowAfterDelay());
        StartCoroutine(SetPowerup());

        Destroy(this.gameObject, lifeTime);
    }

    // IEnumerator ShowAfterDelay() {
    //     yield return new WaitForSeconds(visibleAfter);

    //     mesh.enabled = true;
    // }

    IEnumerator SetPowerup() {
        if (isSnipe) {
            yield return new WaitForSeconds(lifeTime * 0.5f);
            mesh.material.color = Color.red;
            transform.localScale *= 2.5f;
            damage = 2;
        }
    }

    void Update() {
        //if (powerup_snipe && lifeTime >= )
    }
}
