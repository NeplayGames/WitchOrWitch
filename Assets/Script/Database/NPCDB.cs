using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOrWhich.NPC
{
    [CreateAssetMenu(fileName = "NPC DB", menuName = "NPC/Database", order = 0)]
    public class NPCDB : ScriptableObject
    {
        [field:SerializeField] public NPCController[] nPCs {get; private set;}

    }

}
