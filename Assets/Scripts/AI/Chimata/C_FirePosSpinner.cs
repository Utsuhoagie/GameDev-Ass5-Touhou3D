using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FirePosSpinner : MonoBehaviour {
    bool isActive = false;
    float yAngle = 0f, yAngleDelta = 3.5f;
    float yAngleMax = 45f;

    // Update is called once per frame
    void Update() {
        if (isActive) {
            if (yAngle >= 360f) {
                yAngle = 0f;
                transform.eulerAngles = new Vector3(Random.Range(-yAngleMax, yAngleMax), 0f, 0f);
            }

            // yAngle += (yAngle < 90f || 270f < yAngle)? 5f : 10f;
            // yRad = yAngle * Mathf.Deg2Rad;

            yAngle += yAngleDelta;

            //transform.Rotate(Mathf.Sin(yRad) * 1.2f, 1.5f, 0f);
            transform.Rotate(0f, yAngleDelta, 0f);
        }
    }


    public void Fire(GameObject pfBullet) {
        isActive = true;

        foreach (Transform childT in transform) {
            GameObject bullet = Instantiate(pfBullet, childT.position, Quaternion.identity);
            bullet.GetComponent<CBullet>().Init(childT.forward);
        }
    }
}
