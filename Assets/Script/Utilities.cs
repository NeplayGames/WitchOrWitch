using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WitchOrWhich.NPC;

namespace WitchOrWhich.Utils
{
    public static class Utilities
    {
        public static string GetRoledNPCName(EType eType){
            return eType switch{
                EType.Red => "Red",
                EType.Green => "Green",
                EType.Blue => "Blue",
                EType.Yellow => "Yellow",
                EType.Orange => "Orange",
                EType.White => "White",
                _ => "",
            };
        }
    }
}
