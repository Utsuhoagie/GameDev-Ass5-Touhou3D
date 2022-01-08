using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BombCounter : MonoBehaviour {

    [SerializeField] Text textCounter;
    [SerializeField] PlayerShotManager shotCtrl;


    // -------- Methods ---------

    void Awake() {
        shotCtrl.BombEvent += UpdateCount;
    }

    void UpdateCount(int bombCount, float growTime) {
        textCounter.text = $"x {bombCount}";
    }
}
