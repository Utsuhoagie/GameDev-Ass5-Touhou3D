using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_1_Spawner : MonoBehaviour {

    [SerializeField] GameObject pfAI_1;
    [SerializeField] float spawnRate;

    void Start() {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(3f);

        while (true) {
            Instantiate(pfAI_1, transform.position + transform.forward*0.5f, transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
