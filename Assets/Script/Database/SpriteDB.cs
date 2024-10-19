using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WitchOrWhich.NPC;
namespace WitchOrWhich.DataBase
{
    [CreateAssetMenu(fileName = "Sprite DB", menuName = "NPC/Sprite DB", order = 0)]
    public class SpriteDB : ScriptableObject
    {
        [SerializeField] private Sprite redNPCSprite;
        [SerializeField] private Sprite greenNPCSprite;
        [SerializeField] private Sprite blueNPCSprite;
        [SerializeField] private Sprite yellowNPCSprite;
        [SerializeField] private Sprite whiteNPCSprite;
        [SerializeField] private Sprite orangeNPCSprite;

        public Sprite GetPlayerSprite(EType eType){
            return eType switch{
                EType.Green => greenNPCSprite,
                EType.Blue => blueNPCSprite,
                EType.Yellow => yellowNPCSprite,
                EType.White => whiteNPCSprite,
                EType.Orange => orangeNPCSprite,
                _=> redNPCSprite,
            };
        }
    }

}
