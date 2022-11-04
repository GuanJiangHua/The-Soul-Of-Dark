using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkinColor : SelectColor
{
    public Material standardSkinColormaterial;  //��׼Ƥ����ɫ����
    protected override void Awake()
    {
        colorString = "_Color_Skin";
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

    //�ָ���׼Ƥ����ɫ:
    public void RestoreStandardSkinColor()
    {
        Color materialColor = standardSkinColormaterial.GetColor(colorString);
        redSlider.value = materialColor.r;
        greenSlider.value = materialColor.g;
        blueSlider.value = materialColor.b;
        SetColor();
    }

    public override void GetCurrentColor()
    {
        colorString = "_Color_Skin";
        base.GetCurrentColor();
    }
}
