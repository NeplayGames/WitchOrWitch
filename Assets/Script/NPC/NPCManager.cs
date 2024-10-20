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

        public event Action<ERole, EType> InitializedRoledNPC;
        List<Transform> InstantiateGenericPlayers = new();
        List<Vector3> instantiatePositions = new();
        private Transform witchNPC;
        private AudioSource killAudioSource;
        public NPCManager(NPCDB nPCDB,Transform spawnPointParent, GameConfig gameConfig, AudioSource audioSource)
        {
            this.nPCDB = nPCDB;
            totalRoledNPC = nPCDB.roledNPC.Length;
            this.spawnPointParent = spawnPointParent;
            this.gameConfig = gameConfig;
            this.killAudioSource = audioSource;
        }

        public void Init(){
             InitializeRoledNPC();
            InitializeGenericNPC();
        }

        public void NPCKillPlayer(){     
            int range = random.Next(1,3);
            Debug.Log(range);

            while(range > 0){
                KillNearestNPC(witchNPC.transform.position);
                range--;
            } 
        }

        private void InitializeGenericNPC()
        {
            int total = random.Next(gameConfig.minGenericNPC, gameConfig.maxGenericNPC);

            while(total > 0){
                Vector3 pos = new Vector3(random.Next(-gameConfig.xRange, gameConfig.xRange),
                 random.Next(-gameConfig.yRange,gameConfig.yRange));
                 if(instantiatePositions.Contains(pos)){
                    continue;
                 }
                 instantiatePositions.Add(pos);
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
                InitializedRoledNPC?.Invoke(characterRole, currentNPCController.eType);
                if(n == witchIndex)
                    witchNPC = currentNPCController.transform;
                enums.Remove(enums[enumTemp]);
            }        
        }

        public void KillNearestNPC(Vector2 Pos){
            killAudioSource.Play();
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
