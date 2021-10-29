using UnityEngine;
using Mirror;
using System;
using System.Collections.Generic;

[Serializable]
public struct MatchWorldPosition
{
    public int worldIndex;
    public bool canUse;
    public GameObject gameWorld;
    public MatchWorldPosition(int worldNumber, bool use, GameObject world)
    {
        worldIndex = worldNumber;
        canUse = use;
        gameWorld = world;
    }
}
[Serializable]
public struct Datas
{
    public int playerNumber;
    public int beginTime;
    public Guid matchGuid;
    public Match myMatch;
    public List<PlayerData> playerDatas;
    public Datas(Guid guid, int player, int time, Match myMat, List<PlayerData> Datas)
    {
        matchGuid = guid;
        playerNumber = player;
        beginTime = time;
        myMatch = myMat;
        playerDatas = Datas;
    }
}
[Serializable]
public struct PlayerData
{
    public string playerName;
    public int playerIcon;
    public int playerCerceve;
    //public bool ff;
    public PlayerData(string name, int icon, int cerceve)
    {
        playerName = name;
        playerIcon = icon;
        playerCerceve = cerceve;
    }
}
[Serializable]
public struct MatchPlayerData
{
    public NetworkIdentity networkIdentity;
    public PlayerData playerData;
    public MatchPlayerData(NetworkIdentity identity, PlayerData datas)
    {
        networkIdentity = identity;
        playerData = datas;
    }
}
[Serializable]
public class Match
{
    // Match id, player dataları, match açık mı
    public bool isOpenMatch;
    public int beginTime = 10;
    public int maxPlayer = 10;
    public int matchWorldIndex;
    public Guid matchGuidId;
    public SyncListPlayer playerDatas = new SyncListPlayer();

    public Match() { }
    public Match(Guid matchID, NetworkIdentity player, int maxPlayer)
    {
        isOpenMatch = true;
        matchGuidId = matchID;
        this.maxPlayer = maxPlayer;
        playerDatas.Add(player);
    }
    public bool CanAddMatchPlayer()
    {
        return isOpenMatch && playerDatas.Count < maxPlayer;
    }
    public void AddMatchPlayer(NetworkIdentity player)
    {
        playerDatas.Add(player);
        if (playerDatas.Count == maxPlayer)
        {
            isOpenMatch = false;
        }
    }
}
[Serializable]
public class SyncListPlayer : SyncList<NetworkIdentity> { }

[Serializable]
public class SyncListMatch : SyncList<Match> { }

