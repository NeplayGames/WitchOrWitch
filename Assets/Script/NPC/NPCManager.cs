using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WitchOrWhich.Configs;

namespace WitchOrWhich.NPC
{
    public class NPCManager
    {
        System.Random random = new System.Random();
        private readonly NPCDB nPCDB;
        private readonly GameConfig gameConfig;
        private readonly int totalRoledNPC;
        private readonly Transform spawnPointParent;

        public event Action InitializedRoledNPC;
        List<Transform> InstantiateGenericPlayers = new();
        private Transform witchNPC;
        public NPCManager(NPCDB nPCDB,Transform spawnPointParent, GameConfig gameConfig)
        {
            this.nPCDB = nPCDB;
            totalRoledNPC = nPCDB.roledNPC.Length;
            this.spawnPointParent = spawnPointParent;
            this.gameConfig = gameConfig;
            InitializeRoledNPC();
            InitializeGenericNPC();
        }

        public void DangerTimeInitiated(){
            int npcToKill = random.Next(3,6);
            while(npcToKill > 0){
                KillNearestNPC(witchNPC.transform.position);
                npcToKill--;
            }
        }

        private void InitializeGenericNPC()
        {
            int total = random.Next(gameConfig.minGenericNPC, gameConfig.maxGenericNPC);

            while(total > 0){
                Vector3 pos = new Vector3(random.Next(-gameConfig.xRange, gameConfig.xRange),
                 random.Next(-gameConfig.yRange,gameConfig.yRange));
                GameObject genericNPC = GameObject.Instantiate(nPCDB.genericNPC[random.Next(0, nPCDB.genericNPC.Length)], 
                pos,Quaternion.identity);
                InstantiateGenericPlayers.Add(genericNPC.transform);
                total--;
            }
        }

        public void InitializeRoledNPC()
        {
            GameObject[] nPCController = new GameObject[totalRoledNPC];
            ShuffleNPC(ref nPCController);
            List<int> enums = new List<int>() { 0, 1, 2, 3, 4, 5 };
            int witchIndex = random.Next(0, totalRoledNPC);
            int n = totalRoledNPC;
            while (n > 0)
            {
                int enumTemp = random.Next(n--);
                ERole characterRole = (ERole)enums[enumTemp];
                Transform pos = spawnPointParent.GetChild(n);
                NPCController currentNPCController = GameObject.Instantiate(nPCController[n], pos.position, pos.rotation).GetComponent<NPCController>();
                currentNPCController.AssignRole(characterRole);
                currentNPCController.SetWitch(n == witchIndex);
                if(n == witchIndex)
                    witchNPC = currentNPCController.transform;
                enums.Remove(enums[enumTemp]);
            }        
        }

        public void KillNearestNPC(Vector2 Pos){
            float minLength = Mathf.Infinity;
            int index = 0;
            for(int i =0; i < InstantiateGenericPlayers.Count; i++){
                float dist = Vector2.Distance(Pos, InstantiateGenericPlayers[i].position);
                if(dist < minLength){
                    minLength = dist;
                    index = i;
                }
            }
            GameObject.Destroy(InstantiateGenericPlayers[index].gameObject);
            InstantiateGenericPlayers.RemoveAt(index);
        }

        public void DestroyAllNPC()
        {
        }

        private void ShuffleNPC(ref GameObject[] nPCController)
        {
            Array.Copy(nPCDB.roledNPC, nPCController, totalRoledNPC);
            int n = totalRoledNPC;
            while (n > 0)
            {
                int k = random.Next(n--);
                GameObject temp = nPCController[n];
                nPCController[n] = nPCController[k];
                nPCController[k] = temp;
            }
        }
    }
}
