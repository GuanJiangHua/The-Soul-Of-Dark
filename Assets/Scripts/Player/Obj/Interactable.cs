using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;                                                    //绘制的圆形的半径:
        public string InteractableText;                                            //交互文本(交互时弹出的ui文本信息);
        //选定时，绘制范围:
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        //可交互对象的交互功能(子类可重写):
        public virtual void Interact(PlayerManager playerManger)
        {
            Debug.Log("你与可交互对象发生了交互");
        }
    }
}
