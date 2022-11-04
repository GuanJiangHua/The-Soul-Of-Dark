using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½����Ʒ�����Ʒ��Ϣ", menuName = "��Ϸ��Ʒ/�½����Ʒ�����Ʒ")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState , PlayerWeaponSlotManger weaponSlotManger , bool isLeftHanded)
        {
            base.AttemptToCastSpell(animatorHandler, playerState , weaponSlotManger , isLeftHanded);

            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true, false, isLeftHanded);
        }

        public override void SucessfullyCastSpell(AnimatorManager animatorManager, CharacterStatsManager playerState, PlayerWeaponSlotManger weaponSlotManger, CameraHandler cameraHandler , bool isLeftHanded)
        {
            base.SucessfullyCastSpell(animatorManager, playerState , weaponSlotManger,  cameraHandler , isLeftHanded);
            GameObject instaniatedSpellFX = Instantiate(spellCastFX, animatorManager.transform);

            playerState.HealThisTarget(healAmount);
        }
    }
}
