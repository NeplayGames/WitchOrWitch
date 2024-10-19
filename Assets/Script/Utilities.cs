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

        public static string GetRoledNPCJob(ERole eRole){
            return eRole switch{
                ERole.Engineer => "Engineer",
                ERole.Scientist => "Scientist",
                ERole.Police => "Police",
                ERole.Doctor => "Doctor",
                ERole.Jester => "Jester",
                ERole.lawyer => "Lawyer",
                _ => "",
            };
        }
    }
}
