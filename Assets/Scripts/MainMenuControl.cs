using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using DG.Tweening.Plugins.Options;

public class MainMenuControl : MonoBehaviour {

    [Header("關卡數值")]
    public int NowOpenStage;

    [Header("抓取場景物件")]
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject Stage4;
    public GameObject StageInfo;
    public Image Effect1;
    public Image FadeImage;
    public bool IsEffect1Ani = false;

	// Use this for initialization
	void Start () {
        ChangeStageState();
    }
	
	// Update is called once per frame
	void Update () {
        if (!IsEffect1Ani)
        {
            StartCoroutine("Effect1Animation");
        }
	}

    void ChangeStageState()
    {

        FadeImage.DOFade(0,0.5f);

        if(NowOpenStage == 1)
        {
            Stage1.SetActive(true);
            Stage2.SetActive(false);
            Stage3.SetActive(false);
            Stage4.SetActive(false);
        }
        if (NowOpenStage == 2)
        {
            Stage1.SetActive(false);
            Stage2.SetActive(true);
            Stage3.SetActive(false);
            Stage4.SetActive(false);
        }
        if (NowOpenStage == 3)
        {
            Stage1.SetActive(false);
            Stage2.SetActive(false);
            Stage3.SetActive(true);
            Stage4.SetActive(false);
        }
        if (NowOpenStage == 4)
        {
            Stage1.SetActive(false);
            Stage2.SetActive(false);
            Stage3.SetActive(false);
            Stage4.SetActive(true);
        }

    }

    IEnumerator Effect1Animation()
    {
        IsEffect1Ani = true;
        Effect1.transform.DOScale(new Vector3(0.17f, 0.17f, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        Effect1.transform.DOScale(new Vector3(0.13f, 0.13f, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        IsEffect1Ani = false;

    }

    public void StartStageOnClick()
    {
        if(NowOpenStage == 1)
        {
            StartCoroutine("DelayLoadScene");
        }
    }

    IEnumerator DelayLoadScene()
    {
        FadeImage.DOFade(1, 0.5f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    public void Stage1BtnOnClick()
    {
        StageInfo.GetComponent<RectTransform>().DOScaleX(1, 0.5f);
        //Debug.Log("Click");
    }

}
