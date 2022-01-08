using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShopInteraction : MonoBehaviour {
    AI_Chimata chimata;

    // Player input class (C# generated class from Asset)
    PlayerInput baseInput;
    PlayerInput.PlayerActions input;


    void Awake() {
        SetupInput();
    }

    void SetupInput() {
        baseInput = new PlayerInput();
        baseInput.Player.Enable();
        input = baseInput.Player;

        input.Talk.performed += Talk;

        input.Item_HP.performed    += BuyItem;
        input.Item_P.performed     += BuyItem;
        input.Item_Bomb.performed  += BuyItem;
        input.Item_Snipe.performed += BuyItem;
    }



    public void SetChimataTalkable(AI_Chimata chimata, bool talkable) {
        this.chimata = talkable? chimata : null;
    }

    void Talk(InputAction.CallbackContext ctx) {
        chimata?.SetBuying();
    }

    void BuyItem(InputAction.CallbackContext ctx) {
        if (chimata == null)
            return;

        string itemName = ctx.action.name;
        Item.Type itemType;
        
        switch (itemName) {
            case "Item_HP"   : itemType = Item.Type.HP;     break;
            case "Item_P"    : itemType = Item.Type.P;      break;
            case "Item_Bomb" : itemType = Item.Type.Bomb;   break;
            case "Item_Snipe": itemType = Item.Type.Snipe;  break;

            // NOTE: NOT ACTUALLY USED, JUST TO BYPASS COMPILE ERROR
            default: itemType = Item.Type.HP; break;
        }
        chimata.BuyItem(itemType);
    }
}