using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadData : MonoBehaviour {

    CreateJsonData CJD = new CreateJsonData();
    CharaterData CD = new CharaterData();
    

    private void Start()
    {
        CJD.IniCharaterData();// 初始化Json

    }

    public void LoadCharaterData()
    {
        

        //做一個讀取器

        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/GameData", "CharaterData.json"));

        string CharaterDataSring = fileReader.ReadToEnd();

        fileReader.Close();



        //將讀取的string改成player物件型態

        CD = JsonUtility.FromJson<CharaterData>(CharaterDataSring);

        // Debug.Log(CD.HidoruNo);
    }


}
