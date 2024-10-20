using System;
using Unity.VisualScripting;
using UnityEngine;
using WitchOrWhich.NPC;

namespace WitchOrWhich.Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action<bool, ERole> clickedPlayer;
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent<NPCController>(out NPCController controller))
                    {
                        clickedPlayer?.Invoke(controller.IsWitch, controller.NPCRole);
                    }
                }
            }
        }
    }

}
