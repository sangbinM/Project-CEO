using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
    public InputField InputField;
    public Button StartButton;

    //private PlayerController _player;

    private string sceneToLoad;  // next loading scene

    public enum IntroState
    {
        Idle, PopUp
    }

    public IntroState I_state = IntroState.Idle;

    // Use this for initialization
    void Awake () {
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }
	
    void Update() {
        //Invoke(I_state.ToString(), 0f);
        //Invoke("SetState", 0.5f);

        if (Input.GetMouseButtonDown(0)) // start 눌렸을 때
        {

            //_player.SetName(InputField.textComponent.ToString());
            OnGUI();
            DoMyWindow(0);
        }
    }

    void IntroIdle()
    {

    }

    void IntroPopUP()
    {

    }

    public Rect windowRect = new Rect(20, 20, 120, 50);

    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "My Window");
    }
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
            print("Got a click");

    }

}
