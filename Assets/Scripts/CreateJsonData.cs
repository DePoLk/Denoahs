using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

class CreateJsonData : MonoBehaviour {

    CharaterData CD = new CharaterData();

	
    public void IniCharaterData()
    {
        //[Header("希多爾數值")]
        CD.HidoruNo = 1;
        CD.HidoruImg = "Hidoru";
        CD.HidoruType = 1;
        CD.HidoruLv = 1;
        CD.HidoruExp = 0;
        CD.HidoruNextExp = 50;
        CD.HidoruMaxHp = 150;
        CD.HidoruAtkBuffValue = 15;
        CD.HidoruDefBuffValue = 7;
        CD.HidoruMagicBuffValue = 5;
        CD.HidoruHealBuffValue = 8;

        //[Header("奈爾數值")]
        CD.NaiaNo = 2;
        CD.NaiaImg = "Naia";
        CD.NaiaType = 2;
        CD.NaiaLv = 1;
        CD.NaiaExp = 0;
        CD.NaiaNextExp = 50;
        CD.NaiaMaxHp = 220;
        CD.NaiaAtkBuffValue = 10;
        CD.NaiaDefBuffValue = 15;
        CD.NaiaMagicBuffValue = 3;
        CD.NaiaHealBuffValue = 5;

        //[Header("珂羅娜數值")]
        CD.KoronaNo = 3;
        CD.KoronaImg = "Korona";
        CD.KoronaType = 3;
        CD.KoronaLv = 1;
        CD.KoronaExp = 0;
        CD.KoronaNextExp = 50;
        CD.KoronaMaxHp = 120;
        CD.KoronaAtkBuffValue = 5;
        CD.KoronaDefBuffValue = 5;
        CD.KoronaMagicBuffValue = 20;
        CD.KoronaHealBuffValue = 10;

        //[Header("米亞莎數值")]
        CD.MiasaNo = 4;
        CD.MiasaImg = "Miasa";
        CD.MiasaType = 4;
        CD.MiasaLv = 1;
        CD.MiasaExp = 0;
        CD.MiasaNextExp = 50;
        CD.MiasaMaxHp = 140;
        CD.MiasaAtkBuffValue = 5;
        CD.MiasaDefBuffValue = 6;
        CD.MiasaMagicBuffValue = 8;
        CD.MiasaHealBuffValue = 12;







        // 暫存數據

        string SaveString = JsonUtility.ToJson(CD);

        // 輸出Json

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/GameData", "CharaterData.json"));

        file.Write(SaveString);

        file.Close();
    }
}
