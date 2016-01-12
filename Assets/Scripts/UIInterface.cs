using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIInterface : MonoBehaviour
{

    public Text TimeText;
    public Text PlayerLevel;
    public Text PlayerName;
    public Image DistanceBar;
    public Image JumpButton;
    public Image AttackButton;
    public Image SkillButton;
    

    private int _layerMask;

    private int flag;
    private float timer;
    private float init_time;

    public new Transform transform;
    public PlayerController playercontroller;

    private Image[] menuPanel;


    void Awake() {

        init_time = Time.time;
        timer = 0.0f;
        flag = 0;
        TimeText.text = "00:00";
        PlayerLevel.text = "사원";
        PlayerName.text = "오늘만";
        DistanceBar.fillAmount = 0.5f;
        //setTimeText();
        //Obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        GameObject[] tempObject = GameObject.FindGameObjectsWithTag("MenuPanel");
        menuPanel = new Image[tempObject.Length];
        int i = 0;
        foreach (GameObject temp in tempObject) {

            menuPanel[i] = temp.GetComponent<Image>();
            menuPanel[i].enabled = false;
            if (temp.GetComponentInChildren<Text>())
                menuPanel[i].GetComponentInChildren<Text>().enabled = false;
            i++;
        }



        transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playercontroller = transform.GetComponent<PlayerController>();
        

    }

    private void setTimeText() {

        int minute = (int)(Time.time- init_time) / 60;
        int second = (int)(Time.time - init_time) % 60;
        string minuteS;
        string secondS;

        if (second / 10 == 0) {
            secondS = "0" + second.ToString();
        }
        else
        {
            secondS = second.ToString();
        }

        if (minute / 10 == 0)
        {
            minuteS = "0" + minute.ToString();
        }
        else
        {
            minuteS =  minute.ToString();
        }

        TimeText.text = minuteS + ":" + secondS;

    }

    void Update() {

        //Time.timeScale;

        //timer += Time.deltaTime;

        setTimeText();
        /*
        if (timer >= 1) { 
            timer = 0;
            //setTimeText();
        }*/
        

        ButtonCheck();

    }

    public void pause() {
        if (Time.timeScale == 0){

            foreach (Image temp in menuPanel)
            {

                temp.enabled = false;
                if (temp.GetComponentInChildren<Text>())
                    temp.GetComponentInChildren<Text>().enabled = false;
            }
            Time.timeScale = 1;

        } else {

            foreach (Image temp in menuPanel)
            {

                temp.enabled = true;
                if (temp.GetComponentInChildren<Text>())
                    temp.GetComponentInChildren<Text>().enabled = true;
            }
            Time.timeScale = 0;

        }
        
    }


    private void ButtonCheck() {

        // flag 는 공격 버튼 눌렀을 때 활성화

        if (flag == 1 && Input.GetMouseButton(0)) {
            if (CheckTouchUI(Input.mousePosition, SkillButton)) { // 드래그 한 것이 스킬 버튼까지 오면
                SkillBt();
                flag = 0;
            }

        } else if (Input.GetMouseButtonUp(0) && flag == 1)    //  공격 버튼 누르고 떼면
        {
            if (CheckTouchUI(Input.mousePosition, AttackButton)) 
            {
                flag = 1;
                AttackBt();
            }
        } else if (Input.GetMouseButtonDown(0)) {

            if (CheckTouchUI(Input.mousePosition, AttackButton)){
                flag = 1;

            } else if (CheckTouchUI(Input.mousePosition, JumpButton)){

                JumpBt();
            }

        }else {

            flag = 0;
        }

    }
    /*
    public void UnStop() {


    }

    public void Stop() {

        StopBackground();
        StopObatacle();
        
    }

    private void StopBackground() {

        bgm.moveFlag = false;

    }

    private void StopObatacle() {



    }
    */

    private bool CheckTouchUI(Vector3 position, Image img) {        //버튼이 사각형 일 때 

        if (img.transform.position.x + img.rectTransform.rect.width / 2 > position.x
            && img.transform.position.x - img.rectTransform.rect.width / 2 < position.x
            && img.transform.position.y + img.rectTransform.rect.height / 2 > position.y
            && img.transform.position.y - img.rectTransform.rect.height / 2 < position.y) {

            return true;

        }

        return false;
    }

    private bool CheckTouchUIRound(Vector3 position, Image img)
    {        //버튼이 원형 일 때 

        if (Vector3.Distance(img.transform.position, position) <= img.rectTransform.rect.width / 2)
        {
            return true;
        }

        return false;
    }

    /*
    private bool CheckTouchUIRound(Vector3 position, Image img) // 버튼이 원일 때
    {
        float r = img.rectTransform.rect.width / 2;
        if ((img.transform.position.x - position.x) * (img.transform.position.x - position.x) 
            + (img.transform.position.y - position.y) * (img.transform.position.y - position.y) < r*r){

            return true;

        }

        return false;
    }
    */


    public void SkillBt()  // 어택 버튼 눌렸을 때 실행
    {

        //Debug.Log("스킬 시전");
        playercontroller.DoSkill();
        
    }

    public void AttackBt()  // 어택 버튼 눌렸을 때 실행
    {

        //Debug.Log("공격 시전");
        playercontroller.DoAttack();
    }

    public void JumpBt()    // 점프  버튼 눌렸을 떄 실행
    {
        // 점프 액션 
        //Debug.Log("점프 시전");
        playercontroller.DoJump();
    }

    

}
