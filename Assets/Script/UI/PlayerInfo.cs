using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WitchOrWhich.NPC;
using WitchOrWhich.Utils;

namespace WitchOrWhich.UI
{
    public class PlayerInfo : MonoBehaviour
    {
       [SerializeField] private Image playerImage;
       [SerializeField] private TextMeshProUGUI playerRoleText;
       [SerializeField] private TextMeshProUGUI playerNameText;

       public void SetInfo(Sprite playerImage, ERole eRole, EType eType){
        this.playerImage.sprite = playerImage;
        playerRoleText.text = Utilities.GetRoledNPCName(eType);
        playerNameText.text = Utilities.GetRoledNPCJob(eRole);
       }
    }

}