public class Lobby_Manager : NetworkBehaviour
{
    public static Lobby_Manager Instance;
    public IntVariables maxPlayerAmount;
    private void Awake()
    {
        Instance = this;
    }
    public SyncListMatch matches = new SyncListMatch();
    private float beginTimeNext;
    public GameObject matchWorld;
    public List<MatchWorldPosition> matchWorldIndex = new List<MatchWorldPosition>();
    private void Start()
    {
        //worldIndex.Add(new WorldPosition(0, false, null));
        //LobbyNetworkManager lobbyNetworkManager = GameObject.FindGameObjectWithTag("LobbyNetworkManager").
        //                                            GetComponent<LobbyNetworkManager>();
    }
    private void Update()
    {
        if (isServer)
        {
            if (matches.Count == 0)
            {
                return;
            }
            beginTimeNext += Time.deltaTime;
            if (beginTimeNext > 1)
            {
                beginTimeNext = 0;
                for (int e = matches.Count - 1; e >= 0; e--)
                {
                    if (matches[e].playerDatas.Count == 0)
                    {
                        matches.RemoveAt(e);
                    }
                    else
                    {
                        matches[e].beginTime--;
                        if (matches[e].beginTime == 0)
                        {
                            MatchReady(e);
                        }
                    }
                }
            }
        }
    }
    private void MatchReady(int matchIndex)
    {
        matches[matchIndex].isOpenMatch = false;
        if (matches[matchIndex].playerDatas.Count > 0)
        {
            // Match'den çıkmış oyuncuları sil
            for (int h = matches[matchIndex].playerDatas.Count - 1; h >= 0; h--)
            {
                if (matches[matchIndex].playerDatas[h] == null)
                {
                    matches[matchIndex].playerDatas.RemoveAt(h);
                }
            }
            // Match'deki playerların numarasını belirle
            for (int h = 0; h < matches[matchIndex].playerDatas.Count; h++)
            {
                matches[matchIndex].playerDatas[h].GetComponent<Player>().playerNumber = h;
            }
            //// Match'deki playerları dizdirt ve tüm kullanıcılara oyun baslattır.
            //matches[e].playerDatas[0].GetComponent<Player>().ShortPlayer(matches[e].matchGuidId);
        }
    }
    public void PlayerGone(Guid matchGuid, NetworkIdentity identity)
    {
        for (int e = matches.Count - 1; e >= 0; e--)
        {
            if (matches[e].matchGuidId == matchGuid)
            {
                for (int h = matches[e].playerDatas.Count - 1; h >= 0; h--)
                {
                    if (matches[e].playerDatas[h] == null || matches[e].playerDatas[h] == identity)
                    {
                        matches[e].playerDatas.RemoveAt(h);
                    }
                    else
                    {
                        matches[e].playerDatas[h].GetComponent<Player>().playerNumber = h;
                    }
                }
            }
        }
    }
    public int FindGame(PlayerData playerData, NetworkIdentity identity)
    {
        bool findedGame = false;
        int time = -1;
        Guid matchGuid = Guid.Empty;
        Match mewMatches = new Match();
        List<PlayerData> playerDatas = new List<PlayerData>();
        for (int e = 0; e < matches.Count && !findedGame; e++)
        {
            if (matches[e].CanAddMatchPlayer())
            {
                findedGame = true;
                matchGuid = matches[e].matchGuidId;

                Player p = identity.GetComponent<Player>();

                matches[e].AddMatchPlayer(identity);
                mewMatches = matches[e];
                time = matches[e].beginTime;
                identity.GetComponent<NetworkMatchChecker>().matchId = matchGuid;
                p.matchGuidId = matchGuid;
                p.myData = playerData;
                p.myMatch = matches[e];
                p.matchNumber = mewMatches.matchWorldIndex;

                // Playerın iconunu herkese gönder
                p.AddPlayerIcon(playerData, identity);
                // Playerdan öncekilerin ikonunu gönder
                p.AddBeforePlayerIcon(identity);
            }
        }
        if (!findedGame)
        {
            // Create match, matchId and time
            matchGuid = Guid.NewGuid();
            mewMatches = new Match(matchGuid, identity, maxPlayerAmount.value);
            mewMatches.matchWorldIndex = matches.Count;
            time = mewMatches.beginTime;

            // Set Player component and NetworkMatchChecker
            identity.GetComponent<NetworkMatchChecker>().matchId = matchGuid;
            Player p = identity.GetComponent<Player>();
            p.matchGuidId = matchGuid;
            p.myData = playerData;
            p.myMatch = mewMatches;
            p.matchNumber = matches.Count;

            // Player Icon Eklet
            p.AddPlayerIcon(playerData, identity);

            // Add match to matches List
            matches.Add(mewMatches);
            // Add player to playerDatas List
            playerDatas.Add(playerData);

            // Create a new World
            GameObject newWorld = Instantiate(matchWorld);
            newWorld.GetComponent<NetworkMatchChecker>().matchId = matchGuid;

            // Set position new world
            bool newPosition = false;
            for (int e = 0; e < matchWorldIndex.Count && !newPosition; e++)
            {
                if (matchWorldIndex[e].canUse)
                {
                    newPosition = true;
                    newWorld.transform.position = new Vector3(3, 0, 0) * e;
                    MatchWorldPosition worldPosition = matchWorldIndex[e];
                    worldPosition.canUse = false;
                    worldPosition.gameWorld = newWorld;
                    matchWorldIndex[e] = worldPosition;
                }
            }
            // Add new World to newWorld List
            if (!newPosition)
            {
                newWorld.transform.position = new Vector3(3, 0, 0) * matchWorldIndex.Count;
                matchWorldIndex.Add(new MatchWorldPosition(matchWorldIndex.Count, false, newWorld));
            }
            NetworkServer.Spawn(newWorld);
        }
        return time;
    }
    public void QuitGame(Guid matchGuid, NetworkIdentity identity)
    {
        for (int e = matches.Count - 1; e >= 0; e--)
        {
            if (matches[e].matchGuidId == matchGuid)
            {
                for (int h = matches[e].playerDatas.Count - 1; h >= 0; h--)
                {
                    if (matches[e].playerDatas[h] == null || matches[e].playerDatas[h] == identity)
                    {
                        matches[e].playerDatas.RemoveAt(h);
                    }
                }
            }
        }
    }
}