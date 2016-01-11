using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapInterface : MonoBehaviour {

    //public Image[] StageButton;
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
}
