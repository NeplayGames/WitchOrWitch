using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WitchOrWhich.NPC;

namespace WitchOrWhich.UI
{
    public class SelectRoleButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private ERole eRole;

        public event Action<ERole> roleAssign;

        void Start(){
            button.onClick.AddListener(ButtonClick);
        }

        private void ButtonClick()
        {
            Debug.Log(eRole);
            roleAssign?.Invoke(eRole);
        }

        void OnDestroy(){
            button.onClick.RemoveListener(ButtonClick);
        }
    }

}
