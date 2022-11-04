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

        //����boss����:
        public void SetBossName(string name)
        {
            bossName.text = name;
        }
        //����bossѪ��:
        public void SetUIHealthBarToActive()
        {
            bossName.gameObject.SetActive(true);
            slider.gameObject.SetActive(true);
        }
        //����:
        public void SetHealthBarToInactive()
        {
            bossName.gameObject.SetActive(false);
            slider.gameObject.SetActive(false);
        }

        //����bossѪ�����ֵ:
        public void SetBossMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        //���õ�ǰѪ��:
        public void SetBossCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }
    }
}
