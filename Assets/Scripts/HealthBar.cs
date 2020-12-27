﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Gradient gradient;

    public void SetMaxValue(float max)
    {
        slider.maxValue = max;
        slider.value = max;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float curHealth)
    {
        slider.value = curHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}