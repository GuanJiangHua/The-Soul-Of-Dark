using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;
        private float timer = 0.3f;

        private void OnTriggerEnter(Collider other)
        {
            PlayerStatsManager playerState = other.GetComponent<PlayerStatsManager>();
            if (playerState != null)
            {
                print("接触火");
                timer = 0.3f;
            }
        }
        //玩家再火中:
        private void OnTriggerStay(Collider other)
        {
            PlayerStatsManager playerState = other.GetComponent<PlayerStatsManager>();
            if(playerState != null)
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    playerState.TakeDamage(0 , damage , 0 , 0 , 0);
                    timer = 0.8f;
                }
            }
        }
    }

}
