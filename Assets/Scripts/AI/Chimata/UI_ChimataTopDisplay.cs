using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChimataTopDisplay : MonoBehaviour {
    [SerializeField] GameObject hpBar;

    [SerializeField] GameObject speechBubble;
    
    [SerializeField] GameObject itemContainer;
    [SerializeField] GameObject item_HP;
    [SerializeField] GameObject item_P;
    [SerializeField] GameObject item_Bomb;
    [SerializeField] GameObject item_Snipe;

    void Awake() {
        speechBubble.SetActive(false);
        itemContainer.SetActive(false);
        hpBar.SetActive(false);
    }

    public void SpeechBubble(bool display) {
        if (display)
            speechBubble.SetActive(true);
        else
            speechBubble.SetActive(false);
    }

    public void Items(bool display) {
        if (display)
            itemContainer.SetActive(true);
        else
            itemContainer.SetActive(false);
    }

    public void Item(Item.Type itemType, bool display) {
        switch (itemType) {
            case global::Item.Type.HP:
                item_HP.SetActive(display);
                break;
            case global::Item.Type.P:
                item_P.SetActive(display);
                break;
            case global::Item.Type.Bomb:
                item_Bomb.SetActive(display);
                break;
            case global::Item.Type.Snipe:
                item_Snipe.SetActive(display);
                break;
        }
    }


    public void SetHostile() {
        speechBubble.SetActive(false);
        itemContainer.SetActive(false);

        hpBar.SetActive(true);
    }
}
