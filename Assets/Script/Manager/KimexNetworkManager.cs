using UnityEngine;
using Mirror;

public class KimexNetworkManager : NetworkManager
{
    public override void OnApplicationQuit()
    {
        //Debug.Log("111");
        base.OnApplicationQuit();
    }
    public override void OnStopClient()
    {
        //Debug.Log("222");
        base.OnStopClient();
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        //Debug.Log("333");
        base.OnClientDisconnect(conn);
    }
}