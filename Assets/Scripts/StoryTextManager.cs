using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;
using DG.Tweening.Plugins.Options;

public class StoryTextManager : MonoBehaviour {
    [Header("關卡文本監測值")]
    public int AllStageCount;
    public int StageId = 0;
    public int StoryTellNum = 0;
    public int[] StoryIndex; // 當前關卡的故事文本的索引起點    
    public int[] EachStoryContentCount; // 每個關卡的內容長度
    [Header("文本監測值")]
    [SerializeField]
    public string[] StoryText;
    int StoryTextAllNum = 19;
    [Header("當前關卡文本監測")]
    public string[] NowTellerPos;
    public string[] NowTellerName;
    public string[] NowTellerImg;
    public string[] NowStageStoryText;
    [Header("UI對話框狀態")]
    public bool IsTalking = true;
    public int NowDialogIndex = 0;
    public Image DialogBox;
    public Image TellerImgLeft;
    public Image TellerImgRight;
    public Text TellerName;
    public Text TellerDialog;
  

    // Use this for initialization
    void Start () {

        /*初始化數據*/
        StoryIndex = new int[AllStageCount + 1]; // +1 是為了放置--End--的index
        EachStoryContentCount = new int[AllStageCount]; 
        StoryText = new string[StoryTextAllNum];//新增文本存取空間
        /*初始化數據*/


        LoadTextFile();//讀取文本

        /*需在這之前寫入賦予關卡id的程式碼*/
        GetStageStoryTextIndex();//獲取所有關卡的文本index
                                        /*
                                         index + 1 = TellerPos (Left or Right)
                                         index + 2 = TellerName;
                                         index + 3 = TellerImg;
                                         index + 4 = TellerText;
                                          */
        GetAllStoryTextContentCount();// 獲得各個關卡的文本長度

        RepatchStoryText(StageId,EachStoryContentCount);



    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.J))
        {
            DialogBoxControl(StageId,EachStoryContentCount);
        }

	}


    public void LoadTextFile()
    {
        //初始讀取文本       
        StoryText = File.ReadAllLines(Application.dataPath + "/GameData/StoryTextData.txt");
      
    }

    public void GetStageStoryTextIndex()
    {
        int _StageId = 0;
        int _index = 0;
        int _StoryIndexCount = 0;

        for (; _index < StoryText.Length; _index++)
        {
            if(StoryText[_index].Contains("--" + _StageId.ToString() + "--") || StoryText[_index].Contains("--End--"))
            {
                //break;
                StoryIndex[_StoryIndexCount] = _index;
                _StoryIndexCount++;
                _StageId++;
            }
        }

        //StoryIndex = _index;

    }// END GetStageStoryText 用以獲取故事文本index

    void GetAllStoryTextContentCount()
    {
        for(int i = 0; i < AllStageCount; i++)
        {                  
            EachStoryContentCount[i] = StoryIndex[i + 1] - StoryIndex[i]; // 用後index除去前index得到特定文本長度
        }
    } // End GetAllStoryTextContentCount

    void RepatchStoryText(int _StageId, int[] _EachStoryContentCount)
    {
        int ThisStageTellerContentCount = _EachStoryContentCount[_StageId] / 4; // 取得當前關卡的文本組數量
        int NextTellerContent = 4; // 用以跳至下一組文本

        NowTellerPos = new string[ThisStageTellerContentCount];
        NowTellerName = new string[ThisStageTellerContentCount];
        NowTellerImg = new string[ThisStageTellerContentCount];
        NowStageStoryText = new string[ThisStageTellerContentCount];// 給予空間大小

        for(int i = 0; i < ThisStageTellerContentCount; i++)
        {
            NowTellerPos[i] = StoryText[StoryIndex[_StageId] + 1 + i*NextTellerContent];
            NowTellerName[i] = StoryText[StoryIndex[_StageId] + 2 + i * NextTellerContent];
            NowTellerImg[i] = StoryText[StoryIndex[_StageId] + 3 + i * NextTellerContent];
            NowStageStoryText[i] = StoryText[StoryIndex[_StageId] + 4 + i * NextTellerContent];
        }// 重新將當前關卡文本分類

    }// End RepatchStoryText 

    void DialogBoxControl(int _StageId,int[] _EachStoryContentCount)
    {
        int ThisStageTellerContentCount = _EachStoryContentCount[_StageId] / 4;

        Debug.Log("Talk");
        if (IsTalking)
        {
            if (NowDialogIndex >= ThisStageTellerContentCount)
            {
                IsTalking = false;
                DialogBox.DOFade(0,2);
                TellerImgLeft.DOFade(0, 2);
                TellerImgRight.DOFade(0, 2);
                TellerName.DOFade(0, 2);
                TellerDialog.DOFade(0, 2);

            }//對話結束則關閉對話框
            else
            {
                TellerName.text = NowTellerName[NowDialogIndex];
                TellerDialog.text = NowStageStoryText[NowDialogIndex];
                NowDialogIndex++;
            }//若對話尚未結束則繼續

        }
    }// end DialogBoxControl 用以管理對話框的內容

}
