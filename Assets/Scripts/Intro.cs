using UnityEngine;
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
    void Awake () {
        panel.enabled = false;
        popButton1.enabled = false;
        popButton2.enabled = false;
        b1Text.enabled = false;
        b2Text.enabled = false;
        panelText.enabled = false;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }
	
    void Update() {
        if (Input.GetMouseButtonDown(0)) // start 눌렸을 때
        {

            _player.SetName(InputField.textComponent.text);
            //Debug.Log(InputField.textComponent.text.ToString());
            panelText.text = "캐릭터명을 " + InputField.textComponent.text + "으로 정하시겠습니까?";
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


}
