using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHairColor : SelectColor
{
    protected override void Awake()
    {
        colorString = "_Color_Hair";
        base.Awake();
    }
    private void OnEnable()
    {
        if (material != null)
        {
            Color materialColor = material.GetColor(colorString);
            redSlider.value = materialColor.r;
            greenSlider.value = materialColor.g;
            blueSlider.value = materialColor.b;
        }
    }
    public override void GetCurrentColor()
    {
        colorString = "_Color_Hair";
        base.GetCurrentColor();
    }
}
