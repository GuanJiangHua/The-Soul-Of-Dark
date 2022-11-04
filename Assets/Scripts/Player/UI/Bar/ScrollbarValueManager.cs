using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarValueManager : MonoBehaviour
{
    public float speed;
    public Scrollbar scrollbar;

    bool isAdd = false;
    bool isReduce = false;
    private void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    private void Update()
    {
        if (isAdd)
        {
            float newValue = scrollbar.value;
            newValue += speed * Time.deltaTime;
            newValue = Mathf.Clamp01(newValue);
            scrollbar.value = newValue;
        }
        else if(isReduce)
        {
            float newValue = scrollbar.value;
            newValue -= speed * Time.deltaTime;
            newValue = Mathf.Clamp01(newValue);
            scrollbar.value = newValue;
        }
    }

    public void ValueIncrease(bool isAdd)
    {
        this.isAdd = isAdd;
    }
    public void ValueDecrease(bool isReduce)
    {
        this.isReduce = isReduce;
    }
}
