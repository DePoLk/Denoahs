using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using DG.Tweening.Plugins.Options;

public class BattleControlEvent : MonoBehaviour {

    BattleSceneManager BSM;

    [Header("音效區域")]
    public AudioClip[] SwordsmanAC;
    public AudioClip[] KnightAC;
    public AudioClip[] MagicianAC;
    public AudioClip[] HealerAC;
    public AudioClip[] Enemy0_AC;
    public AudioSource Enemy0_AS;
    public AudioSource PlayerAS;

    public AudioSource BGM_AS;
    public AudioSource SE_AS;
    public AudioClip[] BGM;
    public AudioClip[] SE;

    [Header("抓取子父物件用")]
    public GameObject BattlePanel;

    [Header("轉場景用")]
    public Image FadeImage;
    public Image TeachImage;
    public bool IsTeachDone;

    [Header("BattlePanel陣列")]
    public List<int> BattlePanelRequest = new List<int>();
    public List<Image> BattlePanelImageList = new List<Image>();
    Image NowInstantiateBattlePanelImg;

    [Header("玩家輸入監測")]
    public List<int> PlayerPress = new List<int>();
    public bool IsPlayerPressDone = false;
    int NowPressNum = 0;

    [Header("玩家行動數值")]

    public float Timer_Time;
    public float Timer_MaxTime;
    public bool IsTimeOut = false;
    public bool PlayerIsPressing = false;
    public Image TimerImg;

    public int PlayerActionType;

    public int PressRightAtkBtn;
    public int PressRightDefBtn;
    public int PressRightMagicBtn;
    public int PressRightHealBtn;
    public int PressWrongBtn;

    public float FinalAtkValue;
    public float FinalDefValue;
    public float FinalMagicValue;
    public float FinalHealValue;

    [Header("劍士數值")]
    public Image SwordsmanImg;
    public float SwordsmanMaxHp;
    public float SwordsmanHp;
    public float SwordsmanShield;
    public float SwordsmanAtkBuffValue;
    public float SwordsmanDefBuffValue;
    public float SwordsmanMagicBuffValue;
    public float SwordsmanHealBuffValue;
    public bool IsSwordsmanDead = false;

    [Header("騎士數值")]
    public Image KnightImg;
    public float KnightMaxHp;
    public float KnightHp;
    public float KnightShield;
    public float KnightAtkBuffValue;
    public float KnightDefBuffValue;
    public float KnightMagicBuffValue;
    public float KnightHealBuffValue;
    public bool IsKnightDead = false;

    public bool IsFocus = false;

    [Header("法師數值")]
    public Image MagicianImg;
    public float MagicianMaxHp;
    public float MagicianHp;
    public float MagicianShield;
    public float MagicianAtkBuffValue;
    public float MagicianDefBuffValue;
    public float MagicianMagicBuffValue;
    public float MagicianHealBuffValue;
    public bool IsMagicianDead = false;

    [Header("祭師數值")]
    public Image HealerImg;
    public float HealerMaxHp;
    public float HealerHp;
    public float HealerShield;
    public float HealerAtkBuffValue;
    public float HealerDefBuffValue;
    public float HealerMagicBuffValue;
    public float HealerHealBuffValue;
    public bool IsHealerDead = false;



    [Header("敵人數值")]
    public Image Enemy0_Img;
    public float Enemy0_MaxHp;
    public float Enemy0_Hp;
    public float Enemy0_Atk;
    public float Enemy0_Def;
    public float Enemy0_SkillAtkBuff;
    public int Enemy0_AtkSelect = 0;
    public bool Enemy0_IsSkill = false;
    public int Enemy0_RandomSkillValue;
    public bool IsEnemy0_Dead = false;

    //傷害值路徑數值
    Vector3[] Enemy0_DamagePath = {new Vector3(0,160,0), new Vector3(70, 200, 0), new Vector3(130, 150) };
    Vector3[] SwordsDamagePath = {new Vector3(5,5,0), new Vector3(40,55,0), new Vector3(100,5,0) };
    Vector3[] KnightDamagePath = { new Vector3(5, 5, 0), new Vector3(40, 55, 0), new Vector3(100, 5, 0) };
    Vector3[] MagicianDamagePath = { new Vector3(5, 5, 0), new Vector3(40, 55, 0), new Vector3(100, 5, 0) };
    Vector3[] HealerDamagePath = { new Vector3(5, 5, 0), new Vector3(40, 55, 0), new Vector3(100, 5, 0) };
    Vector3[] PlayerHealPath = { new Vector3(5, 5, 0),new Vector3(5,70,0) };
    

    [Header("抓取場景物件")]
    public Image SwordsmanBattlePanelImg;
    public Image KnightBattlePanelImg;
    public Image MagicianBattlePanelImg;
    public Image HealerBattlePanelImg;
    public GameObject BattleControl;
    public Image Enemy0_HpImg;
    public Image SwordsmanHpImg;
    public Image KnightHpImg;
    public Image MagicianHpImg;
    public Image HealerHpImg;
    public Text SwordsmanHpText;
    public Text KnightHpText;
    public Text MagicianHpText;
    public Text HealerHpText;
    public Text Enemy0_HpText;
    public Image SwordsmanDefImg;
    public Image KnightFocusImg;
    public Image MagicianDefImg;
    public Image HealerDefImg;
    public Image Enemy0_SkillImg;
    public Text Enemy0_Damage;
    public Text SwordsmanDamage;
    public Text KnightDamage;
    public Text MagicianDamage;
    public Text HealerDamage;
    public Text SwordsmanHeal;
    public Text KnightHeal;
    public Text MagicianHeal;
    public Text HealerHeal;

    [Header("抓取場景特效物件")]
    public Image Enemy0_GetHurt;
    public Image SwordsmanGetHurt;
    public Image KnightGetHurt;
    public Image MagicianGetHurt;
    public Image HealerGetHurt;

    [Header("遊戲結束頁面抓取")]
    public GameObject WinGroup;
    public GameObject LoseGroup;
    public bool IsLose = false;

    Vector3 BattlePanelFirstPos;

