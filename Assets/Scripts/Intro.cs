﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public InputField InputField;
    public Button StartButton; // 팝업용 버튼

    private PlayerController _player;
    public string sceneToLoad;  // next loading scene

    public Image panel;
    public Image popButton1;
    public Image popButton2;
    public Text b1Text;
    public Text b2Text;
    public Text panelText;

    // Use this for initialization
   /* void Awake () {
        

        
        GameData.data.playerName = "오늘만";
        GameData.data.stars = new int[] { 3, 2, 1, -1, -1 };
        GameData.data.times = new int[] { 50, 60, 70, -1, -1 };
        GameData.data.Save();
        
        
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();        
    }*/

    void Start()
    {
        panel.enabled = false;
        popButton1.enabled = false;
        popButton2.enabled = false;
        b1Text.enabled = false;
        b2Text.enabled = false;
        panelText.enabled = false;

        // 로드할 데이터가 없을 때를 위한 디폴트 값
        GameData.data.stars = new int[] { -1, -1, -1, -1, -1 };
        GameData.data.times = new int[] { -1, -1, -1, -1, -1 };

        GameData.data.Load();

        
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) // start 눌렸을 때
        {
            
        }
    }


    public void OpenPopup()
    {
        panel.enabled = true;
        popButton1.enabled = true;
        popButton2.enabled = true;
        b1Text.enabled = true;
        b2Text.enabled = true;
        panelText.enabled = true;

        //_player.SetName(InputField.textComponent.text);
        GameData.data.playerName = InputField.textComponent.text;
        //Debug.Log(InputField.textComponent.text.ToString());

        if (InputField.textComponent.text != "")
        {
            panelText.text = "캐릭터명을 " + InputField.textComponent.text + "(으)로 정하시겠습니까?";
        }
        else
        {
            GameData.data.playerName = "오늘만";
            panelText.text = "캐릭터명을 입력하지 않았습니다. 기본 캐릭터명인 [" + GameData.data.playerName + "]으로 설정됩니다.";
        }
    }

    public void ClosePopup()
    {
        panel.enabled = false;
        popButton1.enabled = false;
        popButton2.enabled = false;
        b1Text.enabled = false;
        b2Text.enabled = false;
        panelText.enabled = false;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void inActiveStartButton()
    {
        if(InputField.textComponent.text == "")
        {
            popButton2.enabled = false;
            b2Text.enabled = false;
        } 
    }
}
