using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectColor : MonoBehaviour
{
    [Header("更改的颜色参数:")]
    protected string colorString = "_Color_Hair";
    [Header("颜色参数:")]
    public float redAmount;       //红色量
    public float greenAmount;   //绿色量
    public float blueAmount;     //蓝色量
    public float alphaAmount;   //透明度

    protected Color currentHairColor;

    [Header("颜色滑动条:")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    //表面渲染器列表:
    public Material material;

    protected virtual void Awake()
    {

    }
    //更新滑动条时调用:
    public void UpdateSliders()
    {
        redAmount = redSlider.value;
        greenAmount = greenSlider.value;
        blueAmount = blueSlider.value;
        SetColor();
    }
    public virtual void SetColor()
    {
        currentHairColor = new Color(redAmount, greenAmount, blueAmount, alphaAmount);

        if (material != null)
        {
            material.SetColor(colorString, currentHairColor);
        }
    }

    public virtual void GetCurrentColor()
    {
        Color originalColor = material.GetColor(colorString);
        redAmount = originalColor.r;
        greenAmount = originalColor.g;
        blueAmount = originalColor.b;
    }
}
