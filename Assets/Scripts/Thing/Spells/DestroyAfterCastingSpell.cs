using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DestroyAfterCastingSpell : MonoBehaviour
    {
        CharacterManager chaeacterManager;

        private void Awake()
        {
            chaeacterManager = GetComponentInParent<CharacterManager>();
        }

        private void Update()
        {
            //法术释放完成，删除特效
            if (chaeacterManager.isFiringSpell == false)
            {
                Destroy(gameObject,5);
                chaeacterManager.isFiringSpell = false;
            }
        }
    }
}
