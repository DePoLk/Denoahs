using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Plugins.Options;

public class BattleSceneManager : MonoBehaviour {

    BattleControlEvent BCE;

    [Header("場景監測值")]
    public bool IsPlayerTurn = false;
    public bool IsEnemyTurn = false;

    public bool IsSwordsmanTurn = false;
    public bool IsKnightTurn = false;
    public bool IsMagicianTurn = false;
    public bool IsHealerTurn = false;

    [Header("速度條數值")]
    public bool IsSpeedBarAni = false;
    public float SwordsmanSpeedValue = 0f;
    public float KnightSpeedValue = 0f;
    public float MagicianSpeedValue = 0f;
    public float HealerSpeedValue = 0f;

    public float EnemySpeedValue_0 = 0f;

    [Header("場景操控物件")]
    public Image SwordsmanSpeedImg;
    public Image KnightSpeedImg;
    public Image MagicianSpeedImg;
    public Image HealerSpeedImg;

    public Image EnemySpeedImg_0;

    public GameObject BattleControl;
    public GameObject ActionSelect;
	// Use this for initialization
	void Start () {
        BCE = FindObjectOfType<BattleControlEvent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (BCE.IsTeachDone)
        {
            BattleStateCheck();
        }
	}

    IEnumerator SpeedBarState()
    {
        IsSpeedBarAni = true;
        Debug.Log("SpeedBarAni");
        //yield return new WaitForSeconds(1f);

        // PlayerSpeed Start

        if (SwordsmanSpeedImg.transform.localPosition.y > -700f)
        {
            yield return new WaitForSeconds(0.0001f);           
            SwordsmanSpeedImg.transform.localPosition -= new Vector3(0,SwordsmanSpeedValue,0);

            BCE.SwordsmanImg.transform.DOLocalMoveY (50, 0.5f);//縮小正在跑條的角色圖

        }
        else if(SwordsmanSpeedImg.transform.localPosition.y <= -700f)
        {
            SwordsmanSpeedImg.transform.localPosition = new Vector3(0, -700,0);
            IsPlayerTurn = true;
            IsSwordsmanTurn = true;

            BCE.SwordsmanImg.transform.DOLocalMoveY(80, 0.5f);//放大正在行動的角色圖
            //Debug.Log("SwordsmanTurn");

        }//Swordsman

        if (KnightSpeedImg.transform.localPosition.y > -700f)
        {
            yield return new WaitForSeconds(0.0001f);
            KnightSpeedImg.transform.localPosition -= new Vector3(0, KnightSpeedValue, 0);

            BCE.KnightImg.transform.DOLocalMoveY(50, 0.5f);//縮小正在跑條的角色圖

        }
        else if (KnightSpeedImg.transform.localPosition.y <= -700f)
        {
            KnightSpeedImg.transform.localPosition = new Vector3(0, -700, 0);
            IsPlayerTurn = true;
            IsKnightTurn = true;

            BCE.KnightImg.transform.DOLocalMoveY(80, 0.5f);//放大正在行動的角色圖
            //Debug.Log("KnightTurn");


        }//Knight

        if (MagicianSpeedImg.transform.localPosition.y > -700f)
        {
            yield return new WaitForSeconds(0.0001f);
            MagicianSpeedImg.transform.localPosition -= new Vector3(0, MagicianSpeedValue, 0);

            BCE.MagicianImg.transform.DOLocalMoveY(50, 0.5f);//縮小正在跑條的角色圖

        }
        else if (MagicianSpeedImg.transform.localPosition.y <= -700f)
        {
            MagicianSpeedImg.transform.localPosition = new Vector3(0, -700, 0);
            IsPlayerTurn = true;
            IsMagicianTurn = true;

            BCE.MagicianImg.transform.DOLocalMoveY(80, 0.5f);//放大正在行動的角色圖
            //Debug.Log("MagicianTurn");

        }//Magician

        if (HealerSpeedImg.transform.localPosition.y > -700f)
        {
            yield return new WaitForSeconds(0.0001f);
            HealerSpeedImg.transform.localPosition -= new Vector3(0, HealerSpeedValue, 0);

            BCE.HealerImg.transform.DOLocalMoveY(50, 0.5f);//縮小正在跑條的角色圖

        }
        else if (HealerSpeedImg.transform.localPosition.y <= -700f)
        {
            HealerSpeedImg.transform.localPosition = new Vector3(0, -700, 0);
            IsPlayerTurn = true;
            IsHealerTurn = true;

            BCE.HealerImg.transform.DOLocalMoveY(80, 0.5f);//放大正在行動的角色圖
            //Debug.Log("HealernTurn");

        }//Healer

        // PlayerSpeed End

        //Enemy Start

        if (EnemySpeedImg_0.transform.localPosition.y > -700f)
        {
            yield return new WaitForSeconds(0.0001f);
            EnemySpeedImg_0.transform.localPosition -= new Vector3(0, EnemySpeedValue_0, 0);
        }
        else if (EnemySpeedImg_0.transform.localPosition.y <= -700f)
        {
            EnemySpeedImg_0.transform.localPosition = new Vector3(0, -700, 0);
            IsEnemyTurn = true;
        }

        //Enemy End

        IsSpeedBarAni = false;

    }// 速度條的更新


    bool DefaultActionSelect = false;

    void BattleStateCheck()
    {

        if(!IsPlayerTurn && !IsEnemyTurn)
        {
            /*控制區動畫*/

            /*BattleControl.GetComponent<CanvasGroup>().alpha = 0;
            BattleControl.SetActive(false);*/
            BattleControl.transform.DOLocalRotate(new Vector3(0,0,0), 0.5f);
            BattleControl.transform.DOScale(new Vector3(0, 0, 0), 0.5f);

            /*ActionSelect.GetComponent<CanvasGroup>().alpha = 0;
            ActionSelect.SetActive(false);*/
            ActionSelect.transform.DOLocalMove(new Vector3(1160,190,0),0.5f);

            BCE.BattlePanel.GetComponent<CanvasGroup>().DOFade(0,0.25f);

            /*控制區動畫*/

            DefaultActionSelect = false;

            if (!IsSpeedBarAni)
            {
                StartCoroutine("SpeedBarState");
            }
        }// 雙方都無法攻擊時

        if(IsPlayerTurn && IsEnemyTurn)
        {

        }// 雙方同時可以行動

        if(IsPlayerTurn && !IsEnemyTurn)
        {
            /*BattleControl.SetActive(true);
            BattleControl.GetComponent<CanvasGroup>().alpha = 255;*/

            if (IsSwordsmanTurn || IsKnightTurn || IsMagicianTurn || IsHealerTurn)
            {
                if (!DefaultActionSelect)
                {

                    /*控制區動畫*/

                    /*ActionSelect.SetActive(true);
                    ActionSelect.GetComponent<CanvasGroup>().alpha = 255;*/

                    ActionSelect.transform.DOLocalMove(new Vector3(905, 190, 0), 0.5f);
                    

                    /*控制區動畫*/


                    DefaultActionSelect = true;
                }
            }



        }// 玩家的回合

        if (!IsPlayerTurn && IsEnemyTurn)
        {
            BCE.EnemyAction();
        }// 敵人的回合

    }// 檢查是誰的回合


    

}
