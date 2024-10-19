using UnityEngine;

namespace WitchOrWhich.NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private EType eType;

        public ERole NPCRole { get; private set; }
        public void AssignRole(ERole eRole){
            NPCRole = eRole;
        }

        public bool IsWitch { get; private set; }

        public void SetWitch(bool IsWitch){
            this.IsWitch = IsWitch;
        }

        public void Reset(){
            IsWitch = false;
        }

    }

}
