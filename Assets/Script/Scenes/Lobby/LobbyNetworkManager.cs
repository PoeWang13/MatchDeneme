using Mirror;
using UnityEngine;

public class LobbyNetworkManager : NetworkManager
{
    /// <summary>
    /// Called on the server when a client disconnects.
    /// <para>This is called on the Server when a Client disconnects from the Server. Use an override to decide what should happen when a disconnection is detected.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Lobby_Manager.Instance.PlayerGone(conn.identity.GetComponent<Player>().matchGuidId,
                                          conn.identity.GetComponent<NetworkIdentity>());
        NetworkServer.DestroyPlayerForConnection(conn);
    }
    //public override void ServerChangeScene(string newSceneName)
    //{
    //    foreach (NetworkRoomPlayer roomPlayer in roomSlots)
    //    {
    //        if (roomPlayer == null)
    //            continue;

    //        // find the game-player object for this connection, and destroy it
    //        NetworkIdentity identity = roomPlayer.GetComponent<NetworkIdentity>();

    //        if (NetworkServer.active)
    //        {
    //            // re-add the room object
    //            roomPlayer.GetComponent<NetworkRoomPlayer>().readyToBegin = false;
    //            NetworkServer.ReplacePlayerForConnection(identity.connectionToClient, roomPlayer.gameObject);
    //        }
    //    }
    //    base.ServerChangeScene(newSceneName);
    //}
}