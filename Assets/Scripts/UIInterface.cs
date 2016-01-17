using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInterface : MonoBehaviour
{
    int stageNum;

    public Text TimeText;
    public Text PlayerLevel;
    public Text PlayerName;
    public Image DistanceBar;
    public Image JumpButton;
    public Image AttackButton;
    public Image SkillButton;
    public Image SkillGage;
    
    public Camera maincamera;
    private int _layerMask;

    private int flag;
    private int clearTime;
    private float init_time;

    public new Transform transform;
    public PlayerController playercontroller;

    private Image[] menuPanel;
    private Image clearPanel;
    private Text clearText;
    private Image nextStageBt;
    private Text nextStageText;

    public Canvas ourCanvas;

    public string sceneToLoad;

    void Awake()
    {
        init_time = Time.time;
        clearTime = -1;
        flag = 0;

        stageNum = PlayerPrefs.GetInt("stageNum");
        TimeText.text = "00:00:00";
        switch (stageNum)
        {
            case 1:
                PlayerLevel.text = "사원";
                break;
            case 2:
                PlayerLevel.text = "대리";
                break;
            case 3:
                PlayerLevel.text = "과장";
                break;
            case 4:
                PlayerLevel.text = "부장";
                break;
            case 5:
                PlayerLevel.text = "이사";
                break;
            default:
                PlayerLevel.text = "스테이지 넘버 오류";
                break;
        }
        PlayerName.text = GameData.data.playerName;
        DistanceBar.fillAmount = 0.0f;
        //setTimeText();
        //Obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        GameObject[] menuObject = GameObject.FindGameObjectsWithTag("MenuPanel");
        menuPanel = new Image[menuObject.Length];
        int i = 0;
        foreach (GameObject temp in menuObject)
        {
            menuPanel[i] = temp.GetComponent<Image>();
            menuPanel[i].enabled = false;
            if (temp.GetComponentInChildren<Text>())
                menuPanel[i].GetComponentInChildren<Text>().enabled = false;
            i++;
        }

        GameObject clearObject = GameObject.FindGameObjectWithTag("ClearPanel");
        clearPanel = clearObject.GetComponent<Image>();
        clearPanel.enabled = false;
        if (clearObject.GetComponentInChildren<Text>())
        {
            clearText = clearPanel.GetComponentInChildren<Text>();
            clearText.enabled = false;
        }
        if (clearObject.GetComponentInChildren<Image>())
        {
            nextStageBt = clearPanel.GetComponentInChildren<Image>();
            nextStageBt.enabled = false;
            nextStageText = nextStageBt.GetComponentInChildren<Text>();
            nextStageText.enabled = false;
        }

        transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playercontroller = transform.GetComponent<PlayerController>();

        ourCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    public void nextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void gameClear()
    {
        if (clearTime > 0)
        {
            GameData.data.times[stageNum] = clearTime;
            GameData.data.stars[stageNum] = 3;
            GameData.data.Save();
            
            clearPanel.enabled = true;
            if (clearPanel.GetComponentInChildren<Text>())
            {
                clearText = clearPanel.GetComponentInChildren<Text>();
                clearText.text = "스테이지 클리어 \n" + GameData.data.playerName;
                switch (stageNum)
                {
                    case 1:
                        clearText.text += " 사원님, 그럭저럭 쓸만한데?";
                        break;
                    case 2:
                        clearText.text += " 대리님, 그럭저럭 쓸만한데?";
                        break;
                    case 3:
                        clearText.text += " 과장님, 그럭저럭 쓸만한데?";
                        break;
                    case 4:
                        clearText.text += " 부장님, 정말 대단하시지 말입니다.";
                        break;
                    case 5:
                        clearText.text += " 이사님, 정말 대단하시지 말입니다.";
                        break;
                    default:
                        clearText.text += " 직급 오류";
                        break;
                }
                clearText.enabled = true;
            }
            if (clearPanel.GetComponentInChildren<Image>())
            {
                nextStageBt = clearPanel.GetComponentInChildren<Image>();
                nextStageBt.enabled = false;
                nextStageText = nextStageBt.GetComponentInChildren<Text>();
                nextStageText.enabled = false;
            }
        }
    }

    private void setTimeText() {

        int millisecond = (int)((Time.time - init_time) * 100) % 100;
        int minute = (int)(Time.time- init_time) / 60;
        int second = (int)(Time.time - init_time) % 60;
        string minuteS;
        string secondS;
        string millisecondS;

        clearTime = second + minute * 60;

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

        TimeText.text = minuteS + ":" + secondS + ":" + millisecondS;

    }

    private void setDistanceBar()
    {
        DistanceBar.fillAmount = (playercontroller.max_distance - playercontroller.distance) / playercontroller.max_distance;
    }

    void Update()
    {
        setDistanceBar();
        //SkillGage.fillAmount += Time.deltaTime/5;
        setTimeText();

       ButtonCheck();
    }


    public void exit() {

        Application.Quit();

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
        if (Time.timeScale == 0)
        {
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
        }
        else {
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
                if (SkillGage.fillAmount == 1){
                    SkillBt();
                    SkillGage.fillAmount = 0;
                }
                flag = 0;

            }

        } else if (Input.GetMouseButtonUp(0) && flag == 1) { //  공격 버튼 누르고 떼면

            if (CheckTouchUIRound(Input.mousePosition, AttackButton)) {
                flag = 0;
                AttackBt();
                print("up Attack Bt");
            }

        } else if (Input.GetMouseButtonDown(0)) {

            if (CheckTouchUIRound(Input.mousePosition, AttackButton)){
                flag = 1;
                print("press Attack Bt");
            }else if (CheckTouchUIRound(Input.mousePosition, JumpButton)){
                JumpBt();
                flag = 0;
            }

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

    /*
    private bool CheckTouchUIRound(Vector3 position, Image img)
    {        //버튼이 원형 일 때 

        Vector3 screenPos1 = maincamera.ScreenToWorldPoint(position);
        Vector3 screenPos2 = maincamera.ScreenToWorldPoint(img.transform.position);
        print("screenPos : " + screenPos1);
        print("screenPos : " + screenPos2);
        print("position : " + position);
        print("img.transform.position : " + img.transform.position);
        //
        //if (Vector3.Distance(img.transform.position, position) <= img.rectTransform.rect.width / 2)
        if (Vector3.Distance(position., img.transform.position) <= img.rectTransform.rect.width / 2)
        {
            print("Vector3.Distance(img.transform.position, position) : " + Vector3.Distance(screenPos1, screenPos2));
            print("img.rectTransform.rect.width / 2 : " + img.rectTransform.rect.width / 2);

            return true;
        }

        return false;
    }*/

    
    private bool CheckTouchUIRound(Vector3 position, Image img) // 버튼이 원일 때
    {
        // 현재 실제 (월드) 스케일 / 원래 계획된 (캔버스가 생각하는) 스케일
        //print("scale factor" + ourCanvas.scaleFactor);
        float r = img.rectTransform.rect.width / 2 * ourCanvas.scaleFactor;
        if (Vector3.Distance(img.transform.position, position) < r){
            /*
            print("r : " + r);
            print("calculate : "+ Vector3.Distance(img.transform.position, position));
            print("button position"+ img.transform.position);
            print("click position" + position);
            */

            return true;

        }

        return false;
    }
    


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
        if (playercontroller.state == FSMBase.State.Jump)
        {
            playercontroller.DoJump2();
        }
        else
        {
            playercontroller.DoJump();

        }
    }

    

}
