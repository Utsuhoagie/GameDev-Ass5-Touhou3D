using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour {

    [SerializeField] PlayerShotManager pShot;
    [SerializeField] UI_HPBar hpBar;
    [SerializeField] SkinnedMeshRenderer skinnedMesh;

    int HP = 50;
    int maxHP = 50;
    float takeDamageTimer = 0.033f;


    void Awake() {
        hpBar.SetMaxHP(HP);
    }

    void Update() {
        if (HP <= 0) {
            //Destroy(gameObject);
            SceneManager.LoadScene("SampleScene");
        }
    }


    public void AddHP(int HP) {
        this.HP += HP;

        this.HP = Mathf.Clamp(this.HP, 0, maxHP);

        hpBar.SetHP(this.HP);
    }


    void OnTriggerEnter(Collider col) {
        string tag = col.gameObject.tag;

        switch (tag) {
            case "EBullet": HP -= col.GetComponent<EBullet>().damage;   break;
            case "YBullet": HP -= col.GetComponent<YBullet>().damage;   break;
            case "CBullet": HP -= col.GetComponent<CBullet>().damage;   break;

            default: return;
        }

        hpBar.SetHP(HP);
        pShot.ChangePower(-2);
            
        StartCoroutine(TakeDamageAnim());    
    }
    
    IEnumerator TakeDamageAnim() {
        skinnedMesh.enabled = false;

        yield return new WaitForSeconds(takeDamageTimer);

        skinnedMesh.enabled = true;
    }
}