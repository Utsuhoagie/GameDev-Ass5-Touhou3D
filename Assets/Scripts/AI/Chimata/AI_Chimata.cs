using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Chimata : MonoBehaviour {
    // References
    GameObject targetedPlayer;
    [SerializeField] DropSpawn dropSpawn;
    [SerializeField] CharacterController ctrl;
    [SerializeField] GameObject pfCBullet;
    [SerializeField] C_FirePosSpinner firePosSpinner;

    [SerializeField] C_Buyable buyableDetect;
    [SerializeField] GameObject pfItem;
    
    // UI references
    [SerializeField] Canvas canvas;
    [SerializeField] UI_ChimataTopDisplay topDisplay;
    [SerializeField] UI_HPBar hpBar;
    bool showingItems = false;


    // Behavior Tree
    [SerializeField] TheKiwiCoder.BehaviourTreeRunner bt;


    // Movement
    float speed = 2.5f;

    // Roam
    Vector3 roamPos;
    Vector3 moveDir;
    float roamTime = 3f;
    float roamPause = 1.5f;
    float roamDelta = 5f;


    // Items
    int item_HP_stock    = 4;
    int item_P_stock     = 4;
    int item_Bomb_stock  = 1;
    int item_Snipe_stock = 1;


    // Combat
    int HP = 75;
    bool isDead = false;
    bool isAtking = false;
    float atkDelay = 2f;
    float fireRate = 0.15f;


    // ------------------------------------------------------------------

    void Awake() {
        hpBar.SetMaxHP(HP);

        dropSpawn.Init("Chimata");

        InitBlackboard();
    }

    void InitBlackboard() {
        // CONST
        bt.tree.blackboard.aiType       = TheKiwiCoder.Blackboard.AI.CHIMATA;
        bt.tree.blackboard.C_ogPos      = transform.position;
        bt.tree.blackboard.C_roamDelta  = roamDelta;
        bt.tree.blackboard.C_roamTime   = roamTime;
        bt.tree.blackboard.C_roamPause  = roamPause;

        // VAR
        bt.tree.blackboard.C_HP             = HP;
        bt.tree.blackboard.C_newPos         = Vector3.zero;
        bt.tree.blackboard.C_playerDetected = false;
        bt.tree.blackboard.C_playerBuyable  = false;
    }

    void Update() {
        if (moveDir != Vector3.zero)
            ctrl.Move(moveDir * speed * Time.deltaTime);
    }

    
    // ------- Event handling ---------------


    public void SetFaceAndStopRoam(GameObject player) {
        Debug.Log($"Facing player!");
        
        targetedPlayer = player;
        bt.tree.blackboard.C_player = player;
        bt.tree.blackboard.C_playerDetected = true;
    }

    public void UnsetFaceAndStopRoam(GameObject player) {
        Debug.Log($"Stop facing player!");

        // NOTE: Check for multiplayer
        // but I can't do it yet lmoa
        if (targetedPlayer == player) {
            targetedPlayer = null;
            isAtking = false;

            bt.tree.blackboard.C_player = null;
            bt.tree.blackboard.C_playerDetected = false;
        }        
    }


    public void SetBuyable() {
        Debug.Log($"Buyable!");

        bt.tree.blackboard.C_playerBuyable = true;
    }

    public void UnsetBuyable() {
        Debug.Log($"Not buyable!");

        bt.tree.blackboard.C_playerBuyable = false;
    }

    public void SetBuying() {
        Debug.Log($"Buying!");

        bt.tree.blackboard.C_playerBuying = true;
    }

    public void UnsetBuying() {
        Debug.Log($"Not buying!");

        bt.tree.blackboard.C_playerBuying = false;
    }


    // Collide with PBullet
    void OnTriggerEnter(Collider col) {
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
        if (!bt.tree.blackboard.C_isHostile) {
            bt.tree.blackboard.C_isHostile = true;
            buyableDetect.enabled = false;
        }

        HP -= damage;
        hpBar.SetHP(HP);

        bt.tree.blackboard.C_HP = HP;
    }




    // ------ Methods --------

    // Called from PlayerShopInteractions.cs
    public void BuyItem(Item.Type itemType) {
        int itemCount = 1;

        if (!showingItems)
            return;

        switch (itemType) {
            case Item.Type.HP:
                if (item_HP_stock <= 0)
                    return;

                item_HP_stock -= 4;
                if (item_HP_stock <= 0)
                    topDisplay.Item(itemType, false);

                itemCount = 4;

                break;

            case Item.Type.P:
                if (item_P_stock <= 0)
                    return;

                item_P_stock -= 4;
                if (item_P_stock <= 0)
                    topDisplay.Item(itemType, false);

                itemCount = 4;

                break;

            case Item.Type.Bomb:
                if (item_Bomb_stock <= 0)
                    return;

                item_Bomb_stock--;
                if (item_Bomb_stock <= 0)
                    topDisplay.Item(itemType, false);

                break;

            case Item.Type.Snipe:
                if (item_Snipe_stock <= 0)
                    return;

                item_Snipe_stock--;
                if (item_Snipe_stock <= 0)
                    topDisplay.Item(itemType, false);                

                break;
        }

        while (itemCount > 0) {
            Item item = Instantiate(pfItem, targetedPlayer.transform.position, Quaternion.identity).GetComponent<Item>();
            item.Init(itemType);

            itemCount--;
        }
    }


    // NOTE: Called from BehaviorTree

    // Dying
    public void Die() {
        if (isDead)
            return;

        isDead = true;

        transform.position -= transform.up;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x - 90f, transform.eulerAngles.y, transform.eulerAngles.z);

        canvas.enabled = false;
        
        isAtking = false;
        moveDir = Vector3.zero;

        StopAllCoroutines();

        dropSpawn.Drop(); 
    }


    // General movement
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


    // Facing player
    public void FaceAndStopRoam() {
        TurnWithoutTilt(targetedPlayer.transform.position);
        PauseRoam();
    }


    // Shop actions (speech bubble, items, hide,...)
    public void SetSpeechBubble(bool display) {
        topDisplay.SpeechBubble(display);
    }

    public void SetItems(bool display) {
        topDisplay.Items(display);
        showingItems = display;
    }

    public void Hide() {
        topDisplay.SpeechBubble(false);
        topDisplay.Items(false);
        showingItems = false;
    }



    // Attacking
    public void Attack() {
        if (!isAtking) {
            isAtking = true;
            moveDir = Vector3.zero;

            StartCoroutine(Fire());
        }
    }


    IEnumerator Fire() {
        Debug.Log($"Fire!");
        yield return new WaitForSeconds(atkDelay);

        topDisplay.SetHostile();

        while (true) {

            if (!isAtking)
                yield break;

            firePosSpinner.Fire(pfCBullet);

            yield return new WaitForSeconds(fireRate);
        }
    }
}
