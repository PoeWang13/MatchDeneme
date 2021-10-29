using UnityEngine;

public class PreLoad_Manager : MonoSingletion<PreLoad_Manager>
{
    public PlayerDataVariables playerDataVariables;
    public override void GetSomeTing()
    {
        playerDataVariables.playerData = SaveManager.LoadPlayer();
    }
    private void OnApplicationQuit()
    {
        SaveManager.SavePlayer(playerDataVariables.playerData);
    }
}