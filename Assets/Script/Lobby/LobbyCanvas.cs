using UnityEngine;

public class LobbyCanvas : MonoSingletion<LobbyCanvas>
{
    public LobbyRoomYonetici lobbyRoomYonetici;
    public void STARTGAME()
    {
        lobbyRoomYonetici.ServerChangeScene(lobbyRoomYonetici.GameplayScene);
    }
}