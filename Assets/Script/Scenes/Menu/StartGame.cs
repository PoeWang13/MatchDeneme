using UnityEngine;
using Mirror;
using TMPro;
using System.Collections;

public enum StartingType
{
    Server,
    Host,
    Client
}
public enum PlayerNumber
{
    Server = 0,
    Host = 100,
    Birinci = 1,
    İkinci = 2,
    Üçüncü = 3,
    Dördüncü = 4
}
public class StartGame : MonoBehaviour
{
    public GameObject maxPlayerPanel;
    public IntVariables maxPlayerAmount;
    public TMP_InputField max_InputField;
    public GameObject uyariPanel;
    public TextMeshProUGUI uyariText;
    private StartingType startingType;
    public PlayerNumber playerNumber;
    [Space]
    public StringVariables playerName;
    public IntVariables playerIcon;
    public IntVariables playerCerceve;
    [Space]
    public NetworkManager networkManager;

    private void OnValidate()
    {
        if (networkManager == null)
        {
            networkManager = GameObject.FindGameObjectWithTag("NetWorkManager").GetComponent<NetworkManager>();
            return;
        }
        else
        {
            if (playerNumber == PlayerNumber.Server)
            {
                SetManager(false, StartingType.Server, 0);
            }
            else if (playerNumber == PlayerNumber.Host)
            {
                SetManager(true, StartingType.Host, 1);
            }
            else
            {
                SetManager(true, StartingType.Client, (int)playerNumber);
            }
        }
    }
    private void Start()
    {
        maxPlayerPanel.SetActive(startingType == StartingType.Server || startingType == StartingType.Host);
    }
    private void SetManager(bool canCreatePlayer, StartingType networkType, int myNumber)
    {
        networkManager.autoCreatePlayer = canCreatePlayer;
        startingType = networkType;
        playerName.SetValue("" + myNumber + myNumber + myNumber);
        playerIcon.SetValue(myNumber);
        playerCerceve.SetValue(myNumber);
    }
    public void Starts()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetWorkManager").GetComponent<NetworkManager>();
        //Debug.Log(networkManager.numPlayers);
        if (startingType == StartingType.Server)
        {
            if (max_InputField.text == "")
            {
                maxPlayerAmount.SetValue(2);
            }
            else
            {
                if (!int.TryParse(max_InputField.text, out maxPlayerAmount.value))
                {
                    UyariYap("Lütfen sayı girin.");
                    return;
                }
            }
            // Start Server
            Debug.Log("---Starting Server---");
            networkManager.StartServer();
        }
        else if (startingType == StartingType.Host)
        {
            if (!int.TryParse(max_InputField.text, out maxPlayerAmount.value))
            {
                UyariYap("Lütfen sayı girin.");
                return;
            }
            // Start Host
            Debug.Log("---Starting Host---");
            networkManager.StartHost();
        }
        else
        {
            // Wait for Lobby
            Debug.Log("---Waiting to Client for Lobby---");
            networkManager.StartClient();
        }
    }
    private void UyariYap(string uyari)
    {
        StartCoroutine(Uyari(uyari));
    }
    IEnumerator Uyari(string uyar)
    {
        uyariPanel.SetActive(true);
        uyariText.text = uyar;
        yield return new WaitForSeconds(2);
        uyariPanel.SetActive(false);
    }
}