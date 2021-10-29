using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    // Game Manager scriptinde SavePlayer fonksiyonunda bunu çağıracaksın
    private static string path = "/HEC_XX.HEC";
    public static void SavePlayer(PlayerRecordData playerData)
    {
        BinaryFormatter formater = new BinaryFormatter();
        string yol = Application.persistentDataPath + path;

        FileStream stream = new FileStream(yol, FileMode.Create);

        playerData.SetBoolToByte();
        formater.Serialize(stream, playerData);
        stream.Close();
    }
    // Game Manager scriptinde LoadPlayer fonksiyonunda bunu çağıracaksın
    public static PlayerRecordData LoadPlayer()
    {
        string yol = Application.persistentDataPath + path;

        PlayerRecordData bilgi = null;
        if (File.Exists(yol))
        {
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(yol, FileMode.Open);

            bilgi = (PlayerRecordData)formater.Deserialize(stream);
            stream.Close();

            bilgi.SetByteToBool();
        }
        else
        {
            Debug.LogWarning("You lost your data.");
            bilgi = new PlayerRecordData();
        }
        return bilgi;
    }
}