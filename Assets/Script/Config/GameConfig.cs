using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOrWhich.Configs
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Configs/Game Config", order = 0)]

    public class GameConfig : ScriptableObject
    {
        [field:SerializeField]public int xRange {get; private set;}= 24; 
        [field:SerializeField]public int yRange {get; private set;}= 12; 
        [field:SerializeField]public int minGenericNPC {get; private set;}= 35; 
        [field:SerializeField]public int maxGenericNPC {get; private set;}= 45; 
    }
}
