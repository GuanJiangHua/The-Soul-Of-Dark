using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建身体数据信息", menuName = "游戏身体数据/新建身体数据")]
    public class DodyModelData : ScriptableObject
    {
        [Header("是男性:")]
        public bool isMale = true;
        [Header("头部:")]
        public int headId;
        [Header("发型:")]
        public int hairstyle;
        [Header("面部毛发:")]
        public int facialHairId = -1;
        [Header("眉毛:")]
        public int eyebrow;
        [Header("毛发颜色:")]
        public Color hairColor;
        [Header("皮肤颜色:")]
        public Color skinColor;
        [Header("面部涂鸦颜色:")]
        public Color facialMarkColor;

        [Header("材质球属性:")]
        //毛发颜色属性:
        public string attributeHairColor = "_Color_Hair";
        //皮肤颜色属性:
        public string attributeSkinColor = "_Color_Skin";
        //面部涂鸦颜色:
        public string attributeFacialMarkColor = "_Color_BodyArt";
    }
}
