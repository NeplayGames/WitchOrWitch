using System.Collections;
using UnityEngine;
using WitchOrWhich.Configs;
using WitchOrWhich.DataBase;
using WitchOrWhich.NPC;

namespace WitchOrWhich{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private NPCDB nPCDB;
        [SerializeField] private SpriteDB spriteDB;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private Transform spawnPoint;
        private NPCManager nPCManager;
        void Start(){
            nPCManager = new NPCManager(nPCDB, spawnPoint, gameConfig);
            StartCoroutine(StartDangerTime());
        }

        IEnumerator StartDangerTime(){
            yield return new WaitForSeconds(7);
            nPCManager.DangerTimeInitiated();
        }
    
    }
}

