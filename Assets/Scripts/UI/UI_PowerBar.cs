using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerBar : MonoBehaviour {
    [SerializeField] Slider pSlider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    public void SetRangeP(int minP, int maxP) {
        pSlider.maxValue = maxP;
        pSlider.minValue = minP;
        pSlider.value = minP;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetP(int P) {
        pSlider.value = (float) P;

        fill.color = gradient.Evaluate(pSlider.normalizedValue);
    }
}
