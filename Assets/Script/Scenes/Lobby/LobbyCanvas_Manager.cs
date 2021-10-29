using TMPro;
using Mirror;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LobbyCanvas_Manager : MonoSingletion<LobbyCanvas_Manager>
{
    public PlayerIcon playerIcon;
    public Transform playerIconParent;
    [Space]
    public PlayerMesaj playerMesaj;
    public Transform playerMesajParent;
    public RectTransform playerMesajRectTransform;
    public TMP_InputField mesajInput;
    public TextMeshProUGUI newMesajText;
    private int newMesaj;
    [Space]
    public List<PlayerData> myFriendDatas = new List<PlayerData>();
    private int matchBeginTime = 90;
    private float matchLimitTimeNext;
    private bool canBegin = false;
    private bool canLastTime = false;
    public TextMeshProUGUI beginingTimeText;
    public Animator animator;
    public NetworkManager networkManager;
    public List<Player> players = new List<Player>();
    public override void GetSomeTing()
    {
        Player.OnMessage += OnPlayerMessage;
        networkManager = GameObject.FindGameObjectWithTag("NetWorkManager").GetComponent<NetworkManager>();
    }
    public void OpenClosePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    public void GotoMenu()
    {
        Player.OnMessage -= OnPlayerMessage;
        networkManager.StopClient();
        SceneManager.LoadScene("Menu");
    }
    private void Update()
    {
        if (canBegin)
        {
            matchLimitTimeNext += Time.deltaTime;
            if (matchLimitTimeNext >= 1)
            {
                matchBeginTime -= 1;
                matchLimitTimeNext = 0;
                UpdateBeginingTimeText(matchBeginTime);
            }
        }
        if (canLastTime)
        {
            matchLimitTimeNext += Time.deltaTime;
            if (matchLimitTimeNext >= 1)
            {
                matchBeginTime -= 1;
                matchLimitTimeNext = 0;
                UpdateBeginingTimeText(matchBeginTime);
            }
        }
    }
    public void StartBeginTime(int lastTime)
    {
        canBegin = true;
        matchBeginTime = lastTime;
    }
    public void UpdateBeginingTimeText(int lastTime)
    {
        if (canLastTime)
        {
            beginingTimeText.text = "Begining Game : " + lastTime.ToString("0");
            if (lastTime == 0)
            {
                canLastTime = false;
                SceneManager.LoadScene("Game");
            }
        }
        if (canBegin)
        {
            beginingTimeText.text = "Begining Match : " + lastTime.ToString("0");
            if (lastTime == 0)
            {
                canBegin = false;
                canLastTime = true;
                matchBeginTime = 30;
            }
        }
        animator.Play("BeginingTime");
    }
    public void QuitGame()
    {
        Player.Instance.ClientQuit();
    }
    public void AddPlayerIcons(PlayerData playerData, NetworkIdentity identity)
    {
        PlayerIcon icon = Instantiate(playerIcon, playerIconParent);
        icon.SetPlayerFriend(playerData, identity, identity == Player.Instance.identity);
    }
    public void AddBeforePlayerIcons(NetworkIdentity identity)
    {
        for (int i = 0; i < players.Count; i++)
        {
            NetworkIdentity id = players[i].GetComponent<NetworkIdentity>();
            if (id != identity)
            {
                PlayerIcon icon = Instantiate(playerIcon, playerIconParent);
                icon.SetPlayerFriend(players[i].myData, id, players[i].GetComponent<NetworkIdentity>() == Player.Instance.identity);
            }
        }
        // Player ilk sıraya gelmişti ama oyuna son giren olduğu için en sona at
        playerIconParent.GetChild(0).SetAsLastSibling();
    }
    public void RemovePlayerIcon(NetworkIdentity identity)
    {
        for (int e = 0; e < playerIconParent.childCount; e++)
        {
            if (playerIconParent.GetChild(e).GetComponent<PlayerIcon>().identity == identity)
            {
                Destroy(playerIconParent.GetChild(e).gameObject);
            }
        }
    }

    #region Chat
    public void OnPlayerMessage(PlayerData datas, string message)
    {
        AppendMessage(datas, message);
    }

    internal void AppendMessage(PlayerData datas, string message)
    {
        StartCoroutine(AppendAndScroll(datas, message));
    }
    IEnumerator AppendAndScroll(PlayerData datas, string message)
    {
        PlayerMesaj mesajer = Instantiate(playerMesaj, playerMesajParent);
        mesajer.SendPlayerMessage(datas, message);
        if (playerMesajParent.childCount > 50)
        {
            Destroy(playerMesajParent.GetChild(0).gameObject);
        }
        if (!playerMesajParent.parent.parent.gameObject.activeSelf)
        {
            SetNewMesajAmount(true);
        }
        playerMesajRectTransform.anchoredPosition = new Vector2(0, 0);
        // it takes 2 frames for the UI to update ?!?!
        yield return null;
        yield return null;
    }
    public void OpenChatContainer(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
        SetNewMesajAmount(false);
    }
    private void SetNewMesajAmount(bool isNewMesaj)
    {
        if (isNewMesaj)
        {
            newMesaj++;
        }
        else
        {
            newMesaj = 0;
        }
        newMesajText.text = newMesaj.ToString();
    }
    public void OnSend()
    {
        if (mesajInput.text.Trim() == "")
            return;
        // send a message
        Player.Instance.CmdSend(mesajInput.text.Trim(), Player.Instance.myData);

        mesajInput.text = "";
    }
    #endregion
}