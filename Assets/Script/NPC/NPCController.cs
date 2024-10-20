using System.Collections;
using TMPro;
using UnityEngine;
using WitchOrWhich.Utils;

namespace WitchOrWhich.NPC
{
    public class NPCController : MonoBehaviour
    {
        [field:SerializeField] public EType eType {get; private set;}
        [field:SerializeField] public TextMeshPro textMesh{get; private set;}

        public ERole NPCRole { get; private set; }
        public void AssignRole(ERole eRole){
            NPCRole = eRole;
            textMesh.text = Utilities.GetRoledNPCJob(NPCRole);
            StartCoroutine(RemoveText());
        }

        public bool IsWitch { get; private set; }

        public void SetWitch(bool IsWitch){
            this.IsWitch = IsWitch;
        }

        public void Reset(){
            IsWitch = false;
        }
         IEnumerator RemoveText(){
            yield return new WaitForSeconds(4);
            textMesh.gameObject.SetActive(false);
        }

    }

}
