using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharaterData {

    // Type 1:Sword 2:Knight 3:Magician 4:Healer

    [Header("希多爾數值")]
    public int HidoruNo;
    public string HidoruImg;
    public int HidoruType;
    public int HidoruLv;
    public int HidoruExp;
    public int HidoruNextExp;
    public float HidoruMaxHp;
    public float HidoruAtkBuffValue;
    public float HidoruDefBuffValue;
    public float HidoruMagicBuffValue;
    public float HidoruHealBuffValue;

    [Header("奈爾數值")]
    public int NaiaNo;
    public string NaiaImg;
    public int NaiaType;
    public int NaiaLv;
    public int NaiaExp;
    public int NaiaNextExp;
    public float NaiaMaxHp;
    public float NaiaAtkBuffValue;
    public float NaiaDefBuffValue;
    public float NaiaMagicBuffValue;
    public float NaiaHealBuffValue;

    [Header("珂羅娜數值")]
    public int KoronaNo;
    public string KoronaImg;
    public int KoronaType;
    public int KoronaLv;
    public int KoronaExp;
    public int KoronaNextExp;
    public float KoronaMaxHp;
    public float KoronaAtkBuffValue;
    public float KoronaDefBuffValue;
    public float KoronaMagicBuffValue;
    public float KoronaHealBuffValue;

    [Header("米亞莎數值")]
    public int MiasaNo;
    public string MiasaImg;
    public int MiasaType;
    public int MiasaLv;
    public int MiasaExp;
    public int MiasaNextExp;
    public float MiasaMaxHp;
    public float MiasaAtkBuffValue;
    public float MiasaDefBuffValue;
    public float MiasaMagicBuffValue;
    public float MiasaHealBuffValue;


}
