using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Bomb : MonoBehaviour {
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Rigidbody rgBody;
    [SerializeField] new Light light;

    Vector3 moveDir;
    float speed = 50f;
    float maxScale = 10f;
    Vector3 scaleDelta = new Vector3(0.033f, 0.033f, 0.033f);

    public int damage { get; set; } = 40;
    float descent = 0.075f;
    float lifeTime = 3f;
    static float growTime = 1f;
    bool isGrowing = true;

    public static float GetGrowTime() { return growTime; }



    // Methods

    void Awake() {
        Destroy(this.gameObject, lifeTime);
    }

    public void Init(Vector3 forwardPos) {

        moveDir = (forwardPos - transform.position).normalized;
        moveDir.y -= descent;
        moveDir *= speed;
        
        StartCoroutine(WaitForGrow());
    }

    IEnumerator WaitForGrow() {
        yield return new WaitForSeconds(growTime);

        isGrowing = false;

        rgBody.AddForce(moveDir, ForceMode.VelocityChange);
    }

    void Update() {
        //if (powerup_snipe && lifeTime >= )
        if (transform.localScale.x <= maxScale) {
            transform.localScale += isGrowing? scaleDelta : scaleDelta*2f;
            light.intensity += 0.1f;
        }
    }

}