    // Use this for initialization
    void Start () {
        StartBattleStageFade();

        BSM = FindObjectOfType<BattleSceneManager>();
        BattlePanelFirstPos = SwordsmanBattlePanelImg.GetComponent<RectTransform>().localPosition;
        //RandomCreateBattlePanel();
        HideBattlePanelImage();

        SwordsmanHpText.text = SwordsmanHp.ToString();
        KnightHpText.text = KnightHp.ToString();
        MagicianHpText.text = MagicianHp.ToString();
        HealerHpText.text = HealerHp.ToString();
        Enemy0_HpText.text = Enemy0_Hp.ToString();

    }

    // Update is called once per frame
    void Update () {
        if (IsTeachDone)
        {
            CheckPlayerPress();
            CheckEveryOneState();
            Timer();
        }
	}

    void StartBattleStageFade()
    {
        FadeImage.DOFade(0, 0.5f);        
    }

    public void TeachImageOnClick()
    {
        StartCoroutine("DelayCloseTeachImage");
    }

    IEnumerator DelayCloseTeachImage()
    {
        IsTeachDone = true;
        TeachImage.DOFade(0, 0.5f);
        yield return new WaitForSeconds(1f);
        TeachImage.gameObject.SetActive(false);
    }

    void Timer() {       
        TimerImg.fillAmount = Timer_Time / Timer_MaxTime;

        if(Timer_Time <= 0)
        {
            IsTimeOut = true;
        }
        else if(!IsTimeOut && PlayerIsPressing)
        {
            Timer_Time -= Time.deltaTime;
        }

    }

