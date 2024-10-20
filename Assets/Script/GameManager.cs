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

namespace WitchOrWhich{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private NPCDB nPCDB;
        [SerializeField] private SpriteDB spriteDB;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private PlayerInfoUI playerInfoUI;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject hidePanel;
        [SerializeField] private Info info;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<SelectRoleButton> selectRoleButtons = new();
        [SerializeField] private GameObject playerRoleSelectionPanel;
        private NPCManager nPCManager;
        void Start(){
            nPCManager = new NPCManager(nPCDB, spawnPoint, gameConfig);
            StartCoroutine(RemovePanel());
            playerInfoUI.Init(nPCManager, spriteDB);
            nPCManager.Init();
            
        }

        IEnumerator StartDangerTime(){
            yield return new WaitForSeconds(7);
            StartCoroutine(CoverNPC());
        }

         IEnumerator CoverNPC(){
            hidePanel.SetActive(true);
            yield return new WaitForSeconds(1);
            nPCManager.NPCKillPlayer();
            yield return new WaitForSeconds(1);
            nPCManager.NPCKillPlayer();
            yield return new WaitForSeconds(1);
            nPCManager.NPCKillPlayer();
            yield return new WaitForSeconds(2);
            hidePanel.SetActive(false);
            playerController.clickedPlayer += PlayerClicked;
        }
        private ERole eRole;
        private void PlayerClicked(bool arg1, ERole role)
        {
            if(arg1){
                info.SetInfoText("You caught the witch");
            }else{
                eRole = role;
                playerRoleSelectionPanel.SetActive(true);
                foreach(SelectRoleButton button in selectRoleButtons){
                    button.roleAssign += GuessRole;
                }

                playerController.clickedPlayer -= PlayerClicked;
                info.SetInfoText("Ohh no. Try to guess his role to survice. The witch will kill us all if you cannot say the role");
            }
        }

         IEnumerator Restart(){
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene(1); 
        }
        private void GuessRole(ERole role)
        {
             foreach(SelectRoleButton button in selectRoleButtons){
                    button.roleAssign -= GuessRole;
                }
            if(eRole == role){
                info.SetInfoText("Woo nice save. Now the witch has swapped all your roles and has got caught of one of you.");   
                StartCoroutine(Restart());
            }else{
               // SceneManager.LoadScene(2);     
            }
        }

        IEnumerator RemovePanel(){
            yield return new WaitForSeconds(4);
            playerInfoUI.gameObject.SetActive(false);
            StartCoroutine(StartDangerTime());
        }
    }
}

