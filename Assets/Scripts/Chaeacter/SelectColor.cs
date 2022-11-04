using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectColor : MonoBehaviour
{
    [Header("���ĵ���ɫ����:")]
    protected string colorString = "_Color_Hair";
    [Header("��ɫ����:")]
    public float redAmount;       //��ɫ��
    public float greenAmount;   //��ɫ��
    public float blueAmount;     //��ɫ��
    public float alphaAmount;   //͸����

    protected Color currentHairColor;

    [Header("��ɫ������:")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    //������Ⱦ���б�:
    public Material material;

    protected virtual void Awake()
    {

    }
    //���»�����ʱ����:
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
