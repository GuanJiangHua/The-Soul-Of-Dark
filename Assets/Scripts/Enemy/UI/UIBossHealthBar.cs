using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class UIBossHealthBar : MonoBehaviour
    {
        public Text bossName;
        public Slider slider;

        private void Awake()
        {
            bossName = GetComponentInChildren<Text>();
            slider = GetComponentInChildren<Slider>();
        }
        private void Start()
        {
            SetHealthBarToInactive();
        }

        //设置boss名称:
        public void SetBossName(string name)
        {
            bossName.text = name;
        }
        //启用boss血条:
        public void SetUIHealthBarToActive()
        {
            bossName.gameObject.SetActive(true);
            slider.gameObject.SetActive(true);
        }
        //禁用:
        public void SetHealthBarToInactive()
        {
            bossName.gameObject.SetActive(false);
            slider.gameObject.SetActive(false);
        }

        //设置boss血量最大值:
        public void SetBossMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        //设置当前血量:
        public void SetBossCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }
    }
}