    void CheckEveryOneState()
    {
        if(SwordsmanHp <= 0 && !IsSwordsmanDead)
        {
            SwordsmanHp = 0;
            SwordsmanHpText.text = SwordsmanHp.ToString();
            BSM.SwordsmanSpeedValue = 0;
            BSM.SwordsmanSpeedImg.color = new Color32(255,0,0,0);
            SwordsmanImg.color = new Color32(174, 26, 26, 255);
            IsSwordsmanDead = true;

            /*播放音效*/
            PlayerAS.PlayOneShot(SwordsmanAC[5]);
            /*播放音效*/

        }// Swordsman Dead
        if (KnightHp <= 0 && !IsKnightDead)
        {
            KnightHp = 0;
            KnightHpText.text = KnightHp.ToString();
            BSM.KnightSpeedValue = 0;
            BSM.KnightSpeedImg.color = new Color32(255, 0, 0, 0);
            KnightImg.color = new Color32(174, 26, 26, 255);
            IsKnightDead = true;
            /*播放音效*/
            PlayerAS.PlayOneShot(KnightAC[6]);
            /*播放音效*/
        }
        if (MagicianHp <= 0 && !IsMagicianDead)
        {
            MagicianHp = 0;
            MagicianHpText.text = MagicianHp.ToString();
            BSM.MagicianSpeedValue = 0;
            BSM.MagicianSpeedImg.color = new Color32(255, 0, 0, 0);
            MagicianImg.color = new Color32(174, 26, 26, 255);
            IsMagicianDead = true;

            /*播放音效*/
            PlayerAS.PlayOneShot(MagicianAC[5]);
            /*播放音效*/
        }
        if (HealerHp <= 0 && !IsHealerDead)
        {
            HealerHp = 0;
            HealerHpText.text = HealerHp.ToString();
            BSM.HealerSpeedValue = 0;
            BSM.HealerSpeedImg.color = new Color32(255, 0, 0, 0);
            HealerImg.color = new Color32(174, 26, 26, 255);
            IsHealerDead = true;

            /*播放音效*/
            PlayerAS.PlayOneShot(HealerAC[6]);
            /*播放音效*/
        }

        if (Enemy0_Hp <= 0 && !IsEnemy0_Dead)
        {
            Enemy0_Hp = 0;
            Enemy0_HpText.text = Enemy0_Hp.ToString();
            BSM.EnemySpeedValue_0 = 0;
            BSM.EnemySpeedImg_0.color = new Color32(255, 0, 0, 0);
            Enemy0_Img.color = new Color32(255, 150, 150, 255);
            IsEnemy0_Dead = true;
            /*敵人死亡 停止玩家動作*/
            BSM.SwordsmanSpeedValue = 0;
            BSM.KnightSpeedValue = 0;
            BSM.MagicianSpeedValue = 0;
            BSM.HealerSpeedValue = 0;
            /*敵人死亡 停止玩家動作*/

            int WhoCallForWin = 0;

            WhoCallForWin = Random.Range(1, 5);

            if(WhoCallForWin == 1)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(SwordsmanAC[4]);
                /*播放音效*/
            }
            if (WhoCallForWin == 2)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(KnightAC[5]);
                /*播放音效*/
            }
            if (WhoCallForWin == 3)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(MagicianAC[4]);
                /*播放音效*/
            }
            if (WhoCallForWin == 4)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(HealerAC[5]);
                /*播放音效*/
            }

            /*播放勝利音效*/
            BGM_AS.Stop();
            BGM_AS.PlayOneShot(BGM[1]);
            /*播放勝利音效*/

            WinGroup.SetActive(true);
            WinGroup.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            WinGroup.GetComponent<CanvasGroup>().interactable = true;
            

        }

        if(IsSwordsmanDead && IsKnightDead && IsMagicianDead && IsHealerDead && !IsLose)
        {
            IsLose = true;

            BSM.EnemySpeedValue_0 = 0;

            /*播放失敗音效*/
            BGM_AS.Stop();           
            BGM_AS.PlayOneShot(BGM[2]);
            Debug.Log("PlayFailedMusic");
            /*播放失敗音效*/

            LoseGroup.SetActive(true);
            LoseGroup.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            LoseGroup.GetComponent<CanvasGroup>().interactable = true;
            
        }

    }

    void HideBattlePanelImage()
    {
        SwordsmanBattlePanelImg.color -= new Color32(0, 0, 0, 255);
        KnightBattlePanelImg.color -= new Color32(0, 0, 0, 255);
        MagicianBattlePanelImg.color -= new Color32(0, 0, 0, 255);
        HealerBattlePanelImg.color -= new Color32(0, 0, 0, 255);
    }

    void ShowBattlePanelImage()
    {
        SwordsmanBattlePanelImg.color += new Color32(0, 0, 0, 255);
        KnightBattlePanelImg.color += new Color32(0, 0, 0, 255);
        MagicianBattlePanelImg.color += new Color32(0, 0, 0, 255);
        HealerBattlePanelImg.color += new Color32(0, 0, 0, 255);
    }

    public void RandomCreateBattlePanel()
    {

        ShowBattlePanelImage();

        for (int i = 0; i < 10; i++)
        {
            BattlePanelRequest.Add(Random.Range(1,5));// 輸入版面需求處
        }

        for(int j = 0; j < 10; j++)
        {
            if(BattlePanelRequest[j] == 1)
            {
                NowInstantiateBattlePanelImg = Instantiate(SwordsmanBattlePanelImg, SwordsmanBattlePanelImg.transform.position + new Vector3(104 * j, 0, 0),Quaternion.identity) as Image;
                NowInstantiateBattlePanelImg.transform.parent = BattlePanel.transform;

                BattlePanelImageList.Add(NowInstantiateBattlePanelImg);
            }// Swords
            if (BattlePanelRequest[j] == 2)
            {
                NowInstantiateBattlePanelImg = Instantiate(KnightBattlePanelImg, SwordsmanBattlePanelImg.transform.position + new Vector3(104 * j, 0, 0), Quaternion.identity) as Image;
                NowInstantiateBattlePanelImg.transform.parent = BattlePanel.transform;

                BattlePanelImageList.Add(NowInstantiateBattlePanelImg);
                
            }// Knight
            if (BattlePanelRequest[j] == 3)
            {
                NowInstantiateBattlePanelImg = Instantiate(MagicianBattlePanelImg, SwordsmanBattlePanelImg.transform.position + new Vector3(104 * j, 0, 0), Quaternion.identity) as Image;
                NowInstantiateBattlePanelImg.transform.parent = BattlePanel.transform;

                BattlePanelImageList.Add(NowInstantiateBattlePanelImg);

            }// Magician
            if (BattlePanelRequest[j] == 4)
            {
                NowInstantiateBattlePanelImg = Instantiate(HealerBattlePanelImg, SwordsmanBattlePanelImg.transform.position + new Vector3(104 * j, 0, 0), Quaternion.identity) as Image;
                NowInstantiateBattlePanelImg.transform.parent = BattlePanel.transform;

                BattlePanelImageList.Add(NowInstantiateBattlePanelImg);
            }// Healer
        }

        HideBattlePanelImage();

    }

    public void CheckPlayerPress()
    {
        if (!IsPlayerPressDone)
        {
            //Debug.Log("判定完成否");
            // *** 下面行判定會造成SpeedBar判定錯誤 ****
            if (PlayerPress.Count == BattlePanelRequest.Count)
            {
                OutPutPlayerAction();
                IsPlayerPressDone = true;               
            }
            else if (IsTimeOut)
            {
                OutPutPlayerAction();
                IsPlayerPressDone = true;

                /*敵人拉條*/
                BSM.EnemySpeedImg_0.transform.DOScale(new Vector3(1.2f,1.2f,1),0.25f);
                BSM.EnemySpeedImg_0.transform.DOLocalMove(BSM.EnemySpeedImg_0.transform.localPosition - new Vector3(0,40,0),0.25f);
                BSM.EnemySpeedImg_0.transform.DOScale(new Vector3(1f, 1f, 1), 0.25f).SetDelay(0.5f);
                /*敵人拉條*/

            }
        }

        

    }


    void OutPutPlayerAction()
    {
        for (int i = 0; i < BattlePanelImageList.Count; i++)
        {
            Destroy(BattlePanelImageList[i].gameObject);
        }//清除場景上的BattlePanel圖片          
        //重置BattlePanel數據

        if (BSM.IsSwordsmanTurn)
        {
            SwordsmanShield = 0;
            SwordsmanDefImg.fillAmount = 0;
            /*重新行動時重置防禦行為*/

            FinalAtkValue = Random.Range(10,100) + Mathf.Round(PressRightAtkBtn * SwordsmanAtkBuffValue);
            FinalDefValue = Random.Range(5,10) + Mathf.Round(PressRightDefBtn * SwordsmanDefBuffValue);
            FinalMagicValue = Random.Range(4,10) + Mathf.Round(PressRightMagicBtn * SwordsmanMagicBuffValue);
            FinalHealValue = Random.Range(10,15) + Mathf.Round(PressRightHealBtn * SwordsmanHealBuffValue);
            BSM.SwordsmanSpeedImg.transform.localPosition = new Vector3(0, 0, 0);

        }
        if (BSM.IsKnightTurn)
        {
            KnightShield = 0;
            KnightFocusImg.fillAmount = 0;
            /*重新行動時重置防禦行為*/

            FinalAtkValue = Random.Range(10,25) + Mathf.Round(PressRightAtkBtn * KnightAtkBuffValue);
            FinalDefValue = Random.Range(15,50) + Mathf.Round(PressRightDefBtn * KnightDefBuffValue);
            FinalMagicValue = Random.Range(8,15) + Mathf.Round(PressRightMagicBtn * KnightMagicBuffValue);
            FinalHealValue = Random.Range(12,20) + Mathf.Round(PressRightHealBtn * KnightHealBuffValue);
            BSM.KnightSpeedImg.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (BSM.IsMagicianTurn)
        {
            MagicianShield = 0;
            MagicianDefImg.fillAmount = 0;
            /*重新行動時重置防禦行為*/

            FinalAtkValue = Random.Range(5,10) + Mathf.Round(PressRightAtkBtn * MagicianAtkBuffValue);
            FinalDefValue = Random.Range(6,10) + Mathf.Round(PressRightDefBtn * MagicianDefBuffValue);
            FinalMagicValue = Random.Range(50,70) + Mathf.Round(PressRightMagicBtn * MagicianMagicBuffValue);
            FinalHealValue = Random.Range(20,40) + Mathf.Round(PressRightHealBtn * MagicianHealBuffValue);
            BSM.MagicianSpeedImg.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (BSM.IsHealerTurn)
        {
            HealerShield = 0;
            HealerDefImg.fillAmount = 0;
            /*重新行動時重置防禦行為*/

            FinalAtkValue = Random.Range(10,15) + Mathf.Round(PressRightAtkBtn * HealerAtkBuffValue);
            FinalDefValue = Random.Range(10,15) + Mathf.Round(PressRightDefBtn * HealerDefBuffValue);
            FinalMagicValue = Random.Range(20,30) + Mathf.Round(PressRightMagicBtn * HealerMagicBuffValue);
            FinalHealValue = Random.Range(50,120) + Mathf.Round(PressRightHealBtn * HealerHealBuffValue);
            
            BSM.HealerSpeedImg.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (PlayerActionType == 1)
        {


            Enemy0_Hp -= FinalAtkValue;
            Enemy0_HpImg.fillAmount = Enemy0_Hp /Enemy0_MaxHp;
            Enemy0_HpText.text = Enemy0_Hp.ToString();


            /*傷害值動畫*/
            Enemy0_Damage.text = FinalAtkValue.ToString();
            Enemy0_Damage.DOFade(1, 0.5f);
            Enemy0_Damage.transform.DOScale(new Vector3(1,1,0),0.5f);
            Enemy0_Damage.transform.DOLocalPath(Enemy0_DamagePath,0.5f);
            Enemy0_Damage.DOFade(0, 0.5f).SetDelay(1f);
            Enemy0_Damage.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
            /*傷害值動畫*/

            /*傷害特效*/
            Enemy0_GetHurt.DOFade(1, 0.125f);
            Enemy0_GetHurt.DOFade(0, 0.125f).SetDelay(0.25f);
            /*傷害特效*/

            if (BSM.IsSwordsmanTurn)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(SwordsmanAC[Random.Range(0, 2)]);
                /*播放音效*/
            }
            if (BSM.IsKnightTurn)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(KnightAC[Random.Range(0, 3)]);
                /*播放音效*/
            }
            if (BSM.IsMagicianTurn)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(MagicianAC[Random.Range(0, 2)]);
                /*播放音效*/
            }
            if (BSM.IsHealerTurn)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(HealerAC[Random.Range(0, 2)]);
                /*播放音效*/
            }

            /*播放音效*/
            SE_AS.PlayOneShot(SE[0]);
            /*播放音效*/

            /*敵人受擊動畫*/
            Enemy0_Img.transform.DOShakePosition(1,10,100,90);             
            /*敵人受擊動畫*/


        }//Atk
        if (PlayerActionType == 2)
        {
            if (BSM.IsSwordsmanTurn)
            {
                SwordsmanShield = FinalDefValue;
                SwordsmanDefImg.fillAmount = 1;
            }
            else
            {
               /* SwordsmanShield = 0;
                SwordsmanDefImg.fillAmount = 0;*/
            }
            if (BSM.IsKnightTurn)
            {
                KnightShield = FinalDefValue;
                KnightFocusImg.fillAmount = 1;
            }
            else
            {
               /* KnightShield = 0;
                KnightFocusImg.fillAmount = 0;*/
            }
            if (BSM.IsMagicianTurn)
            {
                MagicianShield = FinalDefValue;
                MagicianDefImg.fillAmount = 1;
            }
            else
            {
                /*MagicianShield = 0;
                MagicianDefImg.fillAmount = 0;*/
            }
            if (BSM.IsHealerTurn)
            {
                HealerShield = FinalDefValue;
                HealerDefImg.fillAmount = 1;
            }
            else
            {
                /*HealerShield = 0;
                HealerDefImg.fillAmount = 0;*/
            }

            /*播放音效*/
            SE_AS.PlayOneShot(SE[1]);
            /*播放音效*/

        }//Def
        if (PlayerActionType == 3)
        {
            Enemy0_Hp -= FinalMagicValue;
            Enemy0_HpImg.fillAmount = Enemy0_Hp / Enemy0_MaxHp;
            Enemy0_HpText.text = Enemy0_Hp.ToString();

            /*傷害值動畫*/
            Enemy0_Damage.text = FinalMagicValue.ToString();
            Enemy0_Damage.DOFade(1, 0.5f);
            Enemy0_Damage.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
            Enemy0_Damage.transform.DOLocalPath(Enemy0_DamagePath, 0.5f);
            Enemy0_Damage.DOFade(0, 0.5f).SetDelay(1f);
            Enemy0_Damage.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
            /*傷害值動畫*/

            /*傷害特效*/
            Enemy0_GetHurt.DOFade(1, 0.125f);
            Enemy0_GetHurt.DOFade(0, 0.125f).SetDelay(0.25f);
            /*傷害特效*/


            if (BSM.IsSwordsmanTurn)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(SwordsmanAC[Random.Range(0, 2)]);
                /*播放音效*/
            }
            if (BSM.IsKnightTurn)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(KnightAC[Random.Range(0, 3)]);
                /*播放音效*/
            }
            if (BSM.IsMagicianTurn)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(MagicianAC[Random.Range(0, 2)]);
                /*播放音效*/
            }
            if (BSM.IsHealerTurn)
            {
                /*播放音效*/
                PlayerAS.PlayOneShot(HealerAC[Random.Range(0, 2)]);
                /*播放音效*/
            }

            /*播放音效*/
            SE_AS.PlayOneShot(SE[2]);
            /*播放音效*/

            /*敵人受擊動畫*/
            Enemy0_Img.transform.DOShakePosition(1, 10, 100, 90);
            /*敵人受擊動畫*/

        }//Magic
        if (PlayerActionType == 4)
        {

            if (BSM.IsSwordsmanTurn)
            {
                SwordsmanHp += FinalHealValue;
                if(SwordsmanHp > SwordsmanMaxHp)
                {
                    SwordsmanHp = SwordsmanMaxHp;
                }

                SwordsmanHpImg.fillAmount = SwordsmanHp / SwordsmanMaxHp;
                SwordsmanHpText.text = SwordsmanHp.ToString();

                /*治療值動畫*/
                SwordsmanHeal.text = FinalHealValue.ToString();
                SwordsmanHeal.DOFade(1, 0.5f);
                SwordsmanHeal.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                SwordsmanHeal.transform.DOLocalPath(PlayerHealPath, 0.5f);
                SwordsmanHeal.DOFade(0, 0.5f).SetDelay(1f);
                SwordsmanHeal.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                /*治療值動畫*/

            }
            if (BSM.IsKnightTurn)
            {
                KnightHp += FinalHealValue;

                if(KnightHp > KnightMaxHp)
                {
                    KnightHp = KnightMaxHp;
                }

                KnightHpImg.fillAmount = KnightHp / KnightMaxHp;
                KnightHpText.text = KnightHp.ToString();

                /*治療值動畫*/
                KnightHeal.text = FinalHealValue.ToString();
                KnightHeal.DOFade(1, 0.5f);
                KnightHeal.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                KnightHeal.transform.DOLocalPath(PlayerHealPath, 0.5f);
                KnightHeal.DOFade(0, 0.5f).SetDelay(1f);
                KnightHeal.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                /*治療值動畫*/
            }
            if (BSM.IsMagicianTurn)
            {
                MagicianHp += FinalHealValue;

                if(MagicianHp > MagicianMaxHp)
                {
                    MagicianHp = MagicianMaxHp;
                }

                MagicianHpImg.fillAmount = MagicianHp / MagicianMaxHp;
                MagicianHpText.text = MagicianHp.ToString();

                /*治療值動畫*/
                MagicianHeal.text = FinalHealValue.ToString();
                MagicianHeal.DOFade(1, 0.5f);
                MagicianHeal.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                MagicianHeal.transform.DOLocalPath(PlayerHealPath, 0.5f);
                MagicianHeal.DOFade(0, 0.5f).SetDelay(1f);
                MagicianHeal.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                /*治療值動畫*/

            }
            if (BSM.IsHealerTurn)
            {

                if (!IsSwordsmanDead)
                {
                    SwordsmanHp += FinalHealValue;
                    if (SwordsmanHp > SwordsmanMaxHp)
                    {
                        SwordsmanHp = SwordsmanMaxHp;
                    }
                    SwordsmanHpImg.fillAmount = SwordsmanHp / SwordsmanMaxHp;
                    SwordsmanHpText.text = SwordsmanHp.ToString();
                    /*治療值動畫*/
                    SwordsmanHeal.text = FinalHealValue.ToString();
                    SwordsmanHeal.DOFade(1, 0.5f);
                    SwordsmanHeal.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                    SwordsmanHeal.transform.DOLocalPath(PlayerHealPath, 0.5f);
                    SwordsmanHeal.DOFade(0, 0.5f).SetDelay(1f);
                    SwordsmanHeal.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                    /*治療值動畫*/
                }

                if (!IsKnightDead)
                {
                    KnightHp += FinalHealValue;
                    if (KnightHp > KnightMaxHp)
                    {
                        KnightHp = KnightMaxHp;
                    }
                    KnightHpImg.fillAmount = KnightHp / KnightMaxHp;
                    KnightHpText.text = KnightHp.ToString();
                    /*治療值動畫*/
                    KnightHeal.text = FinalHealValue.ToString();
                    KnightHeal.DOFade(1, 0.5f);
                    KnightHeal.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                    KnightHeal.transform.DOLocalPath(PlayerHealPath, 0.5f);
                    KnightHeal.DOFade(0, 0.5f).SetDelay(1f);
                    KnightHeal.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                    /*治療值動畫*/
                }

                if (!IsMagicianDead)
                {
                    MagicianHp += FinalHealValue;
                    if (MagicianHp > MagicianMaxHp)
                    {
                        MagicianHp = MagicianMaxHp;
                    }
                    MagicianHpImg.fillAmount = MagicianHp / MagicianMaxHp;
                    MagicianHpText.text = MagicianHp.ToString();
                    /*治療值動畫*/
                    MagicianHeal.text = FinalHealValue.ToString();
                    MagicianHeal.DOFade(1, 0.5f);
                    MagicianHeal.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                    MagicianHeal.transform.DOLocalPath(PlayerHealPath, 0.5f);
                    MagicianHeal.DOFade(0, 0.5f).SetDelay(1f);
                    MagicianHeal.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                    /*治療值動畫*/
                }

                HealerHp += FinalHealValue;
                if(HealerHp > HealerMaxHp)
                {
                    HealerHp = HealerMaxHp;
                }
                HealerHpImg.fillAmount = HealerHp / HealerMaxHp;
                HealerHpText.text = HealerHp.ToString();
                /*治療值動畫*/
                HealerHeal.text = FinalHealValue.ToString();
                HealerHeal.DOFade(1, 0.5f);
                HealerHeal.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                HealerHeal.transform.DOLocalPath(PlayerHealPath, 0.5f);
                HealerHeal.DOFade(0, 0.5f).SetDelay(1f);
                HealerHeal.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                /*治療值動畫*/

                /*播放音效*/
                PlayerAS.PlayOneShot(HealerAC[4]);
                /*播放音效*/
            }

            /*播放音效*/
            SE_AS.PlayOneShot(SE[3]);
            /*播放音效*/

        }//Heal

        BSM.IsSwordsmanTurn = false;
        BSM.IsKnightTurn = false;
        BSM.IsMagicianTurn = false;
        BSM.IsHealerTurn = false;



        PlayerIsPressing = false;
        Timer_Time = Timer_MaxTime;
        IsTimeOut = false;

        BSM.IsPlayerTurn = false;

    }

    public void SwordsmanButtonClick()
    {
        PlayerPress.Add(1);
        if(PlayerPress[NowPressNum] == BattlePanelRequest[NowPressNum])
        {
            BattlePanelImageList[NowPressNum].color = new Color32(0,255,0,150);
            BattlePanelImageList[NowPressNum].transform.DOScale(new Vector3(1.1f,1.1f,0),0.25f);
            PressRightAtkBtn++;
        }//Press Right
        else
        {
            BattlePanelImageList[NowPressNum].color = new Color32(255,0,0,150);
            BattlePanelImageList[NowPressNum].transform.DOScale(new Vector3(0.9f,0.9f,0),0.25f);
            PressWrongBtn += 1;
        }//Press Wrong
        NowPressNum++;

        /*播放音效*/
        SE_AS.PlayOneShot(SE[4]);
        /*播放音效*/
    }

    public void KnightButtonClick()
    {
        PlayerPress.Add(2);
        if (PlayerPress[NowPressNum] == BattlePanelRequest[NowPressNum])
        {
            BattlePanelImageList[NowPressNum].color = new Color32(0, 255, 0, 150);
            BattlePanelImageList[NowPressNum].transform.DOScale(new Vector3(1.1f, 1.1f, 0), 0.25f);
            PressRightDefBtn++;
        }//Press Right
        else
        {
            BattlePanelImageList[NowPressNum].color = new Color32(255, 0, 0, 150);
            BattlePanelImageList[NowPressNum].transform.DOScale(new Vector3(0.9f, 0.9f, 0), 0.25f);
            PressWrongBtn += 1;
        }//Press Wrong
        NowPressNum++;

        /*播放音效*/
        SE_AS.PlayOneShot(SE[4]);
        /*播放音效*/
    }

    public void MagicianButtonClick()
    {
        PlayerPress.Add(3);
        if (PlayerPress[NowPressNum] == BattlePanelRequest[NowPressNum])
        {
            BattlePanelImageList[NowPressNum].color = new Color32(0, 255, 0, 150);
            BattlePanelImageList[NowPressNum].transform.DOScale(new Vector3(1.1f, 1.1f, 0), 0.25f);
            PressRightMagicBtn++;
        }//Press Right
        else
        {
            BattlePanelImageList[NowPressNum].color = new Color32(255, 0, 0, 150);
            BattlePanelImageList[NowPressNum].transform.DOScale(new Vector3(0.9f, 0.9f, 0), 0.25f);
            PressWrongBtn += 1;
        }//Press Wrong
        NowPressNum++;

        /*播放音效*/
        SE_AS.PlayOneShot(SE[4]);
        /*播放音效*/
    }

    public void HealerButtonClick()
    {
        PlayerPress.Add(4);
        if (PlayerPress[NowPressNum] == BattlePanelRequest[NowPressNum])
        {
            BattlePanelImageList[NowPressNum].color = new Color32(0, 255, 0, 150);
            BattlePanelImageList[NowPressNum].transform.DOScale(new Vector3(1.1f, 1.1f, 0), 0.25f);
            PressRightHealBtn++;
        }//Press Right
        else
        {
            BattlePanelImageList[NowPressNum].color = new Color32(255, 0, 0, 150);
            BattlePanelImageList[NowPressNum].transform.DOScale(new Vector3(0.9f, 0.9f, 0), 0.25f);
            PressWrongBtn += 1;
        }//Press Wrong
        NowPressNum++;

        /*播放音效*/
        SE_AS.PlayOneShot(SE[4]);
        /*播放音效*/
    }

    void ResetAllBattleValue()
    {



        BattlePanelRequest.Clear();
        BattlePanelImageList.Clear();
        PlayerPress.Clear();
        NowPressNum = 0;        

        PressRightAtkBtn = 0;
        PressRightDefBtn = 0;
        PressRightMagicBtn = 0;
        PressRightHealBtn = 0;

        FinalAtkValue = 0;
        FinalDefValue = 0;
        FinalMagicValue = 0;
        FinalHealValue = 0;

        PlayerActionType = 0;

        IsPlayerPressDone = false;


    }

    public void AtkSelect()
    {
        ResetAllBattleValue();

        PlayerIsPressing = true;

        BattlePanel.GetComponent<CanvasGroup>().DOFade(1, 0.25f);

        BSM.BattleControl.transform.DOLocalRotate(new Vector3(0, 0, -45), 0.5f);
        BSM.BattleControl.transform.DOScale(new Vector3(0.55f, 0.55f, 0), 0.5f);


        /* BSM.ActionSelect.GetComponent<CanvasGroup>().alpha = 0;
         BSM.ActionSelect.SetActive(false);*/
        BSM.ActionSelect.transform.DOLocalMove(new Vector3(1160, 190, 0), 0.5f);

        BSM.BattleControl.SetActive(true);
        BSM.BattleControl.GetComponent<CanvasGroup>().alpha = 255;

        RandomCreateBattlePanel();

        PlayerActionType = 1;

        /*播放音效*/
        SE_AS.PlayOneShot(SE[5]);
        /*播放音效*/
    }
    public void DefSelect()
    {
        ResetAllBattleValue();

        PlayerIsPressing = true;

        BattlePanel.GetComponent<CanvasGroup>().DOFade(1, 0.25f);

        BSM.BattleControl.transform.DOLocalRotate(new Vector3(0, 0, -45), 0.5f);
        BSM.BattleControl.transform.DOScale(new Vector3(0.55f, 0.55f, 0), 0.5f);

        /*BSM.ActionSelect.GetComponent<CanvasGroup>().alpha = 0;
        BSM.ActionSelect.SetActive(false);*/
        BSM.ActionSelect.transform.DOLocalMove(new Vector3(1160, 190, 0), 0.5f);

        BSM.BattleControl.SetActive(true);
        BSM.BattleControl.GetComponent<CanvasGroup>().alpha = 255;

        RandomCreateBattlePanel();

        PlayerActionType = 2;

        if (BSM.IsKnightTurn)
        {
            IsFocus = true;
        }


        /*播放音效*/
        SE_AS.PlayOneShot(SE[5]);
        /*播放音效*/
    }
    public void MagicSelect()
    {
        ResetAllBattleValue();

        PlayerIsPressing = true;

        BattlePanel.GetComponent<CanvasGroup>().DOFade(1, 0.25f);

        BSM.BattleControl.transform.DOLocalRotate(new Vector3(0, 0, -45), 0.5f);
        BSM.BattleControl.transform.DOScale(new Vector3(0.55f, 0.55f, 0), 0.5f);

        /*BSM.ActionSelect.GetComponent<CanvasGroup>().alpha = 0;
        BSM.ActionSelect.SetActive(false);*/
        BSM.ActionSelect.transform.DOLocalMove(new Vector3(1160, 190, 0), 0.5f);


        BSM.BattleControl.SetActive(true);
        BSM.BattleControl.GetComponent<CanvasGroup>().alpha = 255;

        RandomCreateBattlePanel();

        PlayerActionType = 3;

        /*播放音效*/
        SE_AS.PlayOneShot(SE[5]);
        /*播放音效*/
    }
    public void HealSelect()
    {
        ResetAllBattleValue();

        PlayerIsPressing = true;

        BattlePanel.GetComponent<CanvasGroup>().DOFade(1, 0.25f);

        BSM.BattleControl.transform.DOLocalRotate(new Vector3(0, 0, -45), 0.5f);
        BSM.BattleControl.transform.DOScale(new Vector3(0.55f, 0.55f, 0), 0.5f);

        /* BSM.ActionSelect.GetComponent<CanvasGroup>().alpha = 0;
         BSM.ActionSelect.SetActive(false);*/
        BSM.ActionSelect.transform.DOLocalMove(new Vector3(1160, 190, 0), 0.5f);


        BSM.BattleControl.SetActive(true);
        BSM.BattleControl.GetComponent<CanvasGroup>().alpha = 255;

        RandomCreateBattlePanel();

        PlayerActionType = 4;

        /*播放音效*/
        SE_AS.PlayOneShot(SE[5]);
        /*播放音效*/

    }


    public void RestartOnClick()
    {
        StartCoroutine("DelayRestart");
    }

    IEnumerator DelayRestart()
    {
        FadeImage.DOFade(1, 0.5f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }

    public void ReturnOnClick()
    {
        StartCoroutine("DelayReturn");
    }

    IEnumerator DelayReturn()
    {
        FadeImage.DOFade(1, 0.5f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    bool IsEnemyAtkAnimation = false;

    public void EnemyAction()
    {
        if (!IsEnemyAtkAnimation)
        {
            StartCoroutine("EnemyAtkAnimation");
        }
       
    }// 敵人的動作

    bool IsTargetDead = true;

    IEnumerator EnemyAtkAnimation()
    {
        IsEnemyAtkAnimation = true;
        
        Enemy0_RandomSkillValue = Random.Range(0, 100);

        if (Enemy0_IsSkill)
        {
            Enemy0_SkillAtkBuff = Random.Range(80, 150);
            Enemy0_Atk = Enemy0_SkillAtkBuff + 5*PressWrongBtn;
        }
        else
        {
            Enemy0_Atk = Random.Range(50, 75) + 5*PressWrongBtn;
        }

        //Debug.Log("EnemyAtk: " + Enemy0_Atk);

        if (!IsFocus)
        {
            IsTargetDead = true;

            while (IsTargetDead)
            {
                Enemy0_AtkSelect = Random.Range(1, 5);
                IsTargetDead = false;

                if (Enemy0_AtkSelect == 1 && IsSwordsmanDead)
                {
                    IsTargetDead = true;
                }
                if(Enemy0_AtkSelect == 2 && IsKnightDead)
                {
                    IsTargetDead = true;
                }
                if (Enemy0_AtkSelect == 3 && IsMagicianDead)
                {
                    IsTargetDead = true;
                }
                if (Enemy0_AtkSelect == 4 && IsHealerDead)
                {
                    IsTargetDead = true;
                }

                Debug.Log("EnemyTarget: " + Enemy0_AtkSelect);
            }

            if (Enemy0_AtkSelect == 1)
            {
                if (Enemy0_Atk > SwordsmanShield)
                {
                    SwordsmanHp -= (Enemy0_Atk - SwordsmanShield);
                }
                else if(Enemy0_Atk < SwordsmanShield)
                {
                    SwordsmanHp -= 0;
                }//block atk

                SwordsmanHpImg.fillAmount = SwordsmanHp / SwordsmanMaxHp;
                SwordsmanHpText.text = SwordsmanHp.ToString();

                /*被攻擊後重置防禦動作*/
                PlayerActionType = 0;
                SwordsmanShield = 0;
                SwordsmanDefImg.fillAmount = 0;
                /*被攻擊後重置防禦動作*/

                /*傷害值動畫*/
                SwordsmanDamage.text = Enemy0_Atk.ToString();
                SwordsmanDamage.DOFade(1, 0.5f);
                SwordsmanDamage.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                SwordsmanDamage.transform.DOLocalPath(SwordsDamagePath, 0.5f);
                SwordsmanDamage.DOFade(0, 0.5f).SetDelay(1f);
                SwordsmanDamage.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                /*傷害值動畫*/

                /*傷害特效*/
                SwordsmanGetHurt.DOFade(1,0.125f);    
                SwordsmanGetHurt.DOFade(0,0.125f).SetDelay(0.25f);
                /*傷害特效*/

                /*播放音效*/
                PlayerAS.PlayOneShot(SwordsmanAC[Random.Range(2, 4)]);
                /*播放音效*/

                /*受擊動畫*/
                SwordsmanImg.transform.DOShakePosition(1, 10, 100, 90);
                /*受擊動畫*/

            }
            if (Enemy0_AtkSelect == 2)
            {
                if(Enemy0_Atk > KnightShield)
                {
                    KnightHp -= (Enemy0_Atk - KnightShield);
                }
                else if(Enemy0_Atk < KnightShield)
                {
                    KnightHp -= 0;
                }//block atk
                
                KnightHpImg.fillAmount = KnightHp / KnightMaxHp;
                KnightHpText.text = KnightHp.ToString();

                /*被攻擊後重置防禦動作*/
                PlayerActionType = 0;
                KnightShield = 0;
                KnightFocusImg.fillAmount = 0;
                /*被攻擊後重置防禦動作*/

                /*傷害值動畫*/
                KnightDamage.text = Enemy0_Atk.ToString();
                KnightDamage.DOFade(1, 0.5f);
                KnightDamage.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                KnightDamage.transform.DOLocalPath(KnightDamagePath, 0.5f);
                KnightDamage.DOFade(0, 0.5f).SetDelay(1f);
                KnightDamage.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                /*傷害值動畫*/

                /*傷害特效*/
                KnightGetHurt.DOFade(1, 0.125f);
                KnightGetHurt.DOFade(0, 0.125f).SetDelay(0.25f);
                /*傷害特效*/

                /*播放音效*/
                PlayerAS.PlayOneShot(KnightAC[Random.Range(3, 5)]);
                /*播放音效*/

                /*受擊動畫*/
                KnightImg.transform.DOShakePosition(1, 10, 100, 90);
                /*受擊動畫*/
            }
            if (Enemy0_AtkSelect == 3)
            {
                if(Enemy0_Atk > MagicianShield)
                {
                    MagicianHp -= (Enemy0_Atk - MagicianShield);
                }
                else if (Enemy0_Atk < MagicianShield)
                {
                    MagicianHp -= 0;
                }
                MagicianHpImg.fillAmount = MagicianHp / MagicianMaxHp;
                MagicianHpText.text = MagicianHp.ToString();

                /*被攻擊後重置防禦動作*/
                PlayerActionType = 0;
                MagicianShield = 0;
                MagicianDefImg.fillAmount = 0;
                /*被攻擊後重置防禦動作*/

                /*傷害值動畫*/
                MagicianDamage.text = Enemy0_Atk.ToString();
                MagicianDamage.DOFade(1, 0.5f);
                MagicianDamage.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                MagicianDamage.transform.DOLocalPath(MagicianDamagePath, 0.5f);
                MagicianDamage.DOFade(0, 0.5f).SetDelay(1f);
                MagicianDamage.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                /*傷害值動畫*/

                /*傷害特效*/
                MagicianGetHurt.DOFade(1, 0.125f);
                MagicianGetHurt.DOFade(0, 0.125f).SetDelay(0.25f);
                /*傷害特效*/

                /*播放音效*/
                PlayerAS.PlayOneShot(MagicianAC[Random.Range(2, 4)]);
                /*播放音效*/

                /*受擊動畫*/
                MagicianImg.transform.DOShakePosition(1, 10, 100, 90);
                /*受擊動畫*/
            }
            if (Enemy0_AtkSelect == 4)
            {
               if(Enemy0_Atk > HealerShield)
                {
                    HealerHp -= (Enemy0_Atk - HealerShield);
                }
               else if(Enemy0_Atk < HealerShield)
                {
                    HealerHp -= 0;
                }
                HealerHpImg.fillAmount = HealerHp / HealerMaxHp;
                HealerHpText.text = HealerHp.ToString();

                /*被攻擊後重置防禦動作*/
                PlayerActionType = 0;
                HealerShield = 0;
                HealerDefImg.fillAmount = 0;
                /*被攻擊後重置防禦動作*/

                /*傷害值動畫*/
                HealerDamage.text = Enemy0_Atk.ToString();
                HealerDamage.DOFade(1, 0.5f);
                HealerDamage.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
                HealerDamage.transform.DOLocalPath(HealerDamagePath, 0.5f);
                HealerDamage.DOFade(0, 0.5f).SetDelay(1f);
                HealerDamage.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
                /*傷害值動畫*/

                /*傷害特效*/
                HealerGetHurt.DOFade(1, 0.125f);
                HealerGetHurt.DOFade(0, 0.125f).SetDelay(0.25f);
                /*傷害特效*/

                /*播放音效*/
                PlayerAS.PlayOneShot(HealerAC[Random.Range(2, 4)]);
                /*播放音效*/

                /*受擊動畫*/
                HealerImg.transform.DOShakePosition(1, 10, 100, 90);
                /*受擊動畫*/
            }

        }
        else if (IsFocus)
        {
            Enemy0_AtkSelect = 2;
            KnightHp -= Enemy0_Atk;
            KnightHpImg.fillAmount = KnightHp / KnightMaxHp;
            KnightHpText.text = KnightHp.ToString();
            IsFocus = false;
            /*被攻擊後重置防禦動作*/
            PlayerActionType = 0;
            KnightShield = 0;
            KnightFocusImg.fillAmount = 0;
            /*被攻擊後重置防禦動作*/

            /*傷害值動畫*/
            KnightDamage.text = Enemy0_Atk.ToString();
            KnightDamage.DOFade(1, 0.5f);
            KnightDamage.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
            KnightDamage.transform.DOLocalPath(KnightDamagePath, 0.5f);
            KnightDamage.DOFade(0, 0.5f).SetDelay(1f);
            KnightDamage.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(1.5f);
            /*傷害值動畫*/

            /*傷害特效*/
            KnightGetHurt.DOFade(1, 0.125f);
            KnightGetHurt.DOFade(0, 0.125f).SetDelay(0.25f);
            /*傷害特效*/

            /*播放音效*/
            PlayerAS.PlayOneShot(KnightAC[Random.Range(3, 5)]);
            /*播放音效*/

            /*受擊動畫*/
            KnightImg.transform.DOShakePosition(1, 10, 100, 90);
            /*受擊動畫*/
        }

        /*播放音效*/
        Enemy0_AS.volume = 1f;
        Enemy0_AS.PlayOneShot(SE[0]);
        yield return new WaitForSeconds(0.25f);
        Enemy0_AS.volume = 0.4f;
        Enemy0_AS.PlayOneShot(Enemy0_AC[Random.Range(0, 2)]);
        /*播放音效*/


        yield return new WaitForSeconds(1f);
        PressWrongBtn = 0;
        BSM.IsEnemyTurn = false;
        BSM.EnemySpeedImg_0.transform.localPosition = new Vector3(0, 0, 0);
        IsEnemyAtkAnimation = false;
        Enemy0_IsSkill = false;
        Enemy0_SkillImg.fillAmount = 0;

        if (Enemy0_RandomSkillValue >70)
        {
            Enemy0_IsSkill = true;
            Enemy0_SkillImg.fillAmount = 1;
        }

    }

}
