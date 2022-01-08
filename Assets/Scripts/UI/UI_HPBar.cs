using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour {
    [SerializeField] Slider hpSlider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    public void SetMaxHP(int maxHP) {
        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHP(int HP) {
        hpSlider.value = HP;

        fill.color = gradient.Evaluate(hpSlider.normalizedValue);
    }
}
