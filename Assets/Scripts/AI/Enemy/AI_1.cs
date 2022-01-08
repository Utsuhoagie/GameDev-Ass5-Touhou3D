using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_1 : MonoBehaviour {
    // References
    GameObject targetedPlayer;
    [SerializeField] DropSpawn dropSpawn;
    [SerializeField] EDetect detect;
    [SerializeField] CharacterController ctrl;
    [SerializeField] GameObject pfEBullet;

    // UI references
    [SerializeField] UI_HPBar hpBar;

    // State machine
    enum State { Roam, Chase, Attack }
    State state;


    float lifeTime = 30f;

    // Movement
    float speed = 1.2f;
    float atkSpeedModifier = 0.33f;

    // Roam
    Vector3 ogPos;
    Vector3 roamPos;
    Vector3 moveDir;
    float roamDelay = 4f;
    float delta = 3f;
    float trackRate = 0.05f;

    // Combat
    int HP = 10;
    float fireRate = 1f;
    float range;
    Coroutine fireCrt;


    // -----------------------------------------------

    void Awake() {
        ogPos = transform.position;

        hpBar.SetMaxHP(HP);
        dropSpawn.Init("Enemy");

        // Subscribe to DetectEvent
        detect.DetectEvent += OnPlayerDetected;
        detect.ExitEvent   += OnPlayerExited;
    }

    public void SetAtkRange(float r) {
        range = r * 0.66f;
    }

    void Start() {
        StartCoroutine(RoamStraight());
    }

    void Update() {
        if (HP <= 0) {
            dropSpawn.Drop();
            Destroy(this.gameObject);
        }

        if (moveDir != Vector3.zero)
            ctrl.Move(moveDir * speed * Time.deltaTime);

    }

    
    // ------- Event handling ---------------

    // Collide with PBullet
    void OnTriggerEnter(Collider col) {
        int damage = 0;
        if (col.GetComponent<PBullet>() != null) {
            damage = col.GetComponent<PBullet>().damage;
            ReduceHP(damage);
            Destroy(col.gameObject);
        }
        else if (col.GetComponent<P1Bomb>() != null){
            damage = col.GetComponent<P1Bomb>().damage;
            ReduceHP(damage);
        }
    }
    public void ReduceHP(int damage) {
        HP -= damage;
        hpBar.SetHP(HP);
    }


    void OnPlayerDetected(GameObject player) {
        Debug.Log($"Detected player!");
        targetedPlayer = player;
    }

    void OnPlayerExited() {
        Debug.Log($"Player exited! Resume roaming...");
        targetedPlayer = null;
    }


    // --------- Coroutines ----------------

    IEnumerator RoamStraight() {
        moveDir = transform.forward;
        float sineTimer = 0f;
        while (true) {
            if (targetedPlayer != null) {
                StartCoroutine(ChasePlayer());
                yield break;
            }

            yield return new WaitForSeconds(trackRate);

            moveDir.y = Mathf.Sin(sineTimer) * 0.5f;
            sineTimer += 0.1f;
        }
    }



    IEnumerator ChasePlayer() {
        while (true) {
            yield return new WaitForSeconds(trackRate);

            if (targetedPlayer == null) {
                moveDir = Vector3.zero;

                StartCoroutine(RoamStraight());
                yield break;
            }
            else if (Vector3.Distance(transform.position, targetedPlayer.transform.position) <= range) {
                StartCoroutine(AtkChasePlayer());
                yield break;
            }


            Vector3 playerPos = targetedPlayer.transform.position;
            
            moveDir = (playerPos - transform.position).normalized;
            moveDir.y *= 1.1f;
            transform.forward = new Vector3(moveDir.x, 0, moveDir.z);
        }
    }

    IEnumerator AtkChasePlayer() {
        fireCrt = StartCoroutine(Fire());

        while (true) {
            yield return new WaitForSeconds(trackRate);

            if (targetedPlayer == null) {
                moveDir = Vector3.zero;

                StartCoroutine(RoamStraight());
                StopCoroutine(fireCrt);
                yield break;
            }
            else if (Vector3.Distance(transform.position, targetedPlayer.transform.position) > range) {
                StartCoroutine(ChasePlayer());
                StopCoroutine(fireCrt);
                yield break;
            }


            Vector3 playerPos = targetedPlayer.transform.position;

            moveDir = (playerPos - transform.position).normalized;
            transform.forward = new Vector3(moveDir.x, 0, moveDir.z);
            moveDir.x *= atkSpeedModifier;
            moveDir.z *= atkSpeedModifier;
        }
    }
    IEnumerator Fire() {
        while (true) {
            yield return new WaitForSeconds(fireRate);

            Vector3 firePos = transform.position + transform.forward + transform.up*0.3f;
            GameObject eBullet = Instantiate(pfEBullet, firePos, Quaternion.identity);
            eBullet.GetComponent<EBullet>().Init(transform.forward);
        }
    }
}
