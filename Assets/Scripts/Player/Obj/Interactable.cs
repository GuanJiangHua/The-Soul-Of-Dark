using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;                                                    //���Ƶ�Բ�εİ뾶:
        public string InteractableText;                                            //�����ı�(����ʱ������ui�ı���Ϣ);
        //ѡ��ʱ�����Ʒ�Χ:
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        //�ɽ�������Ľ�������(�������д):
        public virtual void Interact(PlayerManager playerManger)
        {
            Debug.Log("����ɽ����������˽���");
        }
    }
}
