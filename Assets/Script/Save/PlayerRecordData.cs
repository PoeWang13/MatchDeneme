using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerRecordData
{
    // Kaydedilecek datalar
    public bool oyunBasladi;
    public int playingLevel;
    public int lastLevel;
    public int playerLife;
    [HideInInspector]
    public byte[] allByte;
    // Kaydedilmeyecek ama kullanılacak datalar
    // Byte arrayi ve bool listi inspectorden ayarlanmalı
    [HideInInspector]
    public List<bool> allBool = new List<bool>();

    // int - float - bool - vector ve color(float[] şeklinde) şeklinde kaydedebilirsin
    public PlayerRecordData()
    {
        playerLife = 0;
        lastLevel = 0;
        playingLevel = 0;
        oyunBasladi = false;
    }

    // Kaydedebilmek için boolları byte dönüştür.
    public void SetBoolToByte()
    {
        for (int e = 0; e < allByte.Length; e++)
        {
            allByte[e] = 0;
        }
        for (int e = 0; e < allBool.Count; e++)
        {
            if (allBool[e])
            {
                allByte[e / 8] += (byte)Mathf.Pow(2, e % 8);
            }
        }
    }
    // Kaydedilmiş byteları kullanmak için boollara dönüştür.
    public void SetByteToBool()
    {
        for (int e = 0; e < allByte.Length; e++)
        {
            for (int h = 0; h < 8; h++)
            {
                if ((allByte[e] & (byte)Mathf.Pow(2, h % 8)) != 0)
                {
                    allBool[(e * 8) + h] = true;
                }
            }
        }
    }
}