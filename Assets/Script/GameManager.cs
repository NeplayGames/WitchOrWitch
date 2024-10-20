using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchOrWhich.Configs;
using WitchOrWhich.DataBase;
using WitchOrWhich.NPC;
using WitchOrWhich.Player;
using WitchOrWhich.UI;
using WitchOrWhich.Utils;

namespace WitchOrWhich{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private NPCDB nPCDB;
        [SerializeField] private AudioSource killAudioSource;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private List<PumpkinWitchController> pumpkinWitchController;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject hidePanel;
        [SerializeField] private Info info;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<SelectRoleButton> selectRoleButtons = new();
        [SerializeField] private GameObject playerRoleSelectionPanel;
        private NPCManager nPCManager;
        void Start(){
            nPCManager = new NPCManager(nPCDB, spawnPoint, gameConfig, killAudioSource);
            StartCoroutine(RemovePanel());
            nPCManager.Init(); 
            foreach(var pWC in pumpkinWitchController)      
                pWC.Init(gameConfig); 
        }

        IEnumerator StartDangerTime(){
            yield return new WaitForSeconds(3);
            StartCoroutine(CoverNPC());
        }

         IEnumerator CoverNPC(){
            foreach(var pWC in pumpkinWitchController){
                pWC.canMove = true;
            }
            info.SetInfoText("Witch has possessed someone and is on kill rampage. To distract she has summon her friends as well.");
            yield return new WaitForSeconds(3);
            nPCManager.NPCKillPlayer();
            yield return new WaitForSeconds(3);
            nPCManager.NPCKillPlayer();
            yield return new WaitForSeconds(3);
            nPCManager.NPCKillPlayer();
            yield return new WaitForSeconds(2);
            info.SetInfoText("Select the character you think is possessed");
           // hidePanel.SetActive(false);
            playerController.clickedPlayer += PlayerClicked;
        }
        private ERole eRole;
        private void PlayerClicked(bool arg1, ERole role, EType eType)
        {
            if(arg1){
                info.SetInfoText("Nice observation. You caught the witch.");
                foreach(var pWC in pumpkinWitchController){
                    pWC.gameObject.SetActive(false);
                }
                 StartCoroutine(Restart(0));
            }else{
                eRole = role;
                playerRoleSelectionPanel.SetActive(true);
                foreach(SelectRoleButton button in selectRoleButtons){
                    button.roleAssign += GuessRole;
                }
                playerController.clickedPlayer -= PlayerClicked;
                info.SetInfoText($"Ohh no. Try to guess {Utilities.GetRoledNPCName(eType)} role to survice. The witch will kill us all if you cannot say the role");
            }
        }

         IEnumerator Restart(int scene){
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene(scene); 
        }
        private void GuessRole(ERole role)
        {
             foreach(SelectRoleButton button in selectRoleButtons){
                    button.roleAssign -= GuessRole;
                }
            if(eRole == role){
                hidePanel.SetActive(true);
                info.SetInfoText("Woo nice save. Now the witch has swapped all character roles and has possessed of one of you.");   
                StartCoroutine(Restart(1));
            }else{
                SceneManager.LoadScene(2);     
            }
        }

        IEnumerator RemovePanel(){
            yield return new WaitForSeconds(4);
            StartCoroutine(StartDangerTime());
        }

        void Update(){
            if(Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(0);
            }
        }
    }
}

