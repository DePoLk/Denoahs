using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class Charater
{
    public int id;
    public string name;
    public string Img;
    public int Type;
    public int Lv;
    public int Exp;
    public int MaxHp;
    public int AtkBuffValue;
    public int DefBuffValue;
    public int MagicBuffValue;
    public int HealBuffValue;
    public int SpeedBuffValue;

    public Charater(int _id, string _name, string _Img, int _Type, int _Lv, int _Exp, int _MaxHp, int _AtkBuffValue, int _DefBuffValue, int _MagicBuffValue, int _HealBuffValue, int _SpeedBuffValue)
    {
        this.id = _id;
        this.name = _name;
        this.Img = _Img;
        this.Type = _Type;

        this.Lv = _Lv;
        this.Exp = _Exp;
        this.MaxHp = _MaxHp;
        this.AtkBuffValue = _AtkBuffValue;
        this.DefBuffValue = _DefBuffValue;
        this.MagicBuffValue = _MagicBuffValue;
        this.HealBuffValue = _HealBuffValue;
        this.SpeedBuffValue = _SpeedBuffValue;
    }
    

}



public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

class CreateJsonData : MonoBehaviour {

    

    private void Start()
    {
        IniCharaterData();
    }
   
    public  void IniCharaterData()
    {
        Charater[] CharaterUnJsonData = new Charater[4];

        CharaterUnJsonData[0] = new Charater(0,"Hidoru","Assets/Sprites/CharaterImg/Hidoru_1.png",1,1,0,150,15,7,5,8,15);
        CharaterUnJsonData[1] = new Charater(1, "Naia", "Assets/Sprites/CharaterImg/Naia_2.png", 2, 1,0, 220, 10, 15, 3, 5, 8);
        CharaterUnJsonData[2] = new Charater(2, "Korona", "Assets/Sprites/CharaterImg/Korona_3.png", 3, 1,0, 120, 5, 5, 20, 10,10);
        CharaterUnJsonData[3] = new Charater(3, "Miasa", "Assets/Sprites/CharaterImg/Miasa_4.png", 4, 1, 0, 140, 5, 6, 8, 12, 9);

        string CharDataJson = JsonHelper.ToJson(CharaterUnJsonData, true); // 轉換Json
       


        //輸出Json
        StreamWriter File = new StreamWriter(Application.dataPath + "/GameData/CharaterData.json");
        File.Write(CharDataJson);
        File.Close();
        //輸出Json
    }


   

}



