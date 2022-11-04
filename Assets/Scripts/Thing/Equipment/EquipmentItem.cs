using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EquipmentItem : Item
    {
        [Header("·À¾ßÀàÐÍ:")]
        public bool isHelmet;
        public bool isTorso;
        public bool isLeg;
        public bool isHand;
        [Header("½éÉÜÎÄ±¾:")]
        [TextArea] public string equipmentIDescription;
        [Header("¹¦ÄÜÃèÊö:")]
        [TextArea] public string functionDescription = "ÓÃÓÚ·À»¤ÉíÇû¡£";
        [Header("·ÀÓù¼Ó³É:")]
        public float physicalDefense;       //ÎïÀí·ÀÓù¼Ó³É;
        public float fireDefense;               //»ðÑæÉËº¦¿¹ÐÔ;
        public float magicDefense;          //Ä§Á¦ÉËº¦¿¹ÐÔ;
        public float lightningDefense;     //À×µçÉËº¦¿¹ÐÔ;
        public float darkDefense;            //ºÚ°µÉËº¦¿¹ÐÔ;
        [Header("Òì³£ÊôÐÔµÖ¿¹Á¦:")]
        [Range(0,1)] public float poisonDefense;                 //¶¾ÊôÐÔµÖ¿¹Á¦;
        [Range(0, 1)] public float frostDefense;                    //º®ÀäÊôÐÔµÖ¿¹Á¦;
        [Range(0, 1)] public float hemorrhageDefense;       //³öÑªÊôÐÔµÖ¿¹Á¦;
        [Range(0, 1)] public float curseDefense;                  //³öÑªÊôÐÔµÖ¿¹Á¦;
    }
}
