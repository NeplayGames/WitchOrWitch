using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WitchOrWhich.NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        public ERole role { get; private set; }
        public void AssignRole(ERole eRole){
            role = eRole;
        }
        void Start()
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }
        

    }

}
