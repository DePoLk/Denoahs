using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using DG.Tweening.Plugins.Options;

public class StartGamePageControl : MonoBehaviour
{
    [Header("須帶入的物件")]
    public Image TittleText;
    public Image PressToStartText;
    public Image FadeOutImg;

    [Header("監測值")]
    public bool TittleIsAni = false;
    public bool PressIsAni = false;

   

    // Use this for initialization
    void Start()
    {
        FadeOutImg.DOFade(0, 2f);   
    }

    // Update is called once per frame
    void Update()
    {
        if (!TittleIsAni)
        {
            StartCoroutine("TittleAnimation");
        }
        if (!PressIsAni)
        {
            StartCoroutine("PressAnimation");
        }
    }


    IEnumerator TittleAnimation()
    {
        TittleIsAni = true;

        yield return new WaitForSeconds(0.025f);
        TittleText.transform.localPosition += new Vector3(100 ,100 ,0);
        yield return new WaitForSeconds(0.025f);
        TittleText.transform.localPosition -= new Vector3(100, 100, 0);
        yield return new WaitForSeconds(0.025f);
        TittleText.transform.localPosition += new Vector3(-50, 50, 0);
        yield return new WaitForSeconds(0.025f);
        TittleText.transform.localPosition -= new Vector3(-50, 50, 0);
        yield return new WaitForSeconds(Random.Range(3,6));

        TittleIsAni = false;
    }

    IEnumerator PressAnimation()
    {
        PressIsAni = true;

        PressToStartText.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.5f);
        PressToStartText.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        PressIsAni = false;

    }

    IEnumerator ChangeScene()
    {
        FadeOutImg.DOFade(1, 1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);//load MainMenu
    }

    public void OnStartGamePageClick()
    {

        //Debug 

        //GameObject.Find("StartGamePageBG").GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        Debug.Log("點擊StartPage");
        //Debug

        StartCoroutine("ChangeScene");

    }

}
