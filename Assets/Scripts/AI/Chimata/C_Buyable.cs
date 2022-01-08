using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Buyable : MonoBehaviour {
    [SerializeField] AI_Chimata chimata;
    PlayerShopInteraction pShopInter;

    void OnTriggerEnter(Collider col) {
        chimata.SetBuyable();

        pShopInter = col.GetComponent<PlayerShopInteraction>();
        pShopInter.SetChimataTalkable(chimata, true);
    }

    void OnTriggerExit(Collider col) {
        chimata.UnsetBuyable();
        chimata.UnsetBuying();

        pShopInter = col.GetComponent<PlayerShopInteraction>();
        pShopInter.SetChimataTalkable(chimata, false);
    }
}
