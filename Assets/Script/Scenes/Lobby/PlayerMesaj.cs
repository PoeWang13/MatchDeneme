using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMesaj : MonoBehaviour
{
    public Image myIcon;
    public Image myCerceve;
    public TextMeshProUGUI myName;
    public TextMeshProUGUI myMessage;
    public PlayerData myPlayer;
    public void SetPlayerMessage(PlayerData pd)
    {
        myPlayer = pd;
        myIcon.sprite = Object_Manager.Instance.playerIconSprites[pd.playerIcon];
        myCerceve.sprite = Object_Manager.Instance.playerCerceveSprites[pd.playerCerceve];
        myName.text = pd.playerName;
    }
    public void SendPlayerMessage(PlayerData datas, string mesaj)
    {
        SetPlayerMessage(datas);
        myMessage.text = mesaj;
    }
}