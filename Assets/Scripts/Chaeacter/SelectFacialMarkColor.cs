using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFacialMarkColor : SelectColor
{
    protected override void Awake()
    {
        colorString = "_Color_BodyArt";
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
        colorString = "_Color_BodyArt";
        base.GetCurrentColor();
    }
}
