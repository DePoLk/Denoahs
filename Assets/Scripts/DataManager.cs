using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Pathfinding.Serialization.JsonFx;




public class DataManager : MonoBehaviour{

    [System.Serializable]
    public class CharaterData
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
    } // 用以轉化多項目json

    // Use this for initialization
    void Start () {

       //CJD.IniCharaterData(); //初始化Json

       //初始讀取
        FileStream fs = new FileStream(Application.dataPath + "/GameData/CharaterData.json", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        // PlayerPrefs.SetString("JsonData", sr.ReadToEnd());

        string LoadJson = sr.ReadToEnd();

        CharaterData[] GetLoadData = JsonHelper.FromJson<CharaterData>(LoadJson); // Json轉成物件

        /*for(int i = 0; i < GetLoadData.Length; i++)
        {
            Debug.Log("id: " + GetLoadData[i].id);
            Debug.Log("name: " + GetLoadData[i].name);
            Debug.Log("Img: " + GetLoadData[i].Img);
            Debug.Log("Type: " + GetLoadData[i].Type);
            Debug.Log("Lv: " + GetLoadData[i].Lv);
            Debug.Log("Exp: " + GetLoadData[i].Exp);
            Debug.Log("MaxHp: " + GetLoadData[i].MaxHp);
            Debug.Log("Atk: " + GetLoadData[i].AtkBuffValue);
            Debug.Log("Def: " + GetLoadData[i].DefBuffValue);
            Debug.Log("Magic: " + GetLoadData[i].MagicBuffValue);
            Debug.Log("Heal: " + GetLoadData[i].HealBuffValue);
            Debug.Log("Speed: " + GetLoadData[i].SpeedBuffValue);
            Debug.Log("---Next_Charater---");


        }*/





        fs.Close();


        
        
         

        //初始讀取

        

        //Debug.Log(PlayerPrefs.GetString("JsonData"));
        /*Debug.Log("ID: " +LoadCharData.id);
        Debug.Log("Name: " +LoadCharData.name);
        Debug.Log("Img: " +LoadCharData.Img);
        Debug.Log("Type: " +LoadCharData.Type);
        for(int i = 0;i < LoadCharData.Stats.Capacity; i++)
        {
            Debug.Log("Stats" + "[" + i + "]: " + LoadCharData.Stats[i]);
        }*/


    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.S))
        {
           
        }//Save
        if (Input.GetKeyDown(KeyCode.L))
        {

           

        }//Load

	}


    public void SaveData()
    {

    }

    public void LoadData()
    {

    }


}

