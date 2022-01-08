using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawn : MonoBehaviour {

    [SerializeField] GameObject pfItem;
    float spawnRange = 1f;

    Dictionary<Item.Type, int> drops = new Dictionary<Item.Type, int>();


    public void Init(string enemyName) {
        switch (enemyName) {
            case "Enemy":
                drops.Add(Item.Type.HP,    Utils.RandInt(0, 2));
                drops.Add(Item.Type.P,     Utils.RandInt(1, 3));
                drops.Add(Item.Type.Bomb,  Utils.RandInt(0, 1));
                drops.Add(Item.Type.Snipe, 0                  );
                break;
                
            case "Yoshika":
                drops.Add(Item.Type.HP,    Utils.RandInt(3, 5));
                drops.Add(Item.Type.P,     Utils.RandInt(3, 5));
                drops.Add(Item.Type.Bomb,  Utils.RandInt(1, 2));
                drops.Add(Item.Type.Snipe, 1                  );

                break;

            case "Chimata":
                drops.Add(Item.Type.HP,    Utils.RandInt(3, 5));
                drops.Add(Item.Type.P,     Utils.RandInt(3, 5));
                drops.Add(Item.Type.Bomb,  Utils.RandInt(1, 2));
                drops.Add(Item.Type.Snipe, 1                  );

                break;
        }
    }


    public void Drop() {
        foreach (var drop in drops) {
            Vector3 itemPos = Utils.RandomItemPos(transform.position, spawnRange);

            Item.Type itemType = drop.Key;
            int count = drop.Value;
            
            while (count > 0) {
                GameObject item = Instantiate(pfItem, itemPos, Quaternion.identity);
                item.GetComponent<Item>().Init(itemType);

                count--;
            }
        }
    }
}
