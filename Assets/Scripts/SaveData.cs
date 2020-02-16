using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour {

    CharaterData CD = new CharaterData();
    LoadData LD = new LoadData();

    public void SaveCharaterData()
    {
        //LD.LoadCharaterData();


        // 存檔修改

        //CD.HidoruNo = 9487;

        // 存檔修改
        if (File.Exists("Assets/GameData"))
        {
            string jsonString = JsonUtility.ToJson(CD);
            File.WriteAllText("Assets/GameData", jsonString);
        }
        Debug.Log(CD.HidoruNo);

        // 暫存數據

        string SaveString = JsonUtility.ToJson(CD);

        // 輸出Json

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/GameData", "CharaterData.json"));

        file.Write(SaveString);

        file.Close();
    }

}
