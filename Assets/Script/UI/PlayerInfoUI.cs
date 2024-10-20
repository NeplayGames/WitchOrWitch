using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WitchOrWhich.DataBase;
using WitchOrWhich.NPC;

namespace WitchOrWhich.UI
{
    public class PlayerInfoUI : MonoBehaviour
    {
        [SerializeField] private PlayerInfo[] playerInfos;

        private int currentCount = 0;

        NPCManager nPCManager;
        SpriteDB spriteDB;
        public void Init(NPCManager nPCManager, SpriteDB spriteDB){
            this.nPCManager = nPCManager;
            this.spriteDB = spriteDB;
            this.nPCManager.InitializedRoledNPC += SetInfo;
        }
        public void SetInfo(ERole eRole, EType eType)
        {  
            var playerInfo = playerInfos[currentCount];
            playerInfo.SetInfo(spriteDB.GetPlayerSprite(eType), eRole, eType);
            currentCount++;
        }

        void OnDestroy(){
            this.nPCManager.InitializedRoledNPC -= SetInfo;
        }
    }

}
