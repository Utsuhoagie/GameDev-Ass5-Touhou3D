using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum Type {
        HP,
        Bomb,
        P,
        Snipe
    }
    public Type type;

    public int HP { get; set; } = 2;
    public int P { get; set; } = 10;

    
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Texture tex_HP;
    [SerializeField] Texture tex_P;
    [SerializeField] Texture tex_Bomb;
    [SerializeField] Texture tex_Snipe;

    [SerializeField] float spinRate;



    void SetType(Type t) {
        type = t;
        switch(type) {
            case Type.HP:
                mesh.material.SetTexture("_MainTex", tex_HP);
                break;
            case Type.P:
                mesh.material.SetTexture("_MainTex", tex_P);
                break;
            case Type.Bomb:
                mesh.material.SetTexture("_MainTex", tex_Bomb);
                break;
            case Type.Snipe:
                mesh.material.SetTexture("_MainTex", tex_Snipe);
                break;
        }
    }


    void Awake() {
        SetType(type);
        StartCoroutine(Spin());
    }

    public void Init(Type type) {
        SetType(type);
    }


    IEnumerator Spin() {
        while (true) {
            yield return new WaitForSeconds(spinRate);
            transform.Rotate(axis: Vector3.up, angle: 3f);
        }
    }
}
