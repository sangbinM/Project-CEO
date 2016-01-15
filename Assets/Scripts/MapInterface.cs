using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapInterface : MonoBehaviour {

    public Button[] StageButton;
    public int[] playTime;
    // 플레이 타임이 (-1)이면 아직 성공하지 못한 스테이지
    public string[] StageDetail = new string[] {"대리 던전",
        "과장 던전", "부장 던전", "전무 던전", "사장 던전" };
    
    public string sceneToLoad;

    public Image panel;
    public Text panelText;
    public Image startButton;
    public Text startText;
    public Image endButton;
    public Text endText;

    //public new Transform transform;

    void Awake()
    {
        panel.enabled = false;
        startButton.enabled = false;
        endButton.enabled = false;
        startText.enabled = false;
        endText.enabled = false;
        panelText.enabled = false;
        //playTime = new int[] { 40, 50, 60, 70, -1 };
        setStageTime();
    }

    void Update()
    {
        //ButtonCheck();
    }

    /*
    private void ButtonCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckTouchUI(Input.mousePosition, StageButton[0]))
            {
                StageBt(1);
            }
            else if (CheckTouchUI(Input.mousePosition, StageButton[1]))
            {
                StageBt(2);
            }
            else if (CheckTouchUI(Input.mousePosition, StageButton[2]))
            {
                StageBt(3);
            }
            else if (CheckTouchUI(Input.mousePosition, StageButton[3]))
            {
                StageBt(4);
            }
            else if (CheckTouchUI(Input.mousePosition, StageButton[4]))
            {
                StageBt(5);
            }
        }
    }
    */

    public void StageBt(int stageNum)
    {
        //sceneToLoad = "Stage" + stageNum;
        sceneToLoad = "First Scene";
        panelText.text = StageDetail[stageNum - 1];
        /*
        switch (stageNum)
        {
            case 1:
                panelText.text = "대리 던전";
                break;
            case 2:
                panelText.text = "과장 던전";
                break;
            case 3:
                panelText.text = "부장 던전";
                break;
            case 4:
                panelText.text = "전무 던전";
                break;
            case 5:
                panelText.text = "사장 던전";
                break;
            default:
                panelText.text = "??? 던전";
                break;
        }
        */
        OpenPopup();
    }

    public void OpenPopup()
    {
        panel.enabled = true;
        startButton.enabled = true;
        endButton.enabled = true;
        startText.enabled = true;
        endText.enabled = true;
        panelText.enabled = true;
    }

    public void OpenStage()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ClosePopup()
    {
        panel.enabled = false;
        startButton.enabled = false;
        endButton.enabled = false;
        startText.enabled = false;
        endText.enabled = false;
        panelText.enabled = false;
    }

    private void setStageTime()
    {
        int minute, second;
        string minuteS, secondS;
        Button bt;
        ColorBlock cb;
        int currentStageNum = -1;

        for (int i = 0; i < 5; i++)
        {
            if (playTime[i] < 0)
            {
                if (currentStageNum < 0)
                    currentStageNum = i;

                if (currentStageNum < i)
                {
                    bt = StageButton[i].GetComponent<Button>();
                    bt.enabled = false;
                    cb = bt.colors;
                    cb.normalColor = Color.grey;
                    cb.highlightedColor = Color.gray;
                    cb.pressedColor = Color.black;
                    cb.disabledColor = Color.black;
                    bt.colors = cb;
                }
                StageDetail[i] = StageDetail[i] + "\n 미정복";
            }
            else
            {
                minute = playTime[i] / 60;
                second = playTime[i] % 60;

                if (minute / 10 < 1)
                    minuteS = "0" + minute.ToString();
                else
                    minuteS = minute.ToString();

                if (second / 10 < 1)
                    secondS = "0" + second.ToString();
                else
                    secondS = second.ToString();

                StageDetail[i] = StageDetail[i] + "\n" + minuteS + ":" + secondS;
            }
        }
    }
}