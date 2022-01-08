using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    public static int RandInt(int min, int max) {
        return Random.Range(min, max+1);
    }


    public static Vector3 RandomPos(Vector3 currentPos, float delta) {
        Vector3 newPos = currentPos;
        
        newPos.x += Random.Range(-delta, delta);
        //newPos.y = Random.Range(currentPos.y - delta, currentPos.y + delta);
        newPos.z += Random.Range(-delta, delta);

        return newPos;
    }

    public static Vector3 RandomItemPos(Vector3 currentPos, float delta) {
        Vector3 newPos = currentPos;

        newPos.x += Random.Range(-delta, delta);
        newPos.y += Random.Range(-delta, delta);
        newPos.z += Random.Range(-delta, delta);

        return newPos;
    }

    public static Color RandomColor() {
        Color orange = new Color32(255, 174, 0, 255);
        Color purple = new Color32(204, 0, 255, 255);
        Color indigo = new Color32(75, 0, 135, 255);
        Color[] colors = { Color.red, orange, Color.yellow, Color.green, Color.cyan, Color.blue, indigo, purple, Color.white };

        int colorIndex = Random.Range(0, colors.Length);

        return colors[colorIndex];
    }
}