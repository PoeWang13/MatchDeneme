using Mirror;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    #region Degiskenler
    public static Player Instance;
    [Header("SyncVar olanlar")]
    [SyncVar(hook = nameof(DataUpdate))] public PlayerData myData;
    [SyncVar] public int playerNumber = -1;
    [SyncVar] public int matchNumber = -1;
    [SyncVar] public Guid matchGuidId;
    [SyncVar] public Match myMatch;

    [Header("SyncVar olmayanlar")]
    public StringVariables playerName;
    public IntVariables playerIcon;
    public IntVariables playerCerceve;
    private NetworkMatchChecker networkMatchChecker;
    public NetworkIdentity identity;
    private void Awake()
    {
        networkMatchChecker = GetComponent<NetworkMatchChecker>();
    }
    private void DataUpdate(PlayerData oldData, PlayerData newData)
    {
        gameObject.name = newData.playerName;
    }
    #endregion

    #region Oyuna Giris
    public override void OnStartClient()
    {
        if (isClient)
        {
            LobbyCanvas_Manager.Instance.players.Add(this);
        }
        if (isLocalPlayer)
        {
            Instance = this;
            identity = GetComponent<NetworkIdentity>();
            myData = new PlayerData(playerName.value, playerIcon.value, playerCerceve.value);
            gameObject.name = playerName.value;
            CmdFindGame(myData, GetComponent<NetworkIdentity>());
        }
    }
    [Command]
    public void CmdFindGame(PlayerData datas, NetworkIdentity identity)
    {
        identity.gameObject.name = datas.playerName;
        TakePlayerDatas(Lobby_Manager.Instance.FindGame(datas, identity));
    }
    [TargetRpc]
    public void TakePlayerDatas(int lastTime)
    {
        LobbyCanvas_Manager.Instance.StartBeginTime(lastTime);
    }
    [ClientRpc]
    public void AddPlayerIcon(PlayerData playerData, NetworkIdentity identity)
    {
        LobbyCanvas_Manager.Instance.AddPlayerIcons(playerData, identity);
    }
    [TargetRpc]
    public void AddBeforePlayerIcon(NetworkIdentity identity)
    {
        LobbyCanvas_Manager.Instance.AddBeforePlayerIcons(identity);
    }
    #endregion

    #region Quit
    public void ClientQuit()
    {
        Quit(matchGuidId, identity);
    }
    private void Quit(Guid matchID, NetworkIdentity identity)
    {
        if (matchGuidId != Guid.Empty && matchGuidId == matchID)
        {
            CmdPlayerQuit(matchID, identity);
            GameObject.FindGameObjectWithTag("NetWorkManager").GetComponent<NetworkManager>().StopClient();
        }
        SceneManager.LoadScene("Menu");
    }
    [Command]
    public void CmdPlayerQuit(Guid matchID, NetworkIdentity identity)
    {
        RemovePlayerIcon(identity);
        Lobby_Manager.Instance.QuitGame(matchID, identity);
    }
    [ClientRpc]
    public void RemovePlayerIcon(NetworkIdentity identity)
    {
        LobbyCanvas_Manager.Instance.RemovePlayerIcon(identity);
    }
    public override void OnStopClient()
    {
        if (LobbyCanvas_Manager.Instance.players.Count > 0)
        {
            LobbyCanvas_Manager.Instance.RemovePlayerIcon(identity);
        }
    }
    #endregion

    #region Chat
    public static event Action<PlayerData, string> OnMessage;
    [Command]
    public void CmdSend(string message, PlayerData datas)
    {
        if (message.Trim() != "")
            RpcReceive(message.Trim(), datas);
    }
    [ClientRpc]
    public void RpcReceive(string message, PlayerData datas)
    {
        OnMessage?.Invoke(datas, message);
    }
    #endregion
}