using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBullet : MonoBehaviour {
    [SerializeField] Rigidbody rgBody;

    GameObject targetPlayer;
    bool moved = false;

    Vector3 moveDir;
    float speed = 55f;

    public int damage { get; set; } = 1;
    float lifeTime = 3f;


    void Awake() {
        Destroy(this.gameObject, lifeTime);
    }

    void Update() {
        if (targetPlayer != null && moved == false)
            transform.forward = targetPlayer.transform.position - transform.position;
    }

    public void Init(GameObject player) {
        targetPlayer = player;

        StartCoroutine(MoveAfterDelay());
    }

    IEnumerator MoveAfterDelay() {
        yield return new WaitForSeconds(lifeTime * 0.4f);
        
        moved = true;
        moveDir = (targetPlayer.transform.position - transform.position).normalized;
        moveDir *= speed;
        rgBody.AddForce(moveDir, ForceMode.VelocityChange);
    }

}
