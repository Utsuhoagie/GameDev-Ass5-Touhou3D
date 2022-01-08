using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerShotManager : MonoBehaviour {
    // References
    [SerializeField] PlayerHP hpCtrl;
    [SerializeField] PlayerAnimations animCtrl;
    [SerializeField] UI_PowerBar pBar;
    [SerializeField] GameObject snipeIcon;
    [SerializeField] Transform firePos;

    // Prefabs
    [SerializeField] GameObject pfBullet;
    [SerializeField] GameObject pfBomb;

    // Player input class (C# generated class from Asset)
    PlayerInput baseInput;
    PlayerInput.PlayerActions input;


    // Atk stats
    int power = 0;
    int minPower = 0, maxPower = 100;
    int bombs = 2;
    bool snipe = false;

    bool isFiring = false;
    float shotDelay = 0.06f;
    float shotTimer = 0f;
    float shotRate = 0.2f;

    float bombTimer = 0f;
    float bombDelay = 3f;
    float bombGrowTime;

    // Events
    public delegate void BombEventDelegate(int bombCount = 0, float growTime = 0f);
    public event BombEventDelegate BombEvent;


    public void ChangePower(int deltaP) {
        power += deltaP;
        power = Mathf.Clamp(power, minPower, maxPower);

        pBar.SetP(power);
    }

    // ---- Lifetime ---------

    void Awake() {

        pBar.SetRangeP(minPower, maxPower);

        bombGrowTime = P1Bomb.GetGrowTime();
        BombEvent?.Invoke(bombs, bombGrowTime);


        baseInput = new PlayerInput();
        baseInput.Player.Enable();
        input = baseInput.Player;

        input.Bomb.performed += Bomb;
    }

    void Update() {
        isFiring = Mathf.Approximately(input.Fire.ReadValue<float>(), 1f);

        if (isFiring && Time.time >= shotTimer) {
            PlayAnimations("Fire", true);
            shotTimer = Time.time + shotRate;
            StartCoroutine(Fire());
        }
        else if (!isFiring) {
            PlayAnimations("Fire", false);
        }

    }

    // ------ Event handling ---------

    // --- Item collection ---
    void OnTriggerEnter(Collider col) {
        // NOTE: collides with either enemies, or powerups

        if (col.gameObject.tag != "Powerup")
            return;
        

        Item item = col.gameObject.GetComponent<Item>();

        switch(item.type) {
                
            case Item.Type.HP:
                hpCtrl.AddHP(item.HP);
                break;

            case Item.Type.P:
                ChangePower(item.P);
                break;

            case Item.Type.Bomb:
                bombs++;
                BombEvent?.Invoke(bombs);
                break;
            
            case Item.Type.Snipe:
                snipe = true;
                snipeIcon.GetComponent<Image>().enabled = true;
                break;
        }

        Destroy(col.gameObject);
    }



    // ------ Methods ----------

    IEnumerator Fire() {
        yield return new WaitForSeconds(shotDelay);

        Vector3 pPos    = transform.position;
        Vector3 pFwd    = transform.forward;
        Vector3 pLeft   = -transform.right;
        Vector3 pRight  = transform.right;
        Vector3 pUp     = transform.up;
        Vector3 pDown   = -transform.up;


        GameObject bullet;

        switch(power) {
            case int p when (minPower <= p && p < 40):
                
                bullet = Instantiate(pfBullet, firePos.position, Quaternion.identity);
                bullet.GetComponent<PBullet>().Init(pFwd, snipe);
                break;
                

            case int p when (40 <= p && p < 80):
                
                bullet = Instantiate(pfBullet, firePos.position + pLeft*0.1f, Quaternion.identity);
                bullet.GetComponent<PBullet>().Init(pFwd, snipe);

                bullet = Instantiate(pfBullet, firePos.position + pRight*0.1f, Quaternion.identity);
                bullet.GetComponent<PBullet>().Init(pFwd, snipe);
                break;

            case int p when (80 <= p && p <= maxPower):

                bullet = Instantiate(pfBullet, firePos.position + pUp*0.1f, Quaternion.identity);
                bullet.GetComponent<PBullet>().Init(pFwd, snipe);

                bullet = Instantiate(pfBullet, firePos.position + pLeft*0.1f + pDown*0.1f, Quaternion.identity);
                bullet.GetComponent<PBullet>().Init(pFwd, snipe);

                bullet = Instantiate(pfBullet, firePos.position + pRight*0.1f + pDown*0.1f, Quaternion.identity);
                bullet.GetComponent<PBullet>().Init(pFwd, snipe);

                break;
        }
    }

    void Bomb(InputAction.CallbackContext ctx) {
        if (bombs <= 0 || Time.time < bombTimer)
            return;

        PlayAnimations("Bomb", true);

        bombTimer = Time.time + bombDelay;        

        Vector3 bombPos = transform.position + transform.up*2.8f;

        GameObject bomb = Instantiate(pfBomb, bombPos, Quaternion.identity);
        P1Bomb bombComp = bomb.GetComponent<P1Bomb>();
        bombComp.Init(bombPos + transform.forward);


        bombs--;
        BombEvent?.Invoke(bombs, bombGrowTime);
        StartCoroutine(BombDelay(bombGrowTime));
    }
    IEnumerator BombDelay(float growTime) {
        input.Disable();
        yield return new WaitForSeconds(growTime);
        input.Enable();
    }
    
    
    // --- Animations ----------

    void PlayAnimations(string action, bool isDoing) {
        if (action == "Fire") {
            if (isDoing)
                animCtrl.StartFiring();
            else
                animCtrl.StopFiring();
        }

        else if (action == "Bomb")
            animCtrl.StartBombing();
    }

}


