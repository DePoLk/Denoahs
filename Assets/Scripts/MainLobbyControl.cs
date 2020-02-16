using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using DG.Tweening.Plugins.Options;


public class MainLobbyControl : MonoBehaviour {

    public Image FadeImage;      

    // Use this for initialization
    void Start () {
        FadeImage.DOFade(0, 0.5f);
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void BattleBtnOnClick()
    {
        StartCoroutine("ChangeScene");
    }

    IEnumerator ChangeScene()
    {
        Debug.Log("ChangingScene");
        FadeImage.DOFade(1, 1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);//load MainMenu
    }

}
