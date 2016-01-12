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
    public Image SkillGage;
    

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
        TimeText.text = "00:00:00";
        PlayerLevel.text = "사원";
        PlayerName.text = "오늘만";
        DistanceBar.fillAmount = 0.0f;
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

        int millisecond = (int)((Time.time - init_time) * 100) % 100;
        int minute = (int)(Time.time- init_time) / 60;
        int second = (int)(Time.time - init_time) % 60;
        string minuteS;
        string secondS;
        string millisecondS;

        if (millisecond / 10 == 0)
        {
            millisecondS = "0" + millisecond.ToString();
        }
        else
        {
            millisecondS = millisecond.ToString();
        }

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

        TimeText.text = minuteS + ":" + secondS + " : " + millisecondS;

    }

    private void setDistanceBar() {

        DistanceBar.fillAmount = (playercontroller.max_distance - playercontroller.distance) / playercontroller.max_distance;

    }

    void Update() {

        //Time.timeScale;
        setDistanceBar();
        //timer += Time.deltaTime;
        SkillGage.fillAmount += Time.deltaTime/5;
        setTimeText();
        /*
        if (timer >= 1) { 
            timer = 0;
            //setTimeText();
        }*/
        

        ButtonCheck();

    }

    // skill버튼 게이지 테스트 코드 -------------------삭제 예정 테스트 코드
    public void printSkillBtActive()
    {
        if (SkillGage.fillAmount == 1) {

            SkillGage.fillAmount = 0;
            print("SkillBt");
        }

    }


    //일시 정지 버튼
    public void pause() {
        if (Time.timeScale == 0) {

            //일시 정지 버튼 끌 시 다른 UI버튼 클릭 활성화
            JumpButton.GetComponent<Button>().enabled = true;
            AttackButton.GetComponent<Button>().enabled = true;
            SkillButton.GetComponent<Button>().enabled = true;
            SkillButton.GetComponent<Button>().enabled = true;
            SkillGage.GetComponent<Button>().enabled = true;

            foreach (Image temp in menuPanel)
            {

                temp.enabled = false;
                if (temp.GetComponentInChildren<Text>())
                    temp.GetComponentInChildren<Text>().enabled = false;
            }
            Time.timeScale = 1;

        } else {


            //일시 정지 버튼 클릭시 다른 UI버튼 클릭 비활성화
            JumpButton.GetComponent<Button>().enabled = false;
            AttackButton.GetComponent<Button>().enabled = false;
            SkillButton.GetComponent<Button>().enabled = false;
            SkillButton.GetComponent<Button>().enabled = false;
            SkillGage.GetComponent<Button>().enabled = false;

            
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
            if (CheckTouchUIRound(Input.mousePosition, SkillButton)) { // 드래그 한 것이 스킬 버튼까지 오면
                if (SkillGage.fillAmount == 1)
                {
                    SkillBt();
                    print("SkillBt Active");
                    SkillGage.fillAmount = 0;
                }
                print("SkillBt checked");
                flag = 0;

            }

        } else if (Input.GetMouseButtonUp(0) && flag == 1)    //  공격 버튼 누르고 떼면
        {
            if (CheckTouchUIRound(Input.mousePosition, AttackButton)) 
            {
                flag = 1;
                AttackBt();
            }
        } else if (Input.GetMouseButtonDown(0)) {

            if (CheckTouchUIRound(Input.mousePosition, AttackButton)){
                flag = 1;

            } else if (CheckTouchUIRound(Input.mousePosition, JumpButton)){

                JumpBt();
            }

        }else {

            flag = 0;
        }

    }
    

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
