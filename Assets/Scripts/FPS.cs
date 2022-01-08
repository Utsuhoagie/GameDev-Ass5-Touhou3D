using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour {
    string label = "";
	float count;

    Vector2 position = new Vector2(Screen.width * 0.95f, Screen.height * 0.1f);
    Vector2 size = new Vector2(Screen.width, Screen.height) * 0.1f;


    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
	
	IEnumerator Start() {
		GUI.depth = 2;
		while (true) {
			if (Time.timeScale == 1) {
				yield return new WaitForSeconds (0.1f);
				count = (1 / Time.deltaTime);
				label = $"{Mathf.Round(count)}";
			} else {
				label = "Pause";
			}
			yield return new WaitForSeconds (0.5f);
		}
	}
	
	void OnGUI () {
        GUI.skin.label.fontSize = 32;
        GUI.contentColor = Color.black;
        GUI.backgroundColor = Color.white;

		GUI.Label (new Rect (position, size), label);
	}
}
