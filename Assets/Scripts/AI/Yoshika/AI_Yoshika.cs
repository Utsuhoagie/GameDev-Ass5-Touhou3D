using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Yoshika : MonoBehaviour {
    // References
    GameObject targetedPlayer;
    [SerializeField] DropSpawn dropSpawn;
    [SerializeField] Canvas canvas;
    [SerializeField] UI_HPBar hpBar;
    [SerializeField] GameObject pfYBullet;
    [SerializeField] CharacterController ctrl;
    

    // Behavior Tree
    [SerializeField] TheKiwiCoder.BehaviourTreeRunner bt;


    // Movement
    float speed = 1f;

    // Roam
    Vector3 roamPos;
    Vector3 moveDir;
    float roamTime = 4f;
    float roamPause = 2f;
    float roamDelta = 5f;



    // Combat
    bool isAtking = false;
    float atkDelay = 1.5f;
    int HP = 75, maxHP = 75;
    float tempDeathTime = 15f;
    bool dying = false;

    float waveRate = 3f;
    float fireRate = 0.1f;
    Vector3 fireCenter;
    float fireDelta = 5f;
    int maxBullets = 12;
    List<GameObject> bulletList = new List<GameObject>();



    // ------------------------------------------------------------------

    void Awake() {
        hpBar.SetMaxHP(HP);

        dropSpawn.Init("Yoshika");

        InitBlackboard();
    }

    void InitBlackboard() {
        // CONST
        bt.tree.blackboard.aiType       = TheKiwiCoder.Blackboard.AI.YOSHIKA;
        bt.tree.blackboard.Y_baseHP     = maxHP;
        bt.tree.blackboard.Y_ogPos      = transform.position;
        bt.tree.blackboard.Y_roamDelta  = roamDelta;
        bt.tree.blackboard.Y_roamTime   = roamTime;
        bt.tree.blackboard.Y_roamPause  = roamPause;

        // VAR
        bt.tree.blackboard.Y_HP             = HP;
        bt.tree.blackboard.Y_newPos         = Vector3.zero;
        bt.tree.blackboard.Y_playerDetected = false;
        bt.tree.blackboard.Y_playerAtkable  = false;
    }

    void Update() {
        
            //Destroy(this.gameObject);
            // TODO: YOSHIKA revive!!!
            

        if (moveDir != Vector3.zero) {
            ctrl.Move(moveDir * speed * Time.deltaTime);
        }

    }

    
    // ------- Event handling ---------------


    public void SetPlayerTargeted(GameObject player) {
        Debug.Log($"Detected player!");
        
        targetedPlayer = player;

        bt.tree.blackboard.Y_player = player;
        bt.tree.blackboard.Y_playerDetected = true;

    }

    public void UnsetPlayerTargeted(GameObject player) {
        Debug.Log($"Player exited!");
        
        if (targetedPlayer == player) {
            targetedPlayer = null;

            bt.tree.blackboard.Y_player = null;
            bt.tree.blackboard.Y_playerDetected = false;
        }
    }


    public void SetPlayerAtkable(GameObject player) {
        Debug.Log($"Player in range!");
        
        bt.tree.blackboard.Y_playerAtkable = true;
    }

    public void UnsetPlayerAtkable(GameObject player) {
        Debug.Log($"Player out of range!");

        bulletList.Clear();
        
        bt.tree.blackboard.Y_playerAtkable = false;
    }


    // Collide with PBullet
    void OnTriggerEnter(Collider col) {
        if (dying) {
            if (col.GetComponent<PBullet>() != null)
                Destroy(col.gameObject);
            
            return;
        }

        int damage;
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

        bt.tree.blackboard.Y_HP = HP;
    }



    // ------ Methods --------
    // Called from BehaviorTree

    public void DieTemporary() {
        if (dying)
            return;

        dying = true;
        transform.position -= transform.up;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x - 90f, transform.eulerAngles.y, transform.eulerAngles.z);

        canvas.enabled = false;
        
        isAtking = false;
        moveDir = Vector3.zero;

        StopAllCoroutines();

        ctrl.enabled = false;
        dropSpawn.Drop();

        StartCoroutine(Revive());
    }

    IEnumerator Revive() {
        yield return new WaitForSeconds(tempDeathTime);

        dying = false;
        canvas.enabled = true;
        HP = maxHP;
        hpBar.SetHP(HP);
        bt.tree.blackboard.Y_HP = HP;

        ctrl.enabled = true;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90f, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position += transform.up;
    }



    void TurnWithoutTilt(Vector3 pos) {
        transform.forward = (new Vector3(pos.x, transform.position.y, pos.z) - transform.position);
    }

    public void MoveTo(Vector3 pos) {
        TurnWithoutTilt(pos);

        if (isAtking)
            return;

        moveDir = (pos - transform.position).normalized;
    }

    public void PauseRoam() {
        moveDir = Vector3.zero;
    }




    public void Attack() {
        TurnWithoutTilt(targetedPlayer.transform.position);

        if (!isAtking) {
            isAtking = true;

            StartCoroutine(StartFire());
        }
    }

    public void StopAttack() {
        isAtking = false;
    }

    IEnumerator StartFire() {
        yield return new WaitForSeconds(atkDelay);

        moveDir = Vector3.zero;
        StartCoroutine(Fire());
    }

    IEnumerator Fire() {
        while (true) {

            if (bulletList.Count >= maxBullets) {
                bulletList.Clear();
                yield return new WaitForSeconds(waveRate);
            }

            if (dying || !isAtking)
                yield break;

            yield return new WaitForSeconds(fireRate);

            fireCenter = transform.position + transform.forward*0.8f;
            Vector3 offset  = new Vector3(Random.Range(-fireDelta, fireDelta), Random.Range(-fireDelta, fireDelta), 0);
            Vector3 firePos = fireCenter + offset;

            GameObject bullet = Instantiate(pfYBullet, firePos, Quaternion.identity);
            bullet.GetComponent<YBullet>().Init(targetedPlayer);
            bulletList.Add(bullet);


            // Vector3 offset = new Vector3()
            // Vector3 firePos = fireCenter + ;
            // GameObject eBullet = Instantiate(pfYBullet, firePos, Quaternion.identity);
            // eBullet.GetComponent<EBullet>().Init(transform.position);
        }
    }
}
