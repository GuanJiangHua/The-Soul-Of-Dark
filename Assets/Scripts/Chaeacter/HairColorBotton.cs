using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairColorBotton : MonoBehaviour
{
    public SelectColor selectColor;
    [Header("颜色参数:")]
    public float redAmount;       //红色量
    public float greenAmount;   //绿色量
    public float blueAmount;     //蓝色量

    [Header("颜色滑动条:")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    [Header("颜色图标:")]
    public Image colorImage;

    private void Awake()
    {
        redAmount = colorImage.color.r;
        greenAmount = colorImage.color.g;
        blueAmount = colorImage.color.b;
    }
    public void SetSliderValuesToImageColor()
    {
        selectColor.redAmount = redAmount;
        selectColor.greenAmount = greenAmount;
        selectColor.blueAmount = blueAmount;

        redSlider.value = redAmount;
        greenSlider.value = greenAmount;
        blueSlider.value = blueAmount;

        selectColor.SetColor();
    }
}
