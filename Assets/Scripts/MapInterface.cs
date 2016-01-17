using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapInterface : MonoBehaviour {

    public Button[] StageButton;
    public int[] playTime;
    // 플레이 타임이 (-1)이면 아직 성공하지 못한 스테이지
    public string[] StageInfo;
    public string[] StageDetail;
    
    public string sceneToLoad;

    public Image panel;
    public Text panelInfo;
    public Text panelDetail;
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
        panelInfo.enabled = false;
        panelDetail.enabled = false;
        playTime = GameData.data.times;
        StageInfo = new string[] { "대리 던전", "과장 던전", "부장 던전", "임원 던전", "사장 던전" };
        StageDetail = new string[] {"피곤에 지친 대리님들로 가득한 던전입니다. 주의 요망!",
            "고통받는 과장님들의 단말마가 울려퍼지는 던전입니다. 한적한 곳은 피하십시오!",
            "경고! 분노에 찬 부장님들의 시선을 피하십시오. 아무 말도 할 수 없게 됩니다!",
            "공포스런 임원님들이 뿜어내는 냉기로 몸이 굳습니다. 공포를 이겨내고 던전을 클리어하십시오!",
            "심연의 사장님이 계신 곳입니다. 겸손한 마음으로 임하십시오!" };
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
        PlayerPrefs.SetInt("stageNum", stageNum);
        PlayerPrefs.Save();
        sceneToLoad = "First Scene";
        panelInfo.text = StageInfo[stageNum - 1];
        panelDetail.text = StageDetail[stageNum - 1];
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
        panelInfo.enabled = true;
        panelDetail.enabled = true;
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
        panelInfo.enabled = false;
        panelDetail.enabled = false;
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
                StageInfo[i] = StageInfo[i] + "\n 미정복";
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

                StageInfo[i] = StageInfo[i] + "\n 최고 기록 " + minuteS + ":" + secondS + "\n 등급 ";
                StageInfo[i] = StageInfo[i] + "★☆☆";
            }
        }
    }
}