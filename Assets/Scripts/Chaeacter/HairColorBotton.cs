using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairColorBotton : MonoBehaviour
{
    public SelectColor selectColor;
    [Header("��ɫ����:")]
    public float redAmount;       //��ɫ��
    public float greenAmount;   //��ɫ��
    public float blueAmount;     //��ɫ��

    [Header("��ɫ������:")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    [Header("��ɫͼ��:")]
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
