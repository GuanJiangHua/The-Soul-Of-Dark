using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        Slider slider;
        float timeUntilBarIsHidden = 0;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
        }

        public void SetHealthBar(int health)
        {
            if (slider != null)
            {
                slider.value = health;
            }
            timeUntilBarIsHidden = 8;
        }

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        private void Update()
        {
            timeUntilBarIsHidden -= Time.deltaTime;

            if (slider != null)
            {
                slider.transform.forward = Camera.main.transform.forward;
                if (timeUntilBarIsHidden <= 0)
                {
                    timeUntilBarIsHidden = 0;
                    slider.gameObject.SetActive(false);
                }
                else
                {
                    if(slider.gameObject.activeInHierarchy == false)
                    {
                        slider.gameObject.SetActive(true);
                    }
                }

                if(slider.value <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
