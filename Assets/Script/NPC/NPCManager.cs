using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOrWhich.NPC{
    public class NPCManager
    {
        private readonly NPCDB nPCDB;
        private readonly int totalNPC;
        public NPCManager(NPCDB nPCDB)
        {
            this.nPCDB = nPCDB;
            totalNPC = nPCDB.nPCs.Length;
        }

        public void DestroyAllNPC(){
            
        }
        private void ShuffleItem(){
            NPCController[] nPCController = new NPCController[totalNPC];
            Array.Copy(nPCDB.nPCs, nPCController,totalNPC);
        }
    }
}
