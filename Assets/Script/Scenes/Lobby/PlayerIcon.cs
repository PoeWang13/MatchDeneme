using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Mirror;

public class PlayerIcon : MonoBehaviour
{
    public Image myIcon;
    public Image myCerceve;
    public Image myFriend;
    public TextMeshProUGUI myName;
    public Button myButton;
    public Sprite dost;
    public Sprite dusman;
    public PlayerData myPlayer;
    public NetworkIdentity identity;
    public void SetPlayerFriend(PlayerData pd, NetworkIdentity id, bool isLocal = false)
    {
        myPlayer = pd;
        identity = id;
        myIcon.sprite = Object_Manager.Instance.playerIconSprites[pd.playerIcon];
        myCerceve.sprite = Object_Manager.Instance.playerCerceveSprites[pd.playerCerceve];
        myName.text = pd.playerName;
        if (isLocal)
        {
            myFriend.sprite = dost;
            myButton.interactable = false;
        }
        else
        {
            myFriend.sprite = dusman;
            myButton.interactable = true;
        }
    }
    public void ChancePlayerFriend()
    {
        if (LobbyCanvas_Manager.Instance.myFriendDatas.Contains(myPlayer))
        {
            LobbyCanvas_Manager.Instance.myFriendDatas.Remove(myPlayer);
            myFriend.sprite = dusman;
        }
        else
        {
            LobbyCanvas_Manager.Instance.myFriendDatas.Add(myPlayer);
            myFriend.sprite = dost;
        }
    }
}